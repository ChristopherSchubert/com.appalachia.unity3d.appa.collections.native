using System;
using System.Collections.Generic;

namespace collections.native.src
{
    /// <summary>
    ///     Provides a debugger view of <see cref="NativeHashSet{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of elements in the set
    /// </typeparam>
    internal sealed class NativeHashSetDebugView<T>
#if CSHARP_7_3_OR_NEWER
        where T : unmanaged
#else
		where T : struct
#endif
        , IEquatable<T>
    {
        /// <summary>
        ///     Set to provide a view of
        /// </summary>
        private readonly NativeHashSet<T> m_Set;

        /// <summary>
        ///     Create the debug view for a given set
        /// </summary>
        /// <param name="set">
        ///     Set to provide a view of
        /// </param>
        public NativeHashSetDebugView(NativeHashSet<T> set)
        {
            m_Set = set;
        }

        /// <summary>
        ///     Get all the items in the set.
        ///     This operation requires read access.
        /// </summary>
        public List<T> Items
        {
            get
            {
                using (var array = m_Set.ToNativeArray())
                {
                    var result = new List<T>(array.Length);
                    foreach (var item in array)
                    {
                        if (m_Set.Contains(item))
                        {
                            result.Add(item);
                        }
                    }

                    return result;
                }
            }
        }
    }
}