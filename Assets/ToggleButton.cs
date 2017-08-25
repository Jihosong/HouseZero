using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;


public class ToggleButton : MonoBehaviour
{
    GameObject selectedObj;

    //Use this for initialization
    void Start()
    {
        selectedObj = this.gameObject;
    }

    //Update is called once per frame
    void Update()
    {
    }

    public void OnSelection()
    {
        selectedObj.GetComponent<Renderer>().enabled = true;
    }

    public void OnDeselection()
    {
        selectedObj.GetComponent<Renderer>().enabled = false;
    }
}
