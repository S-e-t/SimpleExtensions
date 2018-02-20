using System;
using System.Globalization;

namespace SimpleExtensions {
    /// <summary>
    /// Extensions for parse string value
    /// </summary>
    static public class StringExtention {
        #region ----------------------------- Parse --------------------------------
        /// <summary>
        /// Conversion to integer
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">the default value (if the number could not be specified)</param>
        /// <returns>integer</returns>
        static public int ToInt32(this string val, int defaultValue = 0) => Int32.TryParse(val, out int res) ? res : defaultValue;

        /// <summary>
        /// Conversion to long integer
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">the default value (if the number could not be specified)</param>
        /// <returns>long</returns>
        static public long ToInt64(this string val, long defaultValue = 0) => long.TryParse(val, out long res) ? res : defaultValue;

        /// <summary>
        /// Conversion to double
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">the default value (if the number could not be specified)</param>
        /// <returns>double</returns>
        static public double ToDouble(this string val, double defaultValue = 0) =>
            double.TryParse(val?.Replace(',', '.')
                , NumberStyles.Any
                , CultureInfo.InvariantCulture
                , out double res) ? res : defaultValue;
        
        /// <summary>
        /// Conversion to float
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">the default value (if the number could not be specified)</param>
        /// <returns>float</returns>
        static public float ToFloat(this string val, float defaultValue = 0) =>
            float.TryParse(val?.Replace(',', '.')
                , NumberStyles.Any
                , CultureInfo.InvariantCulture
                , out float res) ? res : defaultValue;
 
        /// <summary>
        /// Conversion to decimal
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">the default value (if the number could not be specified)</param>
        /// <returns>decimal</returns>
        static public decimal ToDecimal(this string val, decimal defaultValue = 0) => 
            decimal.TryParse(val?.Replace(',', '.')
                , NumberStyles.Number | NumberStyles.AllowCurrencySymbol
                , CultureInfo.InvariantCulture
                , out decimal res) ? res : defaultValue;


        /// <summary>
        /// Conversion to item of Enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">default value</param>
        /// <param name="ignoreCase">Ignore Case (default true)</param>
        /// <returns>item of Enum</returns>
        static public TEnum ToEnum<TEnum>(this string val, TEnum defaultValue = default(TEnum), bool ignoreCase = true) where TEnum : struct =>
            Enum.TryParse(val, ignoreCase, out TEnum res) && Enum.IsDefined(typeof(TEnum), res) ? res : defaultValue;

        /// <summary>
        /// Conversion to Guid
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <returns>Guid</returns>
        static public Guid ToGuid(this string val) => Guid.TryParse(val, out var res) ? res : Guid.Empty;

        /// <summary>
        /// Conversion to DateTime
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="defaultValue">the default value (if the DateTime could not be specified)</param>
        /// <param name="timeZoneShift">time Zone Shift in hours</param>
        /// <returns>DateTime</returns>
        static public DateTime ToDateTime(this string val, DateTime defaultValue = default(DateTime), int timeZoneShift = 0)=>
             DateTime.TryParse(val, out DateTime res) ? res.AddHours(timeZoneShift) : defaultValue;

        /// <summary>
        /// Conversion to DateTime
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="format">A format specifier that defines the required format of value</param>
        /// <param name="defaultValue">the default value (if the DateTime could not be specified)</param>
        /// <param name="timeZoneShift">time Zone Shift in hours</param>
        /// <returns>DateTime</returns>
        static public DateTime ToDateTime(this string val, string format, DateTime defaultValue = default(DateTime), int timeZoneShift = 0) =>
             DateTime.TryParseExact(val, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime res) ? res.AddHours(timeZoneShift) : defaultValue;

        /// <summary>
        ///Conversion to boolean
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <returns>Boolean</returns>
        public static bool ToBool(this string val) {
            if (string.IsNullOrWhiteSpace(val))
                return false;
            switch (val?.Trim().ToLower()) {
                case "yes":
                case "true":
                    return true;
                case "no":
                case "false":
                    return false;
            }
            return val.ToDecimal() > 0;
        }

        /// <summary>
        /// Conversion to byte[] by default encoding
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <returns>byte[]</returns>
        static public byte[] ToByteArray(this string val) => val.ToByteArray(System.Text.Encoding.Default);

        /// <summary>
        /// Conversion to byte[] by UTF8 encoding
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <returns>byte[]</returns>
        static public byte[] ToByteArrayUtf8(this string val) => val.ToByteArray(System.Text.Encoding.UTF8);

        /// <summary>
        /// Conversion to byte[]
        /// </summary>
        /// <param name="val">string to be converted</param>
        /// <param name="encoding">encoding for convert</param>
        /// <returns>byte[]</returns>
        static public byte[] ToByteArray(this string val, System.Text.Encoding encoding) => encoding.GetBytes(val);

        /// <summary>
        /// Conversion byte array to string by default encoding
        /// </summary>
        /// <param name="val">byte array to be converted</param>
        /// <returns>string</returns>
        static public string ByteToString(this byte[] val) => val.ByteToString(System.Text.Encoding.Default);

        /// <summary>
        /// Conversion byte array to string by Utf8 encoding
        /// </summary>
        /// <param name="val">byte array to be converted</param>
        /// <returns>string</returns>
        static public string ByteToStringUtf8(this byte[] val) => val.ByteToString(System.Text.Encoding.UTF8);

        /// <summary>
        /// Conversion byte array to string
        /// </summary>
        /// <param name="val">byte array to be converted</param>
        /// <param name="encoding">encoding for convert</param>
        /// <returns>string</returns>
        static public string ByteToString(this byte[] val, System.Text.Encoding encoding) => encoding.GetString(val);
        
        /// <summary>
        /// Returns a value indicating whether this instance and a specified System.Double object 
        /// represent the same value with a deviation of not more than epsilon. 
        /// </summary>
        /// <param name="first">first value</param>
        /// <param name="second">second value</param>
        /// <returns>true, if is equal; otherwise, false.</returns>
        static public bool EqualsWithEpsilon(this double first, double second = 0) => Math.Abs(first - second) < double.Epsilon;

        /// <summary>
        /// Returns a value indicating whether this instance and a specified System.Float object 
        /// represent the same value with a deviation of not more than epsilon. 
        /// </summary>
        /// <param name="first">first value</param>
        /// <param name="second">second value</param>
        /// <returns>true, if is equal; otherwise, false.</returns>
        static public bool EqualsWithEpsilon(this float first, float second = 0) => Math.Abs(first - second) < float.Epsilon;

        #endregion ----------------------------- Parse --------------------------------

    }
}
