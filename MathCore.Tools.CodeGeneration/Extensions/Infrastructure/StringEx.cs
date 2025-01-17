﻿namespace MathCore.Tools.CodeGeneration.Extensions.Infrastructure;

public static class StringEx
{
    public static string NewLine { get; set; } = "\r\n";

    public static string TrimEnd(this string str, string trim, StringComparison Comparison = StringComparison.Ordinal)
    {
        if (!str.EndsWith(trim, Comparison) || string.IsNullOrEmpty(trim)) return str;
        if (str == trim) return string.Empty;

        return str.Substring(0, str.Length - trim.Length);
    }

    public static IEnumerable<string> EnumLines(this string str)
    {
        var reader = new StringReader(str);
        while (reader.ReadLine() is { } line)
            yield return line;
    }

    public static IEnumerable<T> EnumLines<T>(this string str, Func<string, T> selector) => str.EnumLines().Select(selector);

    public static IEnumerable<T> EnumLines<T>(this string str, Func<string, int, T> selector) => str.EnumLines().Select(selector);

    public static string JoinString(this IEnumerable<string> strings, string separator) => string.Join(separator, strings);

    public static string JoinStringLN(this IEnumerable<string> strings) => string.Join(NewLine, strings);
}
