using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQTT_object : MonoBehaviour
{
    private MQTT_client client;
    GameObject sphere;
    string name;
    string value;
    public bool button;
    Vector3 position;

    // Use this for initialization
    void Start ()
    {/*
      Transform[] sensors = GetComponentsInChildren<Transform>();
      foreach (Transform sensor in sensors)
        {
            if (sensor.name.Contains("Object"))
            {
                print("SENSOR: " + sensor.name + sensor.position + sensor.localPosition);
                print(sensor.GetComponent<MeshRenderer>().bounds);
            }
        }
        */
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (client != null)
        {
            //value = (string)client.GetType().GetField(name + "_value").GetValue(client);
            //button = (bool)client.GetType().GetField(name + "_button").GetValue(client);
            value = client.Sphere1_value;
            button = client.Sphere1_button;

            float size = (float)((int.Parse(value) / (1024.0)) * 5);

            sphere.transform.localScale = new Vector3(size, size, size);
            sphere.transform.localPosition = button ? position + new Vector3(0, (float)0.5, 0) : position;
        }
        */
    }
    /*
    // Interaction 
    void OnMouseDown()
    {
        button = button ? false : true;
        // string strValue = button.ToString();
        string buttonString = button ? "ON" : "OFF";
        print(name + " CLICKED " + buttonString);

        object[] payload = new object[2];
        payload[0] = name;
        payload[1] = buttonString;

        // publish a message oto a feed with appropriate QoS 
        client.SendMessage("publish", payload);
    }
    */
}
