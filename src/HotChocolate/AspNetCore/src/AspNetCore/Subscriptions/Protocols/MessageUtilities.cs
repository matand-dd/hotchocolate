#if NET6_0_OR_GREATER
using System.Text.Json;
using HotChocolate.AspNetCore.Subscriptions.Protocols.GraphQLOverWebSocket;
using HotChocolate.Utilities;
#else
using System.Text.Json;
using System.Text.Json.Serialization;
using HotChocolate.AspNetCore.Subscriptions.Protocols.GraphQLOverWebSocket;
using HotChocolate.Utilities;
#endif

namespace HotChocolate.AspNetCore.Subscriptions.Protocols;

internal static class MessageUtilities
{
    public static JsonWriterOptions WriterOptions { get; } =
        new() { Indented = false };

#if NET6_0_OR_GREATER
    public static JsonSerializerOptions SerializerOptions { get; } =
        new(JsonSerializerDefaults.Web);
#else
    public static JsonSerializerOptions SerializerOptions { get; } =
        new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            #if NET6_0_OR_GREATER
            NumberHandling = JsonNumberHandling.AllowReadingFromString
            #endif
        };
#endif

    public static void SerializeMessage(
        ArrayWriter arrayWriter,
        ReadOnlySpan<byte> type,
        IReadOnlyDictionary<string, object?>? payload = null,
        string? id = null)
    {
        using var jsonWriter = new Utf8JsonWriter(arrayWriter, WriterOptions);
        jsonWriter.WriteStartObject();

        if (id is not null)
        {
            jsonWriter.WriteString("id", id);
        }

        jsonWriter.WriteString("type", type);

        if (payload is not null)
        {
            jsonWriter.WritePropertyName("payload");
            JsonSerializer.Serialize(jsonWriter, payload);
        }

        jsonWriter.WriteEndObject();
        jsonWriter.Flush();
    }

    public static bool TryGetPayload(JsonElement root, out JsonElement payload)
    {
        if (root.TryGetProperty(Utf8MessageProperties.Payload, out JsonElement payloadValue) &&
            payloadValue.ValueKind is JsonValueKind.Object)
        {
            payload = payloadValue;
            return true;
        }

        payload = default;
        return false;
    }
}
