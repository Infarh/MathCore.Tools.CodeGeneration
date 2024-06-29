using System.Text;

namespace MathCore.Tools.CodeGeneration.Extensions.Infrastructure;

public static class StringBuilderEx
{
    [StringFormatMethod(nameof(Format))]
    public static StringBuilder Append(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0) => builder.AppendFormat(Format, arg0);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder Append(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)]string Format, object arg0, object arg1) => builder.AppendFormat(Format, arg0, arg1);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder Append(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0, object arg1, object arg2) => builder.AppendFormat(Format, arg0, arg1, arg2);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder Append(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, params object[] args) => builder.AppendFormat(Format, args);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder AppendLine(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0) => builder.AppendFormat(Format, arg0);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder AppendLine(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0, object arg1) => builder.AppendFormat(Format, arg0, arg1);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder AppendLine(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0, object arg1, object arg2) => builder.AppendFormat(Format, arg0, arg1, arg2);

    [StringFormatMethod(nameof(Format))]
    public static StringBuilder AppendLine(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, params object[] args) => builder.AppendFormat(Format, args);

    public static StringBuilder LN(this StringBuilder builder) => builder.AppendLine();
    
    public static StringBuilder LN(this StringBuilder builder, string str) => builder.AppendLine(str);
    
    [StringFormatMethod(nameof(Format))]
    public static StringBuilder LN(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0) => builder.Append(Format, arg0).LN();
    
    [StringFormatMethod(nameof(Format))]
    public static StringBuilder LN(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0, object arg1) => builder.Append(Format, arg0, arg1).LN();
    
    [StringFormatMethod(nameof(Format))]
    public static StringBuilder LN(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, object arg0, object arg1, object arg2) => builder.Append(Format, arg0, arg1, arg2).LN();
    
    [StringFormatMethod(nameof(Format))]
    public static StringBuilder LN(this StringBuilder builder, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string Format, params object[] args) => builder.Append(Format, args).LN();

    public static string ToStringWithLineNumbers(this StringBuilder builder, char Separator = '|') =>
        builder
            .ToString()
            .Split([StringEx.NewLine], StringSplitOptions.None)
            .Select((s, i) => $"{i + 1,3}{Separator}{s}")
            .JoinStringLN();

    public static string ToNumeratedLinesString(this StringBuilder builder) => builder.EnumLines(static (s, i) => $"{i + 1,3}|{s}").JoinStringLN();

    public static IEnumerable<string> EnumLines(this StringBuilder builder) => builder.ToString().EnumLines();

    public static IEnumerable<T> EnumLines<T>(this StringBuilder builder, Func<string, T> selector) => builder.ToString().EnumLines(selector);

    public static IEnumerable<T> EnumLines<T>(this StringBuilder builder, Func<string, int, T> selector) => builder.ToString().EnumLines(selector);

    public static StringWriter CreateWriter(this StringBuilder builder) => new(builder);
}
