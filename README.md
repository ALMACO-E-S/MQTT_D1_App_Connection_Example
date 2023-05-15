# MQTT D1 App Connection Example
The MQTT D1 App Connection Example is ALMACO's demo and example of how to connect to the D1 App. Development on this application was completed in the Spring of 2023.
Through observing the code, you too can connect to the D1 App.

# Get Started
Requirements for running the App: The D1 App created by ALMACO 

# Terms of Service
The MQTT example application may be cloned through this repository. 
By cloning or using the D1 App and example application, you agree to ALMACO's Terms of Service: **ALMACO End User License Agreement .txt**

# Understanding Operation 
 The project itself is a simple console app that allows the user to manually view and publish messages through
 typing them out on the command line. There are three main components to it; JSON.cs, MQTTConnection.cs, and Program.cs.

## JSON.cs
The JSON.cs file contains the 3 JSON objects that pertain to the JSON objects published and received through the 
MQTT server. They are implemented in MQTTConnection.cs and Program.cs to define the structure of what is sent 
and received through the publish and subscribe methods. 

## MQTTConnection.cs
The connection to the MQTT server is done through the code in MQTTConnection.cs. This file contains key methods 
for creating your client and subscribing to the topics you want to know about. 

## Program.cs
The main code of the console application is located in the Program.cs file. This is a minimal application 
that lets the user type in: hide, show, readings, or connected.  These commands either will display a message 
from a published MQTT topic or it will publish to a topic and change the display location of the D1 App. 

# Helpful Resources
Libraries that we use to perform the MQTT operations:


	MQTTnet version 4.1.3.436
	MQTTnet.Extensions.ManagedClient 4.1.3.436

https://github.com/dotnet/MQTTnet

A helpful for tool for working with MQTT is MQTT Explorer.

https://apps.microsoft.com/store/detail/mqttexplorer/9PP8SFM082WD?hl=en-us&gl=us
