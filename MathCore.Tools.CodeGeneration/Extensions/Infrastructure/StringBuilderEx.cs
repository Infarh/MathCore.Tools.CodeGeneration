using System.Text;

namespace MathCore.Tools.CodeGeneration.Extensions.Infrastructure;

public static class StringBuilderEx
{
    public static StringBuilder Append(this StringBuilder builder, string Format, object arg0) => builder.AppendFormat(Format, arg0);

    public static StringBuilder Append(this StringBuilder builder, string Format, object arg0, object arg1) => builder.AppendFormat(Format, arg0, arg1);

    public static StringBuilder Append(this StringBuilder builder, string Format, object arg0, object arg1, object arg2) => builder.AppendFormat(Format, arg0, arg1, arg2);

    public static StringBuilder Append(this StringBuilder builder, string Format, params object[] args) => builder.AppendFormat(Format, args);

    public static StringBuilder AppendLine(this StringBuilder builder, string Format, object arg0) => builder.AppendFormat(Format, arg0);

    public static StringBuilder AppendLine(this StringBuilder builder, string Format, object arg0, object arg1) => builder.AppendFormat(Format, arg0, arg1);

    public static StringBuilder AppendLine(this StringBuilder builder, string Format, object arg0, object arg1, object arg2) => builder.AppendFormat(Format, arg0, arg1, arg2);

    public static StringBuilder AppendLine(this StringBuilder builder, string Format, params object[] args) => builder.AppendFormat(Format, args);

    public static StringBuilder LN(this StringBuilder builder) => builder.AppendLine();
    
    public static StringBuilder LN(this StringBuilder builder, string str) => builder.AppendLine(str);
    
    public static StringBuilder LN(this StringBuilder builder, string Format, object arg0) => builder.Append(Format, arg0).LN();
    
    public static StringBuilder LN(this StringBuilder builder, string Format, object arg0, object arg1) => builder.Append(Format, arg0, arg1).LN();
    
    public static StringBuilder LN(this StringBuilder builder, string Format, object arg0, object arg1, object arg2) => builder.Append(Format, arg0, arg1, arg2).LN();
    
    public static StringBuilder LN(this StringBuilder builder, string Format, params object[] args) => builder.Append(Format, args).LN();

    public static string ToStringWithLineNumbers(this StringBuilder builder, char Separator = '|') =>
        builder
            .ToString()
            .Split([StringEx.NewLine], StringSplitOptions.None)
            .Select((s, i) => $"{i + 1,3}{Separator}{s}")
            .JoinStringLN();
}
