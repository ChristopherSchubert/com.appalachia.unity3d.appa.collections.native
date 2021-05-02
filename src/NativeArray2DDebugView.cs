namespace collections.native.src
{
    internal sealed class NativeArray2DDebugView<T>
        where T : struct
    {
        private readonly NativeArray2D<T> _mArray;

        public NativeArray2DDebugView(NativeArray2D<T> array)
        {
            _mArray = array;
        }

        public T[,] Items => _mArray.ToArray();
    }
}