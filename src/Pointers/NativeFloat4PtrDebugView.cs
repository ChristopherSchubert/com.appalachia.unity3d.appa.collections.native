namespace collections.native.src.Pointers
{
    /// <summary>
    ///     Provides a debugger view of <see cref="NativeFloat4Ptr" />.
    /// </summary>
    internal sealed class NativeFloat4PtrDebugView
    {
        /// <summary>
        ///     The object to provide a debugger view for
        /// </summary>
        private NativeFloat4Ptr m_Ptr;

        /// <summary>
        ///     Create the debugger view
        /// </summary>
        /// <param name="ptr">
        ///     The object to provide a debugger view for
        /// </param>
        public NativeFloat4PtrDebugView(NativeFloat4Ptr ptr)
        {
            m_Ptr = ptr;
        }

        /// <summary>
        ///     Get the viewed object's value
        /// </summary>
        /// <value>
        ///     The viewed object's value
        /// </value>
        public float4 Value => m_Ptr.Value;
    }
}