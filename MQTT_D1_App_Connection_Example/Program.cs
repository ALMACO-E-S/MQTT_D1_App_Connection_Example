// See https://aka.ms/new-console-template for more information


using Newtonsoft.Json;
using MQTT_D1_App_Connection_Example;

MQTTConnection mqtt = new MQTTConnection();
mqtt.D1Response += D1Response;
mqtt.D1Readings += D1Readings;
JSON_Response d1Response = new JSON_Response();
JSON_D1Readings d1Readings = new JSON_D1Readings();
bool connected = false;

   
connected = await mqtt.StartClient();


while (connected)
{
    string input = Console.ReadLine();
    switch (input)
    {
        case "hide":
            JSON_Command hide = new JSON_Command
            {
                timestamp = DateTime.Now,
                command_string = "hide"
            };
            var hideJSON = JsonConvert.SerializeObject(hide);
            await mqtt.MqttPublish("d1-command", hideJSON);
            break;
        case "show":
            JSON_Command show = new JSON_Command
            {
                timestamp = DateTime.Now,
                command_string = "show"
            };
            var showJSON = JsonConvert.SerializeObject(show);
            await mqtt.MqttPublish("d1-command", showJSON);
            break;
        case "readings":
            Console.Write("This is the left temp but in you can change the code to make it something else " + d1Readings.left_temperature_raw +" \n");
            break;
        case "connected":
            Console.Write("The MQTT is connected? "+ d1Response.response_string);
            break;
        default:
            Console.WriteLine("Options for commands are: hide, show, readings, connected"+" \n");
            break;
    }
    connected = mqtt.GetConnection();
}

Console.WriteLine("Disconnected");


 void D1Readings(object sender, JSON_D1Readings thisReading)
{
    d1Readings = thisReading;
}

 void D1Response(object sender, JSON_Response thisResponse)
 {
     d1Response = thisResponse;
 }
