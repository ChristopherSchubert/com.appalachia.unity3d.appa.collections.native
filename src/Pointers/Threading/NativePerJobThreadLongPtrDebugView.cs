namespace collections.native.src.Pointers.Threading
{
    /// <summary>
    ///     Provides a debugger view of <see cref="NativePerJobThreadLongPtr" />.
    /// </summary>
    internal sealed class NativePerJobThreadLongPtrDebugView
    {
        /// <summary>
        ///     The object to provide a debugger view for
        /// </summary>
        private NativeLongPtr m_Ptr;

        /// <summary>
        ///     Create the debugger view
        /// </summary>
        /// <param name="ptr">
        ///     The object to provide a debugger view for
        /// </param>
        public NativePerJobThreadLongPtrDebugView(NativeLongPtr ptr)
        {
            m_Ptr = ptr;
        }

        /// <summary>
        ///     Get the viewed object's value
        /// </summary>
        /// <value>
        ///     The viewed object's value
        /// </value>
        public long Value => m_Ptr.Value;
    }
}