namespace Veises.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Escape string format.
        /// </summary>
        private const string EscapeString = "\'{0}\'";

        /// <summary>
        ///     Return escaped string.
        /// </summary>
        /// <param name="source">Source string value.</param>
        /// <returns>Escaped string.</returns>
        public static string Escaped(this string source)
        {
            return string.Format(EscapeString, source);
        }

        /// <summary>
        ///     Return escaped object string representation.
        /// </summary>
        /// <param name="source">Source object.</param>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Escaped object string representation.</returns>
        public static string Escaped<T>(this T source)
        {
            return Escaped(source?.ToString());
        }
    }
}