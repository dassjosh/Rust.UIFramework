using Xunit.Sdk;

namespace Rust.UiFramework.UnitTests.Global.XUnit.Serializers;

public abstract class BaseSerializer<T> : IXunitSerializer
{
    public BaseSerializer() { }

    public object Deserialize(Type type, string serializedValue) => Deserialize(serializedValue);

    public string Serialize(object value) => Serialize((T)value);

    public bool IsSerializable(Type type, object value, out string failureReason)
    {
        if (type == typeof(T))
        {
            failureReason = null;
            return true;
        }

        failureReason = $"Not a {typeof(T)}";
        return false;
    }
    
    protected abstract T Deserialize(string serializedValue);
    protected abstract string Serialize(T value);
}