//-----------------------------------------------------------------------
// <copyright file="NativeHashSet.cs" company="Jackson Dunstan">
//     Copyright (c) Jackson Dunstan. See LICENSE.md.
// </copyright>
//-----------------------------------------------------------------------

#region

using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

#endregion

namespace collections.native.src
{
    // Mark this struct as a NativeContainer, usually this would be a generic struct for containers, but a counter does not need to be generic
    [StructLayout(LayoutKind.Sequential)]
    [NativeContainer]
    public unsafe struct NativeCounter
    {
        // The actual pointer to the allocated count needs to have restrictions relaxed so jobs can be schedled with this container
        [NativeDisableUnsafePtrRestriction]
        private int* m_Counter;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        private AtomicSafetyHandle m_Safety;

        // The dispose sentinel tracks memory leaks. It is a managed type so it is cleared to null when scheduling a job
        // The job cannot dispose the container, and no one else can dispose it until the job has run, so it is ok to not pass it along
        // This attribute is required, without it this NativeContainer cannot be passed to a job; since that would give the job access to a managed object
        [NativeSetClassTypeToNullOnSchedule]
        private DisposeSentinel m_DisposeSentinel;
#endif

        // Keep track of where the memory for this was allocated
        private readonly Allocator m_AllocatorLabelLabel;

        public NativeCounter(Allocator label)
        {
            // This check is redundant since we always use an int that is blittable.
            // It is here as an example of how to check for type correctness for generic types.
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (!UnsafeUtility.IsBlittable<int>())
            {
                throw new ArgumentException(string.Format("{0} used in NativeQueue<{0}> must be blittable", typeof(int)));
            }
#endif
            m_AllocatorLabelLabel = label;

            // Allocate native memory for a single integer// One full cache line (integers per cacheline * size of integer) for each potential worker index, JobsUtility.MaxJobThreadCount
            m_Counter = (int*) UnsafeUtility.Malloc(UnsafeUtility.SizeOf<int>() * IntsPerCacheLine * JobsUtility.MaxJobThreadCount, 4, label);

            // Create a dispose sentinel to track memory leaks. This also creates the AtomicSafetyHandle
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Create(out m_Safety, out m_DisposeSentinel, 0, label);
#endif

            // Initialize the count to 0 to avoid uninitialized data
            Count = 0;
        }

        public void Increment()
        {
            // Verify that the caller has write permission on this data. 
            // This is the race condition protection, without these checks the AtomicSafetyHandle is useless
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif
            (*m_Counter)++;
        }

        public int Count
        {
            get
            {
                // Verify that the caller has read permission on this data. 
                // This is the race condition protection, without these checks the AtomicSafetyHandle is useless
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
#endif
                var count = 0;
                for (var i = 0; i < JobsUtility.MaxJobThreadCount; ++i)
                {
                    count += m_Counter[IntsPerCacheLine * i];
                }

                return count;
            }
            set
            {
                // Verify that the caller has write permission on this data. 
                // This is the race condition protection, without these checks the AtomicSafetyHandle is useless
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif

                // Clear all locally cached counts, 
                // set the first one to the required value
                for (var i = 1; i < JobsUtility.MaxJobThreadCount; ++i)
                {
                    m_Counter[IntsPerCacheLine * i] = 0;
                }

                *m_Counter = value;
            }
        }

        public bool IsCreated => m_Counter != null;

        public void Dispose()
        {
            // Let the dispose sentinel know that the data has been freed so it does not report any memory leaks
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Dispose(ref m_Safety, ref m_DisposeSentinel);
#endif

            UnsafeUtility.Free(m_Counter, m_AllocatorLabelLabel);
            m_Counter = null;
        }

        /// <summary>
        ///     Create a <see cref="ParallelWriter" /> that writes to this set.
        ///     This operation has no access requirements, but using the writer
        ///     requires write access.
        /// </summary>
        /// <returns>
        ///     A <see cref="ParallelWriter" /> that writes to this set
        /// </returns>
        public ParallelWriter AsParallelWriter()
        {
            ParallelWriter writer;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            writer.m_Safety = m_Safety;
#endif
            writer.m_Counter = m_Counter;
            writer.m_ThreadIndex = 0;

            return writer;
        }

        public const int IntsPerCacheLine = JobsUtility.CacheLineSize / sizeof(int);

        /// <summary>
        ///     Implements parallel writer. Use AsParallelWriter to obtain it from container.
        /// </summary>
        [NativeContainer]
        [NativeContainerIsAtomicWriteOnly]
        public struct ParallelWriter
        {
            [NativeDisableUnsafePtrRestriction]
            internal int* m_Counter;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            internal AtomicSafetyHandle m_Safety;
#endif

            // The current worker thread index; it must use this exact name since it is injected
            [NativeSetThreadIndex] internal int m_ThreadIndex;

            public static implicit operator ParallelWriter(NativeCounter cnt)
            {
                ParallelWriter concurrent;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckWriteAndThrow(cnt.m_Safety);
                concurrent.m_Safety = cnt.m_Safety;
                AtomicSafetyHandle.UseSecondaryVersion(ref concurrent.m_Safety);
#endif

                concurrent.m_Counter = cnt.m_Counter;
                concurrent.m_ThreadIndex = 0;
                return concurrent;
            }

            public void Increment()
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif

                // No need for atomics any more since we are just incrementing the local count
                ++m_Counter[IntsPerCacheLine * m_ThreadIndex];
            }
        }
    }
}
