using System;

namespace collections.native.src
{
    /// <summary>
    ///     Provides a debugger view of <see cref="SharedDisposable{T}" />.
    /// </summary>
    /// <typeparam name="TDisposable">
    ///     Type of disposable that is shared.
    /// </typeparam>
    internal sealed class SharedDisposableDebugView<TDisposable>
        where TDisposable : IDisposable
    {
        /// <summary>
        ///     The object to provide a debugger view for
        /// </summary>
        private SharedDisposable<TDisposable> m_Ptr;

        /// <summary>
        ///     Create the debugger view
        /// </summary>
        /// <param name="ptr">
        ///     The object to provide a debugger view for
        /// </param>
        public SharedDisposableDebugView(SharedDisposable<TDisposable> ptr)
        {
            m_Ptr = ptr;
        }

        /// <summary>
        ///     Get the viewed object's disposable
        /// </summary>
        /// <value>
        ///     The viewed object's disposable
        /// </value>
        public TDisposable Disposable => m_Ptr.Value;
    }
}