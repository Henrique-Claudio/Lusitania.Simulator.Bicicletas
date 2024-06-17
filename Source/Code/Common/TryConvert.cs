using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Globalization;

    /// <summary>
    /// Supports methods that try to convert a data type to another, but return null if the conversion fails, instead of throwing an exception.
    /// </summary>
    ///
    public static class TryConvert
    {
        public static T? ToEnum<T>(string iSource, bool iIgnoreCase)
            where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), iSource, iIgnoreCase);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to convert a String to a Nullable Decimal.
        /// </summary>
        /// <param name="iText">The string to be converted.</param>
        /// <returns>The value parsed from the String, if possible, otherwise null.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static decimal? ToDecimal(object iTarget)
        {
            if (iTarget == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return Convert.ToDecimal(iTarget, CultureInfo.CurrentCulture);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Tries to convert a String to Double.
        /// </summary>
        /// <param name="iText">The String to be cnverted.</param>
        /// <returns>The value parsed from the String, if possible, otherwise null.</returns>
        public static double? ToDouble(string iText)
        {
            try
            {
                return Convert.ToDouble(iText, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException ) {}
            catch (ArgumentException) { }
            catch (FormatException ) { }

            return null;
        }

        /// <summary>
        /// Tries to convert a String to a Boolean.
        /// </summary>
        /// <param name="iText">The String to be converted.</param>
        /// <returns>The value parsed from the String, if possible, otherwise null.</returns>
        public static bool? ToBoolean(object source)
        {
            try
            {
                return Convert.ToBoolean(source, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException) {}
            catch (ArgumentException ) { }
            catch (FormatException ) {  }
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to convert an Object to an Int32.
        /// </summary>
        /// <param name="iText">The String to be converted.</param>
        /// <returns>The value parsed from the String, if possible, otherwise null.</returns>
        public static int? ToInt32(object source)
        {
            try
            {
                return Convert.ToInt32(source, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException ) { }
            catch (ArgumentException ) {  }
            catch (FormatException ) {  }

            return null;
        }

        /// <summary>
        /// Tries to convert any object to a string.
        /// </summary>
        /// <param name="iSource">The object to be converted.</param>
        /// <returns>The string that represents the object if possible, or null.</returns>
        public static string ToString(object iSource)
        {
            try
            {
                return Convert.ToString(iSource, CultureInfo.CurrentCulture);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to convert a nullable value to a string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToString<T>(Nullable<T> source) where T : struct
        {
            if (source.HasValue)
            {
                return source.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to convert any object to a DateTime.
        /// </summary>
        /// <param name="iSource">The object to be converted.</param>
        /// <returns>The DateTime that represents the object, or null.</returns>
        public static DateTime? ToDateTime(object iSource)
        {
            if (iSource == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return Convert.ToDateTime(iSource);
                }
                catch
                {
                    return null;
                }
            }
        }
    }