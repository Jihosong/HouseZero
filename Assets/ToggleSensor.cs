using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;


public class ToggleSensor : MonoBehaviour
{
    public GameObject sensors;

    //Use this for initialization
    void Start()
    {
        sensors = GameObject.FindWithTag("allSensors");
    }

    //Update is called once per frame
    void Update()
    {
    }

    public void OnSensor()
    {
        sensors.SetActive(true);
    }

    public void OffSensor()
    {
        sensors.SetActive(false);
    }
}

