using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleExtensions {
    /// <summary>
    /// Extensions for build DataTable
    /// </summary>
    public static class DataTableExtensions {
        /// <summary>
        /// Add Column in DataTable
        /// </summary>
        /// <typeparam name="T">new column type</typeparam>
        /// <param name="dt">DataTable</param>
        /// <param name="name">new column name</param>
        /// <param name="allowDBNull">column allowDBNull</param>
        /// <returns>DataTable</returns>
        public static DataTable ColumnAdd<T>(this DataTable dt, string name, bool allowDBNull = false) {
            dt.Columns.Add(new DataColumn(name, typeof(T)) { AllowDBNull = allowDBNull });            
            return dt;
        }
        /// <summary>
        /// Filling the DataTable from IEnumerable
        /// </summary>
        /// <typeparam name="T">type of item from enumerable/typeparam>
        /// <param name="dt">DataTable</param>
        /// <param name="source">items of data</param>
        /// <param name="rowGen">generator for DataRow </param>
        /// <returns></returns>
        public static DataTable Fill<T>(this DataTable dt, IEnumerable<T> source, Func<T, object[]> rowGen = null) {
            if (dt == null)
                return dt;
            source?.ForEach(t => dt.Rows.Add(rowGen?.Invoke(t) ?? new object[] { t }));
            return dt;
        }

    }
}