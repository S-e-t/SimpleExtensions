using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleExtensions {

    /// <summary>
    /// Extensions for IEnumerable
    /// </summary>
	public static class IEnumerableExtensions {

        /// <summary>
        /// The ForEach statement repeats a group of embedded statements for each element in IEnumerable<T>
        /// </summary>
        /// <typeparam name="T">type element of sequence</typeparam>
        /// <param name="source">enumerable sequence</param>
        /// <param name="func">action for elements of sequence</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> func) {
            if (func == null)
                return;
            foreach (var item in source)
                func.Invoke(item);
        }

        /// <summary>
        /// The async ForEach statement repeats a group of embedded statements for each element in IEnumerable<T>
        /// </summary>
        /// <typeparam name="T">type element of sequence</typeparam>
        /// <param name="source">enumerable sequence</param>
        /// <param name="func">action for elements of sequence</param>
        /// <returns></returns>
        public static async Task ForEach<T>(this IEnumerable<T> source, Func<T, Task> func) {
            if (func == null)
                return;
            foreach (var item in source)
                await func.Invoke(item);
        }

        /// <summary>
        /// Converting IEnumerable<T> to IDictionary<TSource, T>
        /// </summary>
        /// <typeparam name="TSource">key type</typeparam>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="source">transformable sequence</param>
        /// <param name="keyGen">key generator</param>
        /// <returns></returns>
        public static IDictionary<TSource, T> ToDictionaryTry<TSource, T>(this IEnumerable<T> source,
                                                                         Func<T, TSource> keyGen) {
            return source.ToDictionaryTry(keyGen, i => i);
        }

        /// <summary>
        /// Converting IEnumerable<T> to IDictionary<TSource, T>
        /// </summary>
        /// <typeparam name="TSource">key type</typeparam>
        /// <typeparam name="TElement">value type</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">transformable sequence</param>
        /// <param name="keyGen">key generator</param>
        /// <param name="valueGen">value generator</param>
        /// <returns></returns>
        public static IDictionary<TSource, TElement> ToDictionaryTry<TSource, TElement, T>(this IEnumerable<T> source,
                                                                         Func<T, TSource> keyGen, Func<T, TElement> valueGen) {
            return source.Aggregate(new Dictionary<TSource, TElement>(), (res, item) => {
                res[keyGen(item)] = valueGen(item);
                return res;
            });
        }

        /// <summary>
        /// Converting IEnumerable<T> to IDictionary<TSource, IEnumerable<T>> with group by TSource
        /// </summary>
        /// <typeparam name="TSource">key type</typeparam>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="source">transformable sequence</param>
        /// <param name="keyGen">key generator</param>
        /// <returns></returns>
        public static IDictionary<TSource, IEnumerable<T>> GroupByToDictionary<TSource, T>(this IEnumerable<T> source,
                                                                         Func<T, TSource> keyGen) {
            return source.GroupByToDictionary(keyGen, i => i);
        }

        /// <summary>
        /// Converting IEnumerable<T> to IDictionary<TSource, IEnumerable<T>> with group by TSource
        /// </summary>
        /// <typeparam name="TSource">key type</typeparam>
        /// /// <typeparam name="TElement">value type</typeparam>
        /// <typeparam name="T">type element of sequence</typeparam>
        /// <param name="source">transformable sequence</param>
        /// <param name="keyGen">key generator</param>
        /// <param name="valueGen">value generator from TElement</param>
        /// <returns></returns>
        public static IDictionary<TSource, IEnumerable<TElement>> GroupByToDictionary<TSource, TElement, T>(this IEnumerable<T> source,
                                                                         Func<T, TSource> keyGen, Func<T, TElement> valueGen) {
            return source.Aggregate(new Dictionary<TSource, IEnumerable<TElement>>(),
                (res, item) => {
                    var key = keyGen(item);
                    var val = valueGen(item);
                    if (res.ContainsKey(key))
                        (res[key] as ICollection<TElement>)?.Add(val);
                    else
                        res[key] = new List<TElement> { val };
                    return res;
                });
        }        
    }
}