using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;

public class cfdControl : MonoBehaviour {

    InteractiveToggle miniCFD;
    InteractiveToggle roomCFD;

    // Use this for initialization
    void Start () {
        miniCFD = GameObject.Find("ToggleVectorField").GetComponent<InteractiveToggle>();
        roomCFD = GameObject.Find("ToggleRoom").GetComponent<InteractiveToggle>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    // mini CFD section on
    public void miniCFDsection()
    {
        if (miniCFD.HasSelection == false)
        {
            miniCFD.HasSelection = true;
        }
    }

    public void onMiniCFD()
    {
        if (roomCFD.HasSelection == true)
        {
            roomCFD.HasSelection = false;
        }
    }

    public void onRoomCFD()
    {
        if (miniCFD.HasSelection == true)
        {
            miniCFD.HasSelection = false;
        }
    }
}
