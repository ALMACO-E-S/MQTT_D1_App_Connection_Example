using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using System.Text;
using System.Text.Json;
using MQTTnet.Server;


namespace MQTT_D1_App_Connection_Example;

internal class MQTTConnection
{
  private IManagedMqttClient managedMqttClient;
  public event EventHandler<JSON_Response> D1Response;
  public event EventHandler<JSON_D1Readings> D1Readings;


  public async Task<bool> StartClient()
  {
    managedMqttClient = new MqttFactory().CreateManagedMqttClient();

    var tlsOptions = new MqttClientTlsOptions
    {
      UseTls = false,
      IgnoreCertificateChainErrors = true,
      IgnoreCertificateRevocationErrors = true,
      AllowUntrustedCertificates = true
    };

    var options = new MqttClientOptions
    {
      CleanSession = true,
      ClientId = "ConsoleApp",
      KeepAlivePeriod = TimeSpan.FromSeconds(5),
      ProtocolVersion = MqttProtocolVersion.V500,
      ChannelOptions = new MqttClientTcpOptions
      {
        Server = "localhost",
        Port = 1883,
        TlsOptions = tlsOptions
      }
    };

    await managedMqttClient.SubscribeAsync("d1-readings", MqttQualityOfServiceLevel.AtLeastOnce);
    await managedMqttClient.SubscribeAsync("d1-response", MqttQualityOfServiceLevel.AtLeastOnce);

    await managedMqttClient.StartAsync(
        new ManagedMqttClientOptions
        {
          ClientOptions = options
        });

    Thread.Sleep(1000); //The client needs time to sort itself (approx 0.125s on development machine)
    managedMqttClient.ApplicationMessageReceivedAsync += HandleReceivedApplicationMessage;

    if (managedMqttClient.IsConnected == false) Console.WriteLine("Ran into an issue in connecting to the D1 App.");
    else Console.WriteLine("Successfully Connected to the D1 App MQTT Server");

    return managedMqttClient.IsConnected;
  }


  private Task ManagedMqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
  {
    return Task.CompletedTask;
  }

  private Task ManagedMqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
  {
    return Task.CompletedTask;
  }

  public bool GetConnection()
  {
    return managedMqttClient.IsConnected;
  }

  public Task HandleReceivedApplicationMessage(MqttApplicationMessageReceivedEventArgs eventArgs)
  {
    var thisTopic = eventArgs.ApplicationMessage?.Topic;
    var thisPayload = eventArgs.ApplicationMessage.ConvertPayloadToString();

    var errors = new List<string>();

    try
    {
      switch (thisTopic)
      {
        case "d1-response":
          var d1Response = JsonSerializer.Deserialize<JSON_Response>(thisPayload);
          if (!errors.Any())
            D1Response.Invoke(this, d1Response);
          break;
        case "d1-readings":
          var d1Reading = JsonSerializer.Deserialize<JSON_D1Readings>(thisPayload);
          if (!errors.Any())
            D1Readings.Invoke(this, d1Reading);
          break;
        default:
          Console.WriteLine("Unknown topic");
          break;
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.ToString() + "tried to read data from MQTT");
    }

    return Task.CompletedTask;
  }

  public async Task<bool> MqttPublish(string topic, string payload)
  {
    try
    {
      if (managedMqttClient == null)
      {
        return false;
      }

      if (!managedMqttClient.IsConnected)
      {
        return false;
      }

      var thisPayload = Encoding.UTF8.GetBytes(payload);
      var message = new MqttApplicationMessageBuilder()
          .WithTopic(topic).WithPayload(payload)
          .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce).WithRetainFlag().Build();

      if (managedMqttClient != null) await managedMqttClient.EnqueueAsync(message);
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }
}
