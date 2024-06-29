using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore.Tools.CodeGeneration.Infrastructure;

/// <summary>
/// Indicates that the value of the marked element can never be <c>null</c>.
/// </summary>
/// <example><code>
/// [NotNull] object Foo() {
///   return null; // Warning: Possible 'null' assignment
/// }
/// </code></example>
[AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
    AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
internal sealed class NotNullAttribute : Attribute;

/// <summary>
/// Indicates that the marked method builds string by the format pattern and (optional) arguments.
/// The parameter, which contains the format string, should be given in the constructor. The format string
/// should be in <see cref="string.Format(IFormatProvider,string,object[])"/>-like form.
/// </summary>
/// <example><code>
/// [StringFormatMethod("message")]
/// void ShowError(string message, params object[] args) { /* do something */ }
/// 
/// void Foo() {
///   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
/// }
/// </code></example>
[AttributeUsage(
    AttributeTargets.Constructor | AttributeTargets.Method |
    AttributeTargets.Property | AttributeTargets.Delegate)]
internal sealed class StringFormatMethodAttribute : Attribute
{
    /// <param name="formatParameterName">
    /// Specifies which parameter of an annotated method should be treated as the format string
    /// </param>
    public StringFormatMethodAttribute([NotNull] string formatParameterName) => FormatParameterName = formatParameterName;

    [NotNull] public string FormatParameterName { get; }
}

/// <summary>Specifies the syntax used in a string.</summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
internal sealed class StringSyntaxAttribute : Attribute
{
    /// <summary>Initializes the <see cref="StringSyntaxAttribute"/> with the identifier of the syntax used.</summary>
    /// <param name="syntax">The syntax identifier.</param>
    public StringSyntaxAttribute(string syntax)
    {
        Syntax = syntax;
        Arguments = [];
    }

    /// <summary>Initializes the <see cref="StringSyntaxAttribute"/> with the identifier of the syntax used.</summary>
    /// <param name="syntax">The syntax identifier.</param>
    /// <param name="arguments">Optional arguments associated with the specific syntax employed.</param>
    public StringSyntaxAttribute(string syntax, params object?[] arguments)
    {
        Syntax = syntax;
        Arguments = arguments;
    }

    /// <summary>Gets the identifier of the syntax used.</summary>
    public string Syntax { get; }

    /// <summary>Optional arguments associated with the specific syntax employed.</summary>
    public object?[] Arguments { get; }

    /// <summary>The syntax identifier for strings containing composite formats for string formatting.</summary>
    public const string CompositeFormat = nameof(CompositeFormat);

    /// <summary>The syntax identifier for strings containing date format specifiers.</summary>
    public const string DateOnlyFormat = nameof(DateOnlyFormat);

    /// <summary>The syntax identifier for strings containing date and time format specifiers.</summary>
    public const string DateTimeFormat = nameof(DateTimeFormat);

    /// <summary>The syntax identifier for strings containing <see cref="Enum"/> format specifiers.</summary>
    public const string EnumFormat = nameof(EnumFormat);

    /// <summary>The syntax identifier for strings containing <see cref="Guid"/> format specifiers.</summary>
    public const string GuidFormat = nameof(GuidFormat);

    /// <summary>The syntax identifier for strings containing JavaScript Object Notation (JSON).</summary>
    public const string Json = nameof(Json);

    /// <summary>The syntax identifier for strings containing numeric format specifiers.</summary>
    public const string NumericFormat = nameof(NumericFormat);

    /// <summary>The syntax identifier for strings containing regular expressions.</summary>
    public const string Regex = nameof(Regex);

    /// <summary>The syntax identifier for strings containing time format specifiers.</summary>
    public const string TimeOnlyFormat = nameof(TimeOnlyFormat);

    /// <summary>The syntax identifier for strings containing <see cref="TimeSpan"/> format specifiers.</summary>
    public const string TimeSpanFormat = nameof(TimeSpanFormat);

    /// <summary>The syntax identifier for strings containing URIs.</summary>
    public const string Uri = nameof(Uri);

    /// <summary>The syntax identifier for strings containing XML.</summary>
    public const string Xml = nameof(Xml);
}