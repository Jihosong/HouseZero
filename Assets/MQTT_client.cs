using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

public class MQTT_client : MonoBehaviour
{
    private MqttClient client;

    // List of public variables for other GameObjects
    public string Sphere1_value;
    public string Sphere2_value;
    public bool Sphere1_button;
    public bool Sphere2_button;
    public string temperature;
    public string humidity;

    TextMesh console;

    // Use this for initialization
    void Start()
    {
        // Setting up MQTT client with broker address and user info
        string clientId = Guid.NewGuid().ToString();
        client = new MqttClient("io.adafruit.com");
        client.Connect(clientId, "brian_ho", "74a85faff77343adbe82215cbf3ccd75");

        print(client.IsConnected? "CONNECTED" : "FAILED TO CONNECT");

        console = GameObject.Find("Status_Console").GetComponent<TextMesh>();
        console.text = client.IsConnected ? "CONNECTED TO MQTT SERVER" : "FAILED TO CONNECT TO MQTT SERVER";

        // Internal subscribe to events
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        // MQTT subscribe to feeds with QoS 
        client.Subscribe(new string[] { "brian_ho/feeds/test-slider" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Subscribe(new string[] { "brian_ho/feeds/test-slider-2" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Subscribe(new string[] { "brian_ho/feeds/test-boolean" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });


        client.Subscribe(new string[] { "brian_ho/feeds/temp" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Subscribe(new string[] { "brian_ho/feeds/humidity" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Subscribe(new string[] { "brian_ho/errors" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Subscribe(new string[] { "brian_ho/throttle" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        // Initalize variables that will get MQTT updates
        Sphere1_value = "0"; Sphere2_value = "0";
        Sphere1_button = false; Sphere2_button = false;
        temperature = "70.0"; humidity = "30.0";

    }

    // MQTT feed subscription event
    void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
    {
        print("Subscribed for id = " + e.MessageId);
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // Use the switch and case conditional to filter messages by feed
        string message = Encoding.UTF8.GetString(e.Message);
        switch (e.Topic)
        {
            case "brian_ho/feeds/test-slider":
                Sphere1_value = message;
                break;
            case "brian_ho/feeds/test-slider-2":
                Sphere2_value = message;
                break;
            case "brian_ho/feeds/test-boolean":
                Sphere1_button = message == "ON" ? true : false;
                Sphere2_button = message == "ON" ? true : false;
                break;
            case "brian_ho/feeds/temp":
                temperature = message;
                break;
            case "brian_ho/feeds/humidity":
                humidity = message;
                break;
            case "brian_ho/errors":
                print(message);
                console.text = message;
                break;
            case "brian_ho/throttle":
                print(message);
                console.text = message;
                break;
        }
        print("MESSAGE from " + e.Topic + " = " + message);
    }

    /*
    // When we click the button, we will flip the switch.
    void OnMouseDown()
    {
        button = button ? false : true;
        string strValue = button.ToString();
        string strValue2 = button ? "ON" : "OFF";
        print("CLICKED " + strValue2);

        // publish a message oto a feed with appropriate QoS 
        client.Publish("brian_ho/feeds/test-string", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        client.Publish("brian_ho/feeds/test-boolean", Encoding.UTF8.GetBytes(strValue2), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
    }
    */
    void publish(object[] payload)
    {
        string source = (string)payload[0];
        string message = (string)payload[1];

        string feed = null;
        switch (source)
        {
            case "Sphere1":
                feed = "brian_ho/feeds/test-boolean";
                break;
            case "Sphere2":
                feed = "brian_ho/feeds/test-boolean";
                break;
        }

        // publish a message oto a feed with appropriate QoS 
        if (feed != null)
        {
            client.Publish(feed, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }
    }
}
