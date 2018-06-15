using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;


public class WindowOnOff : MonoBehaviour
{
    GameObject AirFlow;
    GameObject WindowAxis;

    //Use this for initialization
    void Start()
    {
        AirFlow = GameObject.Find("AirFlow");
        WindowAxis = GameObject.Find("WindowAxis");
        WindowAxis.GetComponent<windowOpenScript>().open = false;
        AirFlow.SetActive(false);
    }

    //Update is called once per frame
    void Update()
    {
    }

    public void OnSelect()
    {
        AirFlow.SetActive(true);
        WindowAxis.GetComponent<windowOpenScript>().open = true;
    }

    public void OnDeselect()
    {
        AirFlow.SetActive(false);
        WindowAxis.GetComponent<windowOpenScript>().open = false;
    }
}
