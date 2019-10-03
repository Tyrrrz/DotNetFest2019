using System.Collections.Generic;

namespace JsonParser
{
    public class JsonArray : JsonEntity
    {
        public IReadOnlyList<JsonEntity> Children { get; }

        public JsonArray(IReadOnlyList<JsonEntity> children)
        {
            Children = children;
        }

        public override JsonEntity this[int index] => Children.Count > index ? Children[index] : null;
    }
}