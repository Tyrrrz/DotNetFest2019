﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sprache;

namespace JsonParser
{
    internal static class JsonGrammar
    {
        private static readonly Parser<JsonNull> JsonNull = Parse.String("null").Return(new JsonNull());

        private static readonly Parser<JsonBoolean> TrueJsonBoolean = Parse.String("true").Return(new JsonBoolean(true));

        private static readonly Parser<JsonBoolean> FalseJsonBoolean = Parse.String("false").Return(new JsonBoolean(false));

        private static readonly Parser<JsonBoolean> JsonBoolean = TrueJsonBoolean.Or(FalseJsonBoolean);

        private static readonly Parser<JsonNumber> JsonNumber =
            Parse.DecimalInvariant.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).Select(v => new JsonNumber(v));

        private static readonly Parser<JsonString> JsonString =
            from open in Parse.Char('"')
            from value in Parse.CharExcept('"').Many().Text()
            from close in Parse.Char('"')
            select new JsonString(value);

        private static readonly Parser<KeyValuePair<string, JsonEntity>> JsonProperty =
            from name in JsonString.Select(s => s.Value)
            from colon in Parse.Char(':').Token()
            from value in JsonEntity
            select new KeyValuePair<string, JsonEntity>(name, value);

        private static readonly Parser<JsonObject> JsonObject =
            from open in Parse.Char('{')
            from properties in JsonProperty.Token().DelimitedBy(Parse.Char(','))
            from close in Parse.Char('}')
            select new JsonObject(properties.ToDictionary(p => p.Key, p => p.Value));

        private static readonly Parser<JsonArray> JsonArray =
            from open in Parse.Char('[')
            from children in JsonEntity.Token().DelimitedBy(Parse.Char(','))
            from close in Parse.Char(']')
            select new JsonArray(children.ToArray());

        public static readonly Parser<JsonEntity> JsonEntity =
            JsonArray.Or<JsonEntity>(JsonObject).Or(JsonString).Or(JsonNumber).Or(JsonBoolean).Or(JsonNull);
    }
}