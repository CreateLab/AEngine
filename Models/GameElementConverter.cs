using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models;

public class GameElementConverter : JsonConverter<IGameElement>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(IGameElement).IsAssignableFrom(typeToConvert);
    }

    public override IGameElement? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        var elementJson = JsonDocument.ParseValue(ref reader);
        var typeProperty = elementJson.RootElement.GetProperty("Type").GetString();

        return typeProperty switch
        {
            "Scene" => JsonSerializer.Deserialize<Scene>(elementJson.RootElement.GetRawText()),
            "Dialog" => JsonSerializer.Deserialize<Dialog>(elementJson.RootElement.GetRawText()),
            _ => throw new NotSupportedException($"Unsupported game element type: {typeProperty}")
        };
    }

    public override void Write(Utf8JsonWriter writer, IGameElement value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}