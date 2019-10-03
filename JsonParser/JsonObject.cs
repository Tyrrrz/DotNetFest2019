using System.Collections.Generic;

namespace JsonParser
{
    public class JsonObject : JsonEntity
    {
        public IReadOnlyDictionary<string, JsonEntity> Properties { get; }

        public JsonObject(IReadOnlyDictionary<string, JsonEntity> properties)
        {
            Properties = properties;
        }

        public override JsonEntity this[string name] => Properties.TryGetValue(name, out var result) ? result : null;
    }
}