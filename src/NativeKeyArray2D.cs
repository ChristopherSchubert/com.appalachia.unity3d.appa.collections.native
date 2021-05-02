#region

using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

#endregion

namespace collections.native.src
{
    [DebuggerDisplay("Length0 = {Length0}, Length1 = {Length1}, Capacity0 = {Capacity0}, Capacity1 = {Capacity1}")]
    [DebuggerTypeProxy(typeof(NativeKeyArray2DDebugView<,>))]
    [NativeContainer]
    [NativeContainerSupportsDeallocateOnJobCompletion]
    [NativeContainerSupportsMinMaxWriteRestriction]
    public unsafe struct NativeKeyArray2D<TK, TV> : IDisposable, IEquatable<NativeKeyArray2D<TK, TV>>
        where TK : struct
        where TV : struct
    {
        [NativeDisableUnsafePtrRestriction]
        private void* m_BufferKeys;

        [NativeDisableUnsafePtrRestriction]
        private void* m_Buffer;

        internal int m_Length;
        internal int m_MinIndex;
        internal int m_MaxIndex;

#if ENABLE_UNITY_COLLECTIONS_CHECKS

        internal AtomicSafetyHandle m_Safety;

        [NativeSetClassTypeToNullOnSchedule]
        private DisposeSentinel m_DisposeSentinel;
#endif

        internal Allocator m_AllocatorLabel;

        public NativeKeyArray2D(int length0, int length1, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
        {
            Allocate(length0, length1, allocator, out this);
            if ((options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory)
            {
                UnsafeUtility.MemClear(m_Buffer, TotalLength * (long) UnsafeUtility.SizeOf<TV>());
            }
        }

        public NativeKeyArray2D(TK[] keys, TV[,] array, Allocator allocator)
        {
            var length0 = array.GetLength(0);
            var length1 = array.GetLength(1);
            Allocate(length0, length1, allocator, out this);
            Copy(keys, array, this);
        }

        public NativeKeyArray2D(NativeKeyArray2D<TK, TV> array, Allocator allocator)
        {
            Allocate(array.Capacity0, array.Capacity1, allocator, out this);
            Copy(array, this);
        }

        public int TotalCapacity => Capacity0 * Capacity1;

        public int TotalLength => Length0 * Length1;

        public int Capacity0 { get; private set; }

        public int Capacity1 { get; private set; }

        private int _length0;

        public int Length0
        {
            get => _length0;
            set
            {
                SafetyUtility.CheckWriteAccess(m_Safety);
                SafetyUtility.RequireLengthWithinCapacity(value, Capacity0, 0);
                _length0 = value;
            }
        }

        private int _length1;

        public int Length1
        {
            get => _length1;
            set
            {
                SafetyUtility.CheckWriteAccess(m_Safety);
                SafetyUtility.RequireLengthWithinCapacity(value, Capacity1, 1);
                _length1 = value;
            }
        }

        public int GetIndex(int index0, int index1)
        {
            return (index0 * Length1) + index1;
        }

        public void ReverseIndex(int index, out int index0, out int index1)
        {
            index1 = index % Length1;
            index0 = index / Length1;
        }

        public TK this[int index0]
        {
            get
            {
                SafetyUtility.RequireIndexInBounds(index0, Length0, Capacity0, 0, Length1, Capacity1);

                var index = GetIndex(index0, 0);
                SafetyUtility.CheckElementReadAccess(m_Safety, index, m_MinIndex, m_MaxIndex, TotalCapacity, TotalCapacity);
                return UnsafeUtility.ReadArrayElement<TK>(m_BufferKeys, index);
            }

            [WriteAccessRequired]
            set
            {
                SafetyUtility.RequireIndexInBounds(index0, Length0, Capacity0, 0, Length1, Capacity1);

                var index = GetIndex(index0, 0);
                SafetyUtility.CheckElementWriteAccess(m_Safety, index, m_MinIndex, m_MaxIndex, TotalCapacity, TotalCapacity);
                UnsafeUtility.WriteArrayElement(m_BufferKeys, index, value);
            }
        }

        public TV this[int index0, int index1]
        {
            get
            {
                SafetyUtility.RequireIndexInBounds(index0, Length0, Capacity0, index1, Length1, Capacity1);

                var index = GetIndex(index0, index1);
                SafetyUtility.CheckElementReadAccess(m_Safety, index, m_MinIndex, m_MaxIndex, TotalCapacity, TotalCapacity);
                return UnsafeUtility.ReadArrayElement<TV>(m_Buffer, index);
            }

            [WriteAccessRequired]
            set
            {
                SafetyUtility.RequireIndexInBounds(index0, Length0, Capacity0, index1, Length1, Capacity1);

                var index = GetIndex(index0, index1);
                SafetyUtility.CheckElementWriteAccess(m_Safety, index, m_MinIndex, m_MaxIndex, TotalCapacity, TotalCapacity);
                UnsafeUtility.WriteArrayElement(m_Buffer, index, value);
            }
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private void CheckReadAccess()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            SafetyUtility.CheckReadAccess(m_Safety);
#endif
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private void CheckWriteAccess()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            SafetyUtility.CheckWriteAccess(m_Safety);
#endif
        }

        public bool IsCreated => (IntPtr) m_Buffer != IntPtr.Zero;

        public bool IsDisposed => m_Buffer == null;

        public bool ShouldAllocate()
        {
            return IsDisposed || !IsCreated;
        }

        [WriteAccessRequired]
        public void Dispose()
        {
            SafetyUtility.RequireValidAllocator<NativeKeyArray2D<TK, TV>>(m_AllocatorLabel);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Dispose(ref m_Safety, ref m_DisposeSentinel);
#endif
            Deallocate();
        }

        private void Deallocate()
        {
            UnsafeUtility.Free(m_Buffer, m_AllocatorLabel);
            m_Buffer = null;
            UnsafeUtility.Free(m_BufferKeys, m_AllocatorLabel);
            m_BufferKeys = null;
            _length0 = 0;
            _length1 = 0;
            Capacity0 = 0;
            Capacity1 = 0;
        }

        private static void Allocate(int capacity0, int capacity1, Allocator allocator, out NativeKeyArray2D<TK, TV> array)
        {
            SafetyUtility.RequireValidAllocator<NativeKeyArray2D<TK, TV>>(allocator);
            SafetyUtility.IsUnmanagedAndThrow<NativeKeyArray2D<TK, TV>, TK, TV>();

            var totalCapacity = capacity0 * capacity1;

            if (totalCapacity <= 0)
            {
                throw new InvalidOperationException("Total number of elements must be greater than zero");
            }

            array = new NativeKeyArray2D<TK, TV>
            {
                m_Buffer = UnsafeUtility.Malloc(totalCapacity * UnsafeUtility.SizeOf<TV>(), UnsafeUtility.AlignOf<TV>(), allocator),
                m_BufferKeys = UnsafeUtility.Malloc(capacity0 * UnsafeUtility.SizeOf<TK>(), UnsafeUtility.AlignOf<TK>(), allocator),
                Capacity0 = capacity0,
                Capacity1 = capacity1,
                _length0 = capacity0,
                _length1 = capacity1,
                m_AllocatorLabel = allocator,
                m_Length = totalCapacity,
                m_MinIndex = 0,
                m_MaxIndex = totalCapacity - 1
            };

            DisposeSentinel.Create(out array.m_Safety, out array.m_DisposeSentinel, 1, allocator);
        }

        [WriteAccessRequired]
        public void CopyFrom(TK[] keys, TV[,] array)
        {
            Copy(keys, array, this);
        }

        [WriteAccessRequired]
        public void CopyFrom(NativeKeyArray2D<TK, TV> array)
        {
            Copy(array, this);
        }

        public void CopyTo(TK[] keys, TV[,] array)
        {
            Copy(this, keys);
            Copy(this, array);
        }

        public void CopyTo(TK[] array)
        {
            Copy(this, array);
        }

        public void CopyTo(TV[,] array)
        {
            Copy(this, array);
        }

        public void CopyTo(NativeKeyArray2D<TK, TV> array)
        {
            Copy(this, array);
        }

        public TK[] ToKeyArray()
        {
            var dst = new TK[Length0];
            Copy(this, dst);
            return dst;
        }

        public TV[,] ToArray()
        {
            var dst = new TV[Length0, Length1];
            Copy(this, dst);
            return dst;
        }

        public bool Equals(NativeKeyArray2D<TK, TV> other)
        {
            return (m_Buffer == other.m_Buffer) &&
                   (m_BufferKeys == other.m_BufferKeys) &&
                   (Length0 == other.Length0) &&
                   (Length1 == other.Length1) &&
                   (Capacity0 == other.Capacity0) &&
                   (Capacity1 == other.Capacity1);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return other is NativeKeyArray2D<TK, TV> && Equals((NativeKeyArray2D<TK, TV>) other);
        }

        public override int GetHashCode()
        {
            var result = (int) m_Buffer;
            result = (result * 397) ^ (int) m_BufferKeys;
            result = (result * 397) ^ Capacity0;
            result = (result * 397) ^ Capacity1;
            return result;
        }

        public static bool operator ==(NativeKeyArray2D<TK, TV> a, NativeKeyArray2D<TK, TV> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(NativeKeyArray2D<TK, TV> a, NativeKeyArray2D<TK, TV> b)
        {
            return !a.Equals(b);
        }

        private static void Copy(NativeKeyArray2D<TK, TV> src, NativeKeyArray2D<TK, TV> dest)
        {
            src.CheckReadAccess();
            dest.CheckWriteAccess();

            var providedLengthK = -1;
            var providedLength0 = -1;
            var providedLength1 = -1;

            if ((src.Length0 == dest.Length0) && (src.Length1 == dest.Length1))
            {
                providedLength0 = dest.Length0;
                providedLength1 = dest.Length1;
            }
            else if ((src.Capacity0 == dest.Capacity0) && (src.Capacity1 == dest.Capacity1))
            {
                providedLength0 = dest.Capacity0;
                providedLength1 = dest.Capacity1;
            }

            if ((providedLengthK != providedLength0) || (providedLength0 == -1))
            {
                throw new ArgumentException("Arrays must have the same size");
            }

            for (var index0 = 0; index0 < providedLength0; ++index0)
            {
                dest[index0] = src[index0];

                for (var index1 = 0; index1 < providedLength1; ++index1)
                {
                    dest[index0, index1] = src[index0, index1];
                }
            }
        }

        private static void Copy(TK[] keys, TV[,] src, NativeKeyArray2D<TK, TV> dest)
        {
            dest.CheckWriteAccess();

            var providedLengthK = keys.GetLength(0);
            var providedLength0 = src.GetLength(0);
            var providedLength1 = src.GetLength(1);

            if (providedLengthK == providedLength0)
            {
                if (((providedLength0 == dest.Length0) && (providedLength1 == dest.Length1)) ||
                    ((providedLength0 == dest.Capacity0) && (providedLength1 == dest.Capacity1)))
                {
                    for (var index0 = 0; index0 < providedLength0; ++index0)
                    {
                        dest[index0] = keys[index0];

                        for (var index1 = 0; index1 < providedLength1; ++index1)
                        {
                            dest[index0, index1] = src[index0, index1];
                        }
                    }

                    return;
                }
            }

            throw new ArgumentException("Arrays must have the same size");
        }

        private static void Copy(NativeKeyArray2D<TK, TV> src, TK[] dest)
        {
            src.CheckReadAccess();

            var providedLength0 = dest.GetLength(0);

            if ((providedLength0 == src.Length0) || (providedLength0 == src.Capacity0))
            {
                for (var keyIndex = 0; keyIndex < providedLength0; ++keyIndex)
                {
                    dest[keyIndex] = src[keyIndex];
                }

                return;
            }

            throw new ArgumentException("Arrays must have the same size");
        }

        private static void Copy(NativeKeyArray2D<TK, TV> src, TV[,] dest)
        {
            src.CheckReadAccess();

            var providedLength0 = dest.GetLength(0);
            var providedLength1 = dest.GetLength(1);

            if (((providedLength0 == src.Length0) && (providedLength1 == src.Length1)) ||
                ((providedLength0 == src.Capacity0) && (providedLength1 == src.Capacity1)))
            {
                for (var index0 = 0; index0 < providedLength0; ++index0)
                {
                    for (var index1 = 0; index1 < providedLength1; ++index1)
                    {
                        dest[index0, index1] = src[index0, index1];
                    }
                }

                return;
            }

            throw new ArgumentException("Arrays must have the same size");
        }

        private static void Copy(NativeKeyArray2D<TK, TV> src, TK[] keys, TV[,] dest)
        {
            src.CheckReadAccess();

            var providedLengthK = keys.GetLength(0);
            var providedLength0 = dest.GetLength(0);
            var providedLength1 = dest.GetLength(1);

            if (providedLengthK == providedLength0)
            {
                if (((providedLength0 == src.Length0) && (providedLength1 == src.Length1)) ||
                    ((providedLength0 == src.Capacity0) && (providedLength1 == src.Capacity1)))
                {
                    for (var index0 = 0; index0 < providedLength0; ++index0)
                    {
                        keys[index0] = src[index0];

                        for (var index1 = 0; index1 < providedLength1; ++index1)
                        {
                            dest[index0, index1] = src[index0, index1];
                        }
                    }

                    return;
                }
            }

            throw new ArgumentException("Arrays must have the same size");
        }
    }
}
