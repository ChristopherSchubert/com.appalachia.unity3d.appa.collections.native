using System.Runtime.InteropServices;
using Unity.Jobs.LowLevel.Unsafe;

namespace collections.native.src
{
    /// <summary>
    ///     The state of a <see cref="NativeHashSet{T}" />. Shared among instances
    ///     of the struct via a pointer to unmanaged memory. This has no type
    ///     parameters, so it can be used by all set types.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct NativeHashSetState
    {
        /// <summary>
        ///     Item data
        /// </summary>
        [FieldOffset(0)]
        internal byte* Items;

        // 4-byte padding on 32-bit architectures here

        /// <summary>
        ///     Next bucket data
        /// </summary>
        [FieldOffset(8)]
        internal byte* Next;

        // 4-byte padding on 32-bit architectures here

        /// <summary>
        ///     Bucket data
        /// </summary>
        [FieldOffset(16)]
        internal byte* Buckets;

        // 4-byte padding on 32-bit architectures here

        /// <summary>
        ///     Capacity of the <see cref="Items" /> array
        /// </summary>
        [FieldOffset(24)]
        internal int ItemCapacity;

        /// <summary>
        ///     Bucket capacity - 1
        /// </summary>
        [FieldOffset(28)]
        internal int BucketCapacityMask;

        /// <summary>
        ///     Allocated index length
        /// </summary>
        [FieldOffset(32)]
        internal int AllocatedIndexLength;

        /// <summary>
        ///     Array indexed by thread index of first free
        /// </summary>
        [FieldOffset(JobsUtility.CacheLineSize < 64 ? 64 : JobsUtility.CacheLineSize)]
        internal fixed int FirstFreeTLS[JobsUtility.MaxJobThreadCount * IntsPerCacheLine];

        /// <summary>
        ///     Number of ints that fit in one cache line
        /// </summary>
        internal const int IntsPerCacheLine = JobsUtility.CacheLineSize / sizeof(int);
    }
}