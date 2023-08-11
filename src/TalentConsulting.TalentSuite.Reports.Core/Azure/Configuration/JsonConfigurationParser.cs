// based on https://github.com/aspnet/Configuration/blob/release/2.2/src/Config.Json/JsonConfigurationFileParser.cs, which is
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TalentConsulting.TalentSuite.Reports.Core.Azure.Configuration;

public class JsonConfigurationStreamParser
{
    private readonly Stack<string> _context = new();

    private readonly IDictionary<string, string> _data =
        new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    private string _currentPath;

    private JsonTextReader _reader;

    private JsonConfigurationStreamParser()
    {
    }

    public static IDictionary<string, string> Parse(Stream input)
    {
        return new JsonConfigurationStreamParser().ParseStream(input);
    }

    private IDictionary<string, string> ParseStream(Stream input)
    {
        _data.Clear();
        _reader = new JsonTextReader(new StreamReader(input));
        _reader.DateParseHandling = DateParseHandling.None;

        var jsonConfig = JObject.Load(_reader);

        VisitJObject(jsonConfig);

        return _data;
    }

    private void VisitJObject(JObject jObject)
    {
        foreach (var property in jObject.Properties())
        {
            EnterContext(property.Name);
            VisitProperty(property);
            ExitContext();
        }
    }

    private void VisitProperty(JProperty property)
    {
        VisitToken(property.Value);
    }

    private void VisitToken(JToken token)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
                VisitJObject(token.Value<JObject>());
                break;

            case JTokenType.Array:
                VisitArray(token.Value<JArray>());
                break;

            case JTokenType.Integer:
            case JTokenType.Float:
            case JTokenType.String:
            case JTokenType.Boolean:
            case JTokenType.Bytes:
            case JTokenType.Raw:
            case JTokenType.Null:
                VisitPrimitive(token.Value<JValue>());
                break;

            default:
                throw new FormatException(
                    $"Unsupported JSON token '{_reader.TokenType}' was found. Path '{_reader.Path}', line {_reader.LineNumber} position {_reader.LinePosition}.");
        }
    }

    private void VisitArray(JArray array)
    {
        for (var index = 0; index < array.Count; index++)
        {
            EnterContext(index.ToString());
            VisitToken(array[index]);
            ExitContext();
        }
    }

    private void VisitPrimitive(JValue data)
    {
        var key = _currentPath;

        if (_data.ContainsKey(key))
        {
            throw new FormatException($"A duplicate key '{key}' was found.");
        }

        _data[key] = data.ToString(CultureInfo.InvariantCulture);
    }

    private void EnterContext(string context)
    {
        _context.Push(context);
        _currentPath = ConfigurationPath.Combine(_context.Reverse());
    }

    private void ExitContext()
    {
        _context.Pop();
        _currentPath = ConfigurationPath.Combine(_context.Reverse());
    }
}