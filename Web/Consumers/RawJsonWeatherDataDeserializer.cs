// using System;
// using System.Net.Mime;
// using System.Text;
// using System.Text.Json;
// using MassTransit;
// using MassTransit.Context;
// using Web.Contract;
//
// public class RawJsonWeatherDataDeserializer : IMessageDeserializer
// {
//     private readonly JsonSerializerOptions _jsonOptions;
//     private IMessageDeserializer _messageDeserializerImplementation;
//
//     public RawJsonWeatherDataDeserializer()
//     {
//         _jsonOptions = new JsonSerializerOptions
//         {
//             PropertyNameCaseInsensitive = true
//         };
//     }
//
//     public ConsumeContext Deserialize(ReceiveContext receiveContext)
//     {
//         var body = receiveContext.GetBody();
//         var jsonString = Encoding.UTF8.GetString(body);
//
//         try
//         {
//             var weatherData = JsonSerializer.Deserialize<WeatherData>(jsonString, _jsonOptions);
//
//             if (weatherData == null)
//             {
//                 throw new InvalidOperationException("Deserialized WeatherData is null");
//             }
//
//             return new RawJsonConsumeContext<WeatherData>(receiveContext, weatherData);
//         }
//         catch (JsonException ex)
//         {
//             throw new JsonException( "Failed to deserialize JSON", ex);
//         }
//     }
//
//     public ContentType ContentType => new ContentType("application/json");
//
//     public IMessageSerializer GetMessageSerializer()
//     {
//         throw new NotImplementedException("Message serialization is not supported by this deserializer");
//     }
//
//     public ISerialization Serialization => throw new NotImplementedException("Serialization is not supported by this deserializer");
// }
//
// public class RawJsonConsumeContext<T> : DeserializerConsumeContext where T : class
// {
//     public RawJsonConsumeContext(ReceiveContext receiveContext, T message)
//         : base(receiveContext)
//     {
//         Message = message;
//     }
//
//     public override bool HasMessageType(Type messageType) => messageType.IsAssignableFrom(typeof(T));
//
//     public override bool TryGetMessage<T1>(out T1 message)
//     {
//         if (Message is T1 result)
//         {
//             message = result;
//             return true;
//         }
//
//         message = default;
//         return false;
//     }
//
//     public override Guid? MessageId => null;
//     public override Guid? RequestId => null;
//     public override Guid? CorrelationId => null;
//     public override Guid? ConversationId => null;
//     public override Guid? InitiatorId => null;
//     public override DateTime? ExpirationTime => null;
//     public override Uri SourceAddress => null;
//     public override Uri DestinationAddress => null;
//     public override Uri ResponseAddress => null;
//     public override Uri FaultAddress => null;
//     public override DateTime? SentTime => null;
//     public override Headers Headers => new DictionaryHeaders(new Dictionary<string, object>());
//     public override HostInfo Host => default;
//
//     public T Message { get; }
// }