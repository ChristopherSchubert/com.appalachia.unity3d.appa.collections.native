namespace collections.native.src
{
    internal sealed class NativeArray3DDebugView<T>
        where T : struct
    {
        private readonly NativeArray3D<T> _mArray;

        public NativeArray3DDebugView(NativeArray3D<T> array)
        {
            _mArray = array;
        }

        public T[,,] Items => _mArray.ToArray();
    }
}