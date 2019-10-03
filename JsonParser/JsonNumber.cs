namespace JsonParser
{
    public class JsonNumber : JsonLiteral<double>
    {
        public JsonNumber(double value) : base(value)
        {
        }
    }
}