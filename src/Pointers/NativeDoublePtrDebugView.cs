namespace collections.native.src.Pointers
{
    /// <summary>
    ///     Provides a debugger view of <see cref="NativeDoublePtr" />.
    /// </summary>
    internal sealed class NativeDoublePtrDebugView
    {
        /// <summary>
        ///     The object to provide a debugger view for
        /// </summary>
        private NativeDoublePtr m_Ptr;

        /// <summary>
        ///     Create the debugger view
        /// </summary>
        /// <param name="ptr">
        ///     The object to provide a debugger view for
        /// </param>
        public NativeDoublePtrDebugView(NativeDoublePtr ptr)
        {
            m_Ptr = ptr;
        }

        /// <summary>
        ///     Get the viewed object's value
        /// </summary>
        /// <value>
        ///     The viewed object's value
        /// </value>
        public double Value => m_Ptr.Value;
    }
}