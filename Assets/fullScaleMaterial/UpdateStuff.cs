using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStuff : MonoBehaviour {

    public GameObject sensorsParent;
    public GameObject buttonsParent;
    private Dictionary<string, GameObject> sensors;
    private Dictionary<string, GameObject> buttons;
    public static UpdateStuff Instance;

    // Use this for initialization
    void Start () {
        Instance = this;
        sensors = new Dictionary<string, GameObject>();
        foreach (Transform child in sensorsParent.transform)
        {
            //Debug.Log("loaded " + child.name);
            sensors.Add(child.name, child.gameObject);
        }

        /*buttons = new Dictionary<string, GameObject>();
        foreach (Transform child in buttonsParent.transform)
        {
            Debug.Log("loaded " + child.name);
            buttons.Add(child.name, child.gameObject);
        }*/

        //this.UpdateSensor("temp1", "hello"); //debuggin
    }
	
	// Update is called once per frame
	void Update () {
		if (!updating)
        {
            if (forUpdate.Count > 0)
            {
                for (int i = forUpdate.Count - 1; i > 0; --i)
                {
                    UpdateSensor(forUpdate[i].Item1, forUpdate[i].Item2.ToString());
                    forUpdate.RemoveAt(i);
                }
            }
        }
	}

    private List<Tuple<string, double>> forUpdate = new List<Tuple<string, double>>();
    private bool updating = false;
    public void UpdateSensor(string sensor, double value)
    {
        if (updating) return;
        updating = true;
        forUpdate.Add(new Tuple<string, double>(sensor, value));
        updating = false;
        //this.UpdateSensor(sensor, value.ToString());
    }

    private void UpdateSensor(string sensor, string value)
    {
        GameObject obj;
        if (sensors.TryGetValue(sensor, out obj))
        {
            //obj is the target sensor
            string svalue = value;
            if (sensor.Contains("temperature"))
            {
                svalue += "ºC";
            }
            else if (sensor.Contains("humidity"))
            {
                svalue += "%";
            }
            obj.transform.GetChild(0).GetComponent<TextMesh>().text = svalue;
            //Debug.Log("updated " + sensor);
        }
    }

    public void UpdateButton(string button, string value)
    {
        GameObject obj;
        if (buttons.TryGetValue(button, out obj))
        {
            obj.SendMessageUpwards("OnSelect"); //TODO: needs to be tested
        }
    }



}

#if UNITY_EDITOR

public class Tuple<T1, T2>
{
    public T1 Item1 { get; private set; }
    public T2 Item2 { get; private set; }
    internal Tuple(T1 first, T2 second)
    {
        Item1 = first;
        Item2 = second;
    }
}

public static class Tuple
{
    public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
    {
        var tuple = new Tuple<T1, T2>(first, second);
        return tuple;
    }
}

#endif