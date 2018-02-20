using System.Collections.Generic;

namespace SimpleExtensions {
    /// <summary>
    /// Extensions for IDictionary
    /// </summary>
    public static class IDictionaryExtensions {


        /// <summary>
        /// Return either an element by key or the default value
        /// </summary>
        /// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="source">source dictionary</param>
        /// <param name="key">key</param>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key) {
            return source.TryGetValue(key, default(TValue));
        }

        /// <summary>
		/// Return either an element by key or the default value
		/// </summary>
		/// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="source">source dictionary</param>
        /// <param name="key">key</param>
		/// <param name="default">default value</param>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue @default) {
            return source.ContainsKey(key) ? source[key] : @default;
        }

        /// <summary>
        /// Add a value to the dictionary by key, if the key is available
        /// </summary>
        /// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="source">source dictionary</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static IDictionary<TKey, TValue> AddIfNew<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value) {
            if (!source.ContainsKey(key)) source.Add(key, value);
            return source;
        }
    }
}