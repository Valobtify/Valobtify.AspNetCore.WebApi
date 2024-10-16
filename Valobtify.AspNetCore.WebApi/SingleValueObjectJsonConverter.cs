using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Valobtify.AspNetCore.WebApi;

public class SingleValueObjectJsonConverter<TSingleValueObject, TValue> : JsonConverter<TSingleValueObject>
    where TSingleValueObject : SingleValueObject<TSingleValueObject, TValue>, ICreatableValueObject<TSingleValueObject, TValue>
    where TValue : notnull
{
    public override TSingleValueObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        TValue? value = JsonSerializer.Deserialize<TValue>(ref reader, options);

        if (value is null) return null;

        return TSingleValueObject.Create(value) ?? throw new ValidationException("value is not valid");
    }

    public override void Write(Utf8JsonWriter writer, TSingleValueObject value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}