using System.Text;
using Microsoft.CodeAnalysis;

namespace Library.Generators;

[Generator]
public class RoleGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Отправим диагностическое сообщение
        var enumProvider = context.CompilationProvider.Select((compilation, _) =>
        {
            var enumType = compilation.GetTypeByMetadataName("Library.Enums.ERole");
            return enumType;
        });

        context.RegisterSourceOutput(enumProvider, GenerateRoleConstants);
    }

    private static void GenerateRoleConstants(SourceProductionContext context, INamedTypeSymbol? enumType)
    {
        if (enumType is null || enumType.TypeKind != TypeKind.Enum)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(
                    "RG001", "Enum not found", 
                    "Enum 'ERole' not found in the project", 
                    "RoleGenerator", DiagnosticSeverity.Warning, true),
                null));
            return;
        }

        var enumNames = enumType.GetMembers()
            .OfType<IFieldSymbol>()
            .Where(f => f.ConstantValue is not null)
            .Select(f => f.Name)
            .ToList();

        var source = new StringBuilder();
        source.AppendLine("// <auto-generated />");
        source.AppendLine("namespace Library.Constants");
        source.AppendLine("{");
        source.AppendLine("    public static class RoleConstants");
        source.AppendLine("    {");

        foreach (var name in enumNames)
        {
            source.AppendLine($"        public const string {name} = \"{name}\";");
        }

        source.AppendLine("    }");
        source.AppendLine("}");

        context.AddSource("RoleConstants.g.cs", source.ToString());

        context.ReportDiagnostic(Diagnostic.Create(
            new DiagnosticDescriptor(
                "RG002", "Code Generated", 
                $"Generated RoleConstants with {enumNames.Count} values", 
                "RoleGenerator", DiagnosticSeverity.Info, true),
            null));
    }
}
