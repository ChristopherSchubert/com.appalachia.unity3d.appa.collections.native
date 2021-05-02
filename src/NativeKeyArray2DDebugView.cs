namespace collections.native.src
{
    internal sealed class NativeKeyArray2DDebugView<TK, TV>
        where TK : struct
        where TV : struct
    {
        private readonly NativeKeyArray2D<TK, TV> _mArray;

        public NativeKeyArray2DDebugView(NativeKeyArray2D<TK, TV> array)
        {
            _mArray = array;
        }

        public TV[,] Items => _mArray.ToArray();

        public TK[] Keys => _mArray.ToKeyArray();
    }
}