namespace Veises.Common.Extensions
{
    public static class StringExtensions
    {
        public const char StringEscapeChar = '\'';
        
        public static string Escaped(this string source)
        {
            return $"{StringEscapeChar}{source}{StringEscapeChar}";
        }

        public static string Escaped<T>(this T source)
        {
            return Escaped(source?.ToString());
        }
    }
}