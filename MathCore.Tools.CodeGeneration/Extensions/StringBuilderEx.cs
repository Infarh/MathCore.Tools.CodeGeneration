using System.Text;
using MathCore.Tools.CodeGeneration.Extensions.Infrastructure;

namespace MathCore.Tools.CodeGeneration.Extensions;

public static class StringBuilderEx
{
    public static IEnumerable<string> EnumLines(this StringBuilder builder) => builder.ToString().EnumLines();

    public static IEnumerable<T> EnumLines<T>(this StringBuilder builder, Func<string, T> selector) => builder.ToString().EnumLines(selector);
    public static IEnumerable<T> EnumLines<T>(this StringBuilder builder, Func<string, int, T> selector) => builder.ToString().EnumLines(selector);

    public static string ToNumeratedLinesString(this StringBuilder builder) => builder.EnumLines(static (s, i) => $"{i + 1,3}|{s}").JoinStringLN();

    public static StringBuilder UsingNamespace(this StringBuilder builder, string NameSpace) => builder
        .Append("using ")
        .Append(NameSpace)
        .AppendLine(";");

    public static StringBuilder Namespace(this StringBuilder builder, string Namespace) => builder
        .Append("namespace ")
        .Append(Namespace)
        .AppendLine(";");

    public static StringBuilder Nullable(this StringBuilder builder, bool Enable = true)
    {
        if (builder.Length < 2 || builder[builder.Length - 1] != '\n')
            builder.LN();
        return builder.AppendLine(Enable ? "#nullable enable" : "#nullable disable");
    }

    public static StringWriter CreateWriter(this StringBuilder builder) => new(builder);

    public readonly ref struct RegionBuilder
    {
        private readonly int _Ident;
        private readonly bool _FreeLineOffset;

        public StringBuilder Source { get; }

        public RegionBuilder(StringBuilder source, string RegionName, int Ident = 1, bool FreeLineOffset = true)
        {
            Source = source;
            _Ident = Ident;
            _FreeLineOffset = FreeLineOffset;
            source.Ident(Ident).Append("# region {0}", RegionName).LN();
            if (FreeLineOffset)
                source.LN();
        }

        public void Dispose()
        {
            var source = Source;
            if (_FreeLineOffset)
                source.LN();
            source.Ident(_Ident).Append("#endregion").LN();
        }
    }

    public static RegionBuilder Region(this StringBuilder builder, string RegionName, int Ident = 1, bool FreeLineOffset = true) =>
        new(builder, RegionName, Ident, FreeLineOffset);

    public static StringBuilder AddProperty(this StringBuilder source, string Type, string FieldName, string PropertyName) => source
        .Ident().Append("public ").Append(Type).Append(' ').Append(PropertyName).LN()
        .Ident().Append("{").LN()
        .Ident(2).Append("get => ").Append(FieldName).Append(';').LN()
        .Ident(2).Append("set => ").Append(FieldName).Append(" = value;").LN()
        .Ident().Append("}").LN();

    public static StringBuilder AddNotifyProperty(
        this StringBuilder source,
        string Type,
        string FieldName,
        string PropertyName,
        bool GenerateEvent = false,
        string? Comment = null)
    {
        if (Comment is { Length: > 0 })
        {
            if (!Comment.StartsWith("    "))
                source.Ident();
            source.Append(Comment.TrimEnd()).LN();
        }
        source.Ident().Append("public ").Append(Type).Append(' ').Append(PropertyName).LN();
        source.Ident().Append('{').LN();
        source.Ident(2).Append("get => ").Append(FieldName).Append(';').LN();

        if (GenerateEvent)
        {
            source.Ident(2).Append("set").LN();
            source.Ident(2).Append("{").LN();
            source.Ident(3).Append("if(Equals(value, {0})) return;", FieldName).LN();
            source.Ident(3).Append(FieldName).Append(" = value;").LN();
            source.Ident(3).Append(PropertyName).Append("Changed?.Invoke(this, System.EventArgs.Empty);").LN();
            source.Ident(2).Append("}").LN();
            source.Ident().Append('}').LN()
                .LN();
            source.Ident().Append("public System.EventHandler ").Append(PropertyName).Append("Changed = null;").LN();
        }
        else
        {
            source.Ident(2).Append("set => Set(ref {0}, value);", FieldName).LN();
            source.Ident().Append('}').LN();
        }

        return source;
    }

    public static StringBuilder AddDependencyProperty(
        this StringBuilder source,
        string PropertyName,
        string PropertyTypeName,
        string PropertyHostTypeName,
        string? PropertyChangedCallbackMethodName = null,
        string? PropertyCoerceMethodName = null)
    {
        source.Ident().Append("public static readonly DependencyProperty ").Append(PropertyName).Append(" =").LN();
        source.Ident(2).AppendNameOf(PropertyName).Append(',').LN();
        source.Ident(2).AppendTypeOf(PropertyTypeName).Append(',').LN();
        source.Ident(2).AppendTypeOf(PropertyHostTypeName).Append(',').LN();

        if (PropertyChangedCallbackMethodName is not { Length: > 0 } && PropertyCoerceMethodName is not { Length: > 0 })
            source.Ident(2).AppendDefault(PropertyTypeName).LN();
        else
        {
            source.Ident(2).Append("new(");
            source.AppendDefault(PropertyTypeName).Append(',').Append(' ');
            source.Append(PropertyChangedCallbackMethodName is { Length: > 0 } ? PropertyChangedCallbackMethodName : "null").Append(',').Append(' ');
            source.Append(PropertyCoerceMethodName is { Length: > 0 } ? PropertyCoerceMethodName : "null");
            source.Append(')');
        }

        source.Ident().Append(");");

        return source;
    }

    public static StringBuilder AppendDefault(this StringBuilder builder, string TypeName) => builder.Append("default(").Append(TypeName).Append(')');

    public static StringBuilder AppendNameOf(this StringBuilder builder, string TypeName) => builder.Append("nameof(").Append(TypeName).Append(')');

    public static StringBuilder AppendTypeOf(this StringBuilder builder, string TypeName) => builder.Append("typeof(").Append(TypeName).Append(')');

    public static StringBuilder Ident(this StringBuilder builder, int Level = 1, string Ident = "    ")
    {
        while (Level-- > 0)
            builder.Append(Ident);
        return builder;
    }
}