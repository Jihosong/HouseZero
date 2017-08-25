using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;


public class ToggleSection : MonoBehaviour
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

    public void OnSelect()
    {
        selectedObj.SetActive(false);
    }

    public void OnDeselect()
    {
        selectedObj.SetActive(true);
    }
}
