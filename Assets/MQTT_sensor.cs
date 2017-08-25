using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQTT_sensor : MonoBehaviour {

    MQTT_client client;
    TextPopup text;

	// Use this for initialization
	void Start () {
        client = GameObject.Find("MQTT_client").GetComponent<MQTT_client>();

        // Get the position of the parent geometry (an imported mesh from Rhino, so need to use mesh bounds)
        Vector3 position = transform.parent.GetComponent<MeshRenderer>().bounds.center;
        transform.position = position;

        // Set the label text by sensor type and number
        text = transform.GetComponentInChildren<TextPopup>();
        name = transform.parent.parent.name + "-" + transform.parent.name.Substring(transform.parent.name.Length-1, 1) + "\n";
        text.TextPop = name;
    }
	
	// Update is called once per frame
	void Update () {
        text.TextPop = name + "Temp: " + client.temperature +"F\n";
        text.TextPop += "Humidity: " + client.humidity + "%";
    }
}
