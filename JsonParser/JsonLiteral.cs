using System;

namespace JsonParser
{
    public abstract class JsonLiteral<TValue> : JsonEntity
    {
        public TValue Value { get; }

        protected JsonLiteral(TValue value)
        {
            Value = value;
        }

        public override T GetValue<T>() => (T) Convert.ChangeType(Value, typeof(T));
    }
}