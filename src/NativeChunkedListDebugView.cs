namespace collections.native.src
{
    /// <summary>
    ///     Provides a debugger view of <see cref="NativeChunkedList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of elements in the list
    /// </typeparam>
    internal sealed class NativeChunkedListDebugView<T>
#if CSHARP_7_3_OR_NEWER
        where T : unmanaged
#else
		where T : struct
#endif
    {
        /// <summary>
        ///     List to view
        /// </summary>
        private NativeChunkedList<T> m_List;

        /// <summary>
        ///     Create the view for a given list
        /// </summary>
        /// <param name="list">
        ///     List to view
        /// </param>
        public NativeChunkedListDebugView(NativeChunkedList<T> list)
        {
            m_List = list;
        }

        /// <summary>
        ///     Get a managed array version of the list's elements to be viewed in
        ///     the debugger.
        /// </summary>
        public T[] Items => m_List.ToArray();
    }
}