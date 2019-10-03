namespace JsonParser
{
    public class JsonNull : JsonLiteral<object>
    {
        public JsonNull() : base(null)
        {
        }
    }
}