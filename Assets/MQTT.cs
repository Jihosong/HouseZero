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

public class MQTT : MonoBehaviour
{
    private MqttClient client;

    public String value;
    public bool button;
    Vector3 position;

    // Use this for initialization
    void Start()
    {
        // Setting up MQTT client with broker address and user info
        client = new MqttClient("io.adafruit.com");
        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId, "brian_ho", "74a85faff77343adbe82215cbf3ccd75");
        print(client.IsConnected? "CONNECTED" : "FAILED TO CONNECT");

        // Internal subscribe to events
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        // MQTT subscribe to feeds with QoS 
        client.Subscribe(new string[] { "brian_ho/feeds/test-slider" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        client.Subscribe(new string[] { "brian_ho/feeds/test-boolean" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

        // Initalize variables that will get MQTT updates
        value = "0";
        button = false;
    }

    // Update is called once per frame 
    void Update()
    {
        float size = (float)((int.Parse(value) / (1024.0)) * 5);

        this.gameObject.transform.localScale = new Vector3(size, size, size);
        this.gameObject.transform.localPosition = button ? new Vector3(0, (float)0.5, 0) : new Vector3(0, 0, 0);

        // print(size + ", " + value + ", " + button);
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
                value = message;
                break;
            case "brian_ho/feeds/test-boolean":
                button = message == "ON" ? true : false;
                break;
        }
        print("MESSAGE from " + e.Topic + " = " + message);
    }

    
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
    
}
