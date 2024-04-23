using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace TalentConsulting.MinimalApi.Registration;

[ExcludeFromCodeCoverage]
internal class ContextReceiver : ISyntaxContextReceiver
{
    private readonly Collection<ClassDeclarationSyntax> _endpoints = [];
    public IReadOnlyCollection<ClassDeclarationSyntax> Endpoints => _endpoints;
    public ClassDeclarationSyntax? TargetClass { get; private set; }

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax) return;

        if (classDeclarationSyntax.Identifier.ValueText == "WebApplicationExtensions")
        {
            TargetClass = classDeclarationSyntax;
        }
        else
        {
            var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);

            if (classSymbol?.IsAbstract ?? false) return;

            if (classSymbol?.AllInterfaces.Any(i => i.ToDisplayString().EndsWith("IApiEndpoint")) ?? false)
            {
                _endpoints.Add(classDeclarationSyntax);
            }
        }
    }
}