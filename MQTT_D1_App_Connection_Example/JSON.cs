using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_D1_App_Connection_Example
{

        public class JSON_D1Readings
        {
           
            public DateTime timestamp { get; set; }
            public double left_weight { get; set; }
            public double left_test_weight { get; set; }
            public double left_moisture { get; set; }
            public double left_temperature { get; set; }
            public double right_weight { get; set; }
            public double right_test_weight { get; set; }
            public double right_moisture { get; set; }
            public double right_temperature { get; set; }
            public double left_weight_raw { get; set; }
            public double left_test_weight_raw { get; set; }
            public double left_moisture_raw { get; set; }
            public double left_temperature_raw { get; set; }
            public double right_weight_raw { get; set; }
            public double right_test_weight_raw { get; set; }
            public double right_moisture_raw { get; set; }
            public double right_temperature_raw { get; set; }

         
            public double GetCalDataByString(string thisData)
            {
                switch (thisData)
                {
                    case "Left_Weight":
                        return left_weight;
                    case "Left_Test_Weight":
                        return left_test_weight;
                    case "Left_Moisture":
                        return left_moisture;
                    case "Left_Temperature":
                        return left_temperature;
                    case "Right_Weight":
                        return right_weight;
                    case "Right_Test_Weight":
                        return right_test_weight;
                    case "Right_Moisture":
                        return right_moisture;
                    case "Right_Temperature":
                        return right_temperature;
                    default:
                        return -9999;
                }
            }

          
            public double GetCalDataByInt(int thisData)
            {
                switch (thisData)
                {
                    case Constants.LeftWeight:
                        return left_weight;
                    case Constants.LeftTestWeight:
                        return left_test_weight;
                    case Constants.LeftMoisture:
                        return left_moisture;
                    case Constants.LeftTemperature:
                        return left_temperature;
                    case Constants.RightWeight:
                        return right_weight;
                    case Constants.RightTestWeight:
                        return right_test_weight;
                    case Constants.RightMoisture:
                        return right_moisture;
                    case Constants.RightTemperature:
                        return right_temperature;
                    default:
                        return -9999;
                }
            }

          
            public double GetRawDataByString(string thisData)
            {
                switch (thisData)
                {
                    case "Left_Weight":
                        return left_weight_raw;
                    case "Left_Test_Weight":
                        return left_test_weight_raw;
                    case "Left_Moisture":
                        return left_moisture_raw;
                    case "Left_Temperature":
                        return left_temperature_raw;
                    case "Right_Weight":
                        return right_weight_raw;
                    case "Right_Test_Weight":
                        return right_test_weight_raw;
                    case "Right_Moisture":
                        return right_moisture_raw;
                    case "Right_Temperature":
                        return right_temperature_raw;
                    default:
                        return -9999;
                }
            }
        }

        public class JSON_Command
        {
            [JsonProperty(Required = 0)]
            public DateTime timestamp { get; set; }
            public string command_string { get; set; }
        }

        public class JSON_Response
        {
            public DateTime timestamp { get; set; }
            public string command_string { get; set; }
            public string response_string { get; set; }
        }
    


    public static class Constants
    {
     
        public const int LeftWeight = 0;
        public const int LeftMoisture = 1;
        public const int LeftTestWeight = 2;
        public const int RightWeight = 3;
        public const int RightMoisture = 4;
        public const int RightTestWeight = 5;
        public const int LeftTemperature = 6; 
        public const int RightTemperature = 7;

    }
}
