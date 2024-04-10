﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentConsulting.MinimalApi.Registration;

[Generator]
public class SourceGenerator : ISourceGenerator
{
    private readonly ContextReceiver _contextReceiver = new();

    private static string GetNamespace(SyntaxNode? node) => node switch
        {
            NamespaceDeclarationSyntax namespaceNode => namespaceNode.Name.ToString(),
            FileScopedNamespaceDeclarationSyntax fileScopedNamespaceNode => fileScopedNamespaceNode.Name.ToString(),
            { } => GetNamespace(node.Parent),
            _ => throw new InvalidOperationException("Could not find namespace")
        };

    public void Initialize(GeneratorInitializationContext context) => context.RegisterForSyntaxNotifications(() => _contextReceiver);

    public void Execute(GeneratorExecutionContext context)
    {
        if (_contextReceiver.TargetClass is null)
        {
            throw new InvalidOperationException("Could not find partial class 'WebApplicationExtensions'");
        }

        var nsHashSet = new HashSet<string>();
        var usings = new StringBuilder();
        var endpointRegistrations = new StringBuilder();
        
        foreach (var endpointClass in _contextReceiver.Endpoints)
        {
            var classNamespace = GetNamespace(endpointClass);
            if (!nsHashSet.Contains(classNamespace))
            {
                nsHashSet.Add(classNamespace);
                usings.AppendLine($"using {classNamespace};");
            }

            endpointRegistrations.AppendLine($"\t\t{endpointClass.Identifier.ValueText}.Register(app);");
        }

        var fileNs = GetNamespace(_contextReceiver.TargetClass);
        var source = // lang=C#
            $$""""
// <auto-generated/>

{{usings}}

namespace {{fileNs}}; 


static partial class WebApplicationExtensions
{
    static partial void RegisterEndpoints(this WebApplication app)
    { 
{{endpointRegistrations}}
    }
}
"""";
        context.AddSource($"MinimalApiRegistrator.generated.cs", SourceText.From(source, Encoding.UTF8));
    }
}