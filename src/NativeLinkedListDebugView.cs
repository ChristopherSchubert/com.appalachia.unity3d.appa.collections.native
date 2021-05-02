namespace collections.native.src
{
    /// <summary>
    ///     Provides a debugger view of <see cref="NativeLinkedList{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of nodes in the list
    /// </typeparam>
    internal sealed class NativeLinkedListDebugView<T>
#if CSHARP_7_3_OR_NEWER
        where T : unmanaged
#else
		where T : struct
#endif
    {
        /// <summary>
        ///     List to view
        /// </summary>
        private NativeLinkedList<T> m_List;

        /// <summary>
        ///     Create the view for a given list
        /// </summary>
        /// <param name="list">
        ///     List to view
        /// </param>
        public NativeLinkedListDebugView(NativeLinkedList<T> list)
        {
            m_List = list;
        }

        /// <summary>
        ///     Get a managed array version of the list's nodes to be viewed in the
        ///     debugger.
        /// </summary>
        public T[] Items => m_List.ToArray();
    }
}