#region

using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

#endregion

namespace collections.native.src
{
    [DebuggerDisplay("Length0 = {Length0}, Length1 = {Length1}, Capacity0 = {Capacity0}, Capacity1 = {Capacity1}")]
    [DebuggerTypeProxy(typeof(NativeArray2DDebugView<>))]
    [NativeContainer]
    [NativeContainerSupportsDeallocateOnJobCompletion]
    [NativeContainerSupportsMinMaxWriteRestriction]
    public unsafe struct NativeArray2D<T> : IDisposable, IEquatable<NativeArray2D<T>>
        where T : struct
    {
        [NativeDisableUnsafePtrRestriction]
        private void* m_Buffer;

        internal int m_Length;
        internal int m_MinIndex;
        internal int m_MaxIndex;

#if ENABLE_UNITY_COLLECTIONS_CHECKS

        private AtomicSafetyHandle m_Safety;

        [NativeSetClassTypeToNullOnSchedule]
        private DisposeSentinel m_DisposeSentinel;
#endif

        internal Allocator m_AllocatorLabel;

        public NativeArray2D(int length0, int length1, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
        {
            Allocate(length0, length1, allocator, out this);
            if ((options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory)
            {
                UnsafeUtility.MemClear(m_Buffer, TotalLength * (long) UnsafeUtility.SizeOf<T>());
            }
        }

        public NativeArray2D(T[,] array, Allocator allocator)
        {
            var length0 = array.GetLength(0);
            var length1 = array.GetLength(1);
            Allocate(length0, length1, allocator, out this);
            Copy(array, this);
        }

        public NativeArray2D(NativeArray2D<T> array, Allocator allocator)
        {
            Allocate(array.Capacity0, array.Capacity1, allocator, out this);
            Copy(array, this);
        }
        
        public NativeArray2D(T[] array, int2 dimensions, Allocator allocator)
        {
            Allocate(dimensions.x, dimensions.y, allocator, out this);
            CopyFromFlat(array);
        }
        
        public int TotalCapacity => Capacity0 * Capacity1;

        public int TotalLength => Length0 * Length1;

        public int2 Capacity
        {
            get => new int2(Capacity0, Capacity1);
        }
        
        public int2 Length
        {
            get => new int2(_length0, _length1);
            set
            {
                CheckWriteAccess();
                SafetyUtility.RequireLengthWithinCapacity(value.x, Capacity0, 0);
                Length0 = value.x;
                SafetyUtility.RequireLengthWithinCapacity(value.y, Capacity1, 1);
                Length1 = value.y;
            }
        }
        
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

        public T this[int index0, int index1]
        {
            get
            {
                SafetyUtility.RequireIndexInBounds(index0, Length0, Capacity0, index1, Length1, Capacity1);

                var index = GetIndex(index0, index1);
                SafetyUtility.CheckElementReadAccess(m_Safety, index, m_MinIndex, m_MaxIndex, TotalCapacity, TotalCapacity);
                return UnsafeUtility.ReadArrayElement<T>(m_Buffer, index);
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
            SafetyUtility.RequireValidAllocator<NativeArray2D<T>>(m_AllocatorLabel);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Dispose(ref m_Safety, ref m_DisposeSentinel);
#endif
            Deallocate();
        }

        private void Deallocate()
        {
            UnsafeUtility.Free(m_Buffer, m_AllocatorLabel);
            m_Buffer = null;
            _length0 = 0;
            _length1 = 0;
            Capacity0 = 0;
            Capacity1 = 0;
        }

        private static void Allocate(int capacity0, int capacity1, Allocator allocator, out NativeArray2D<T> array)
        {
            SafetyUtility.RequireValidAllocator<NativeArray2D<T>>(allocator);
            SafetyUtility.IsUnmanagedAndThrow<NativeArray2D<T>, T>();

            var totalCapacity = capacity0 * capacity1;

            if (totalCapacity <= 0)
            {
                throw new InvalidOperationException("Total number of elements must be greater than zero");
            }

            array = new NativeArray2D<T>
            {
                m_Buffer = UnsafeUtility.Malloc(totalCapacity * UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), allocator),
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
        public void CopyFrom(T[,] array)
        {
            Copy(array, this);
        }

        [WriteAccessRequired]
        public void CopyFrom(NativeArray2D<T> array)
        {
            Copy(array, this);
        }

        public void CopyTo(T[,] array)
        {
            Copy(this, array);
        }

        public void CopyTo(NativeArray2D<T> array)
        {
            Copy(this, array);
        }

        public T[,] ToArray()
        {
            var dst = new T[Length0, Length1];
            Copy(this, dst);
            return dst;
        }

        public bool Equals(NativeArray2D<T> other)
        {
            return (m_Buffer == other.m_Buffer) &&
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

            return other is NativeArray2D<T> && Equals((NativeArray2D<T>) other);
        }

        public override int GetHashCode()
        {
            var result = (int) m_Buffer;
            result = (result * 397) ^ Capacity0;
            result = (result * 397) ^ Capacity1;
            return result;
        }

        public static bool operator ==(NativeArray2D<T> a, NativeArray2D<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(NativeArray2D<T> a, NativeArray2D<T> b)
        {
            return !a.Equals(b);
        }

        private static void Copy(NativeArray2D<T> src, NativeArray2D<T> dest)
        {
            src.CheckReadAccess();
            dest.CheckWriteAccess();

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

            if (providedLength0 == -1)
            {
                throw new ArgumentException("Arrays must have the same size");
            }

            for (var index0 = 0; index0 < providedLength0; ++index0)
            {
                for (var index1 = 0; index1 < providedLength1; ++index1)
                {
                    dest[index0, index1] = src[index0, index1];
                }
            }
        }

        private static void Copy(T[,] src, NativeArray2D<T> dest)
        {
            dest.CheckWriteAccess();

            var providedLength0 = src.GetLength(0);
            var providedLength1 = src.GetLength(1);

            if (((providedLength0 == dest.Length0) && (providedLength1 == dest.Length1)) ||
                ((providedLength0 == dest.Capacity0) && (providedLength1 == dest.Capacity1)))
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

        private static void Copy(NativeArray2D<T> src, T[,] dest)
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
        
        

        public T[] ToArrayFlat(out int2 dimensions)
        {
            var result = new T[TotalCapacity];

            CopyToFlat(result, out dimensions);

            return result;
        }
        
        public void CopyToFlat(T[] dest, out int2 dimensions)
        {
            CheckReadAccess();

            var destinationLength = dest.Length;
            var targetLength = TotalCapacity;
            
            if (destinationLength != targetLength)
            {
                throw new ArgumentException("Destination array length must match the TotalCapacity of the source.");
            }

            dimensions = Length;
            
            for (var index0 = 0; index0 < Length0; ++index0)
            {
                for (var index1 = 0; index1 < Length1; ++index1)
                {
                    var index = GetIndex(index0, index1);
                    
                    dest[index] = this[index0, index1];
                }
            }
        }
        
        [WriteAccessRequired]
        public void CopyFromFlat(T[] src)
        {
            CheckWriteAccess();

            var sourceLength = src.Length;
            var thisLength = TotalCapacity;
            
            if (sourceLength != thisLength)
            {
                throw new ArgumentException("Array length must match the TotalCapacity of this array.");
            }

            for (var i = 0; i < src.Length; i++)
            {
                ReverseIndex(i, out var ix, out var iy);

                this[ix, iy] = src[i];
            }
        }
    }
}
