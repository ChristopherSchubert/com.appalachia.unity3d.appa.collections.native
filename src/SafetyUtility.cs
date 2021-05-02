#region

using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

#endregion

namespace collections.native.src
{
    public static class SafetyUtility
    {
        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void RequireLengthWithinCapacity(int length, int capacity, int v)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if ((length < 0) || (length > capacity))
            {
                if (v >= 0)
                {
                    throw new IndexOutOfRangeException(
                        string.Format(
                            "Length{0} [{1}] out of bounds.  Capacity{2}: [{3}].",
                            (object) v,
                            (object) length,
                            (object) v,
                            (object) capacity
                        )
                    );
                }

                throw new IndexOutOfRangeException(string.Format("Length [{0}] out of bounds.  Capacity: [{1}].", length, capacity));
            }
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void RequireIndexInBounds(
            int index0,
            int length0,
            int capacity0,
            int index1,
            int length1,
            int capacity1)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if ((index0 < 0) || (index0 >= length0) || (index0 >= capacity0) || (index1 < 0) || (index1 >= length1) || (index1 >= capacity1))
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "Index out of bounds.  Idx0 [{0}] Len0: [{1}] Cap0: [{2}] / Idx1 [{3}] Len1: [{4}] Cap1: [{5}] / ",
                        (object) index0,
                        (object) length0,
                        (object) capacity0,
                        (object) index1,
                        (object) length1,
                        (object) capacity1
                    )
                );
            }
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void RequireIndexInBounds(
            int index0,
            int length0,
            int capacity0,
            int index1,
            int length1,
            int capacity1,
            int index2,
            int length2,
            int capacity2)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if ((index0 < 0) ||
                (index0 >= length0) ||
                (index0 >= capacity0) ||
                (index1 < 0) ||
                (index1 >= length1) ||
                (index1 >= capacity1) ||
                (index2 < 0) ||
                (index2 >= length2) ||
                (index2 >= capacity2))
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "Index out of bounds.  Idx0 [{0}] Len0: [{1}] Cap0: [{2}] / Idx1 [{3}] Len1: [{4}] Cap1: [{5}] / Idx2 [{6}] Len2: [{7}] Cap2: [{8}] / ",
                        (object) index0,
                        (object) length0,
                        (object) capacity0,
                        (object) index1,
                        (object) length1,
                        (object) capacity1,
                        (object) index2,
                        (object) length2,
                        (object) capacity2
                    )
                );
            }
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void RequireIndexInBounds(int index, int length, int capacity)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if ((index < 0) || (index >= length) || (index >= capacity))
            {
                throw new IndexOutOfRangeException(
                    string.Format("Index [{0}] out of bounds.  Length: [{1}]  Capacity: [{2}].", index, length, capacity)
                );
            }
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void RequireValidAllocator<T>(Allocator allocator)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (!UnsafeUtility.IsValidAllocator(allocator))
            {
                throw new InvalidOperationException(
                    string.Format("The {0} can not be Disposed because it was not allocated with a valid allocator.", typeof(T).GetPrintableName())
                );
            }
#endif
        }

        [BurstDiscard]
        public static void IsUnmanagedAndThrow<TC, TE1, TE2>()
        {
            IsUnmanagedAndThrow<TC, TE1>();
            IsUnmanagedAndThrow<TC, TE2>();
        }

        [BurstDiscard]
        public static void IsUnmanagedAndThrow<TC, TE>()
        {
            if (!UnsafeUtility.IsValidNativeContainerElementType<TE>())
            {
                throw new InvalidOperationException(
                    string.Format(
                        "{0} used in {1} must be unmanaged (contain no managed types) and cannot itself be a native container type.",
                        typeof(TE).GetPrintableName(),
                        typeof(TC).GetPrintableName()
                    )
                );
            }
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void CheckElementReadAccess(
            AtomicSafetyHandle safety,
            int index,
            int minimum,
            int maximum,
            int length,
            int capacity)
        {
            if ((index < minimum) || (index > maximum))
            {
                FailOutOfRangeError(index, minimum, maximum, length, capacity);
            }

            /*if (this.m_Safety.version == (*(int*) (void*) this.m_Safety.versionNode & -7))
                return;*/
            AtomicSafetyHandle.CheckReadAndThrow(safety);
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void CheckElementWriteAccess(
            AtomicSafetyHandle safety,
            int index,
            int minimum,
            int maximum,
            int length,
            int capacity)
        {
            if ((index < minimum) || (index > maximum))
            {
                FailOutOfRangeError(index, minimum, maximum, length, capacity);
            }

            /*if (this.m_Safety.version == (*(int*) (void*) this.m_Safety.versionNode & -6))
                return;*/
            AtomicSafetyHandle.CheckWriteAndThrow(safety);
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void CheckReadAccess(AtomicSafetyHandle safety)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckReadAndThrow(safety);
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void CheckWriteAccess(AtomicSafetyHandle safety)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndThrow(safety);
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        [BurstDiscard]
        public static void FailOutOfRangeError(int index, int minimum, int maximum, int length, int capacity)
        {
            if ((index < length) && ((minimum != 0) || (maximum != (capacity - 1))))
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "Index {0} is out of restricted IJobParallelFor range [{1}...{2}] in ReadWriteBuffer.\nReadWriteBuffers are restricted to only read & write the element at the job index. You can use double buffering strategies to avoid race conditions due to reading & writing in parallel to the same elements from a job.",
                        index,
                        minimum,
                        maximum
                    )
                );
            }

            RequireIndexInBounds(index, length, capacity);
            RequireLengthWithinCapacity(length, capacity, -1);
        }
    }
}
