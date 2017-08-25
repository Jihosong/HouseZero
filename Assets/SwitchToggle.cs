using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine.Events;

public class SwitchToggle : MonoBehaviour {
    
    //
    UnityEvent togSwitchVec;
    UnityEvent togSwitchVecOff;

    UnityEvent togSwitchUni;
    UnityEvent togSwitchUniOff;

    UnityEvent togSwitchCol;
    UnityEvent togSwitchColOff;

    //
    InteractiveToggle togButtonVec;
    InteractiveToggle togButtonVecOff;

    InteractiveToggle togButtonUni;
    InteractiveToggle togButtonUniOff;

    InteractiveToggle togButtonCol;
    InteractiveToggle togButtonColOff;

    //
    InteractiveRadialSet selectedIdx;
    int IndexToggle;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleSwitch()
    {
        togButtonVec = GameObject.Find("ToggleVectorField").GetComponent<InteractiveToggle>();
        togButtonVecOff = GameObject.Find("ToggleVectorField").GetComponent<InteractiveToggle>();
        togSwitchVec = togButtonVec.OnSelection;
        togSwitchVecOff = togButtonVec.OnDeselection;

        togButtonUni = GameObject.Find("ToggleUniVec").GetComponent<InteractiveToggle>();
        togButtonUniOff = GameObject.Find("ToggleUniVec").GetComponent<InteractiveToggle>();
        togSwitchUni = togButtonUni.OnSelection;
        togSwitchUniOff = togButtonUni.OnDeselection;

        togButtonCol = GameObject.Find("ToggleColorGradient").GetComponent<InteractiveToggle>();
        togButtonColOff = GameObject.Find("ToggleColorGradient").GetComponent<InteractiveToggle>();
        togSwitchCol = togButtonCol.OnSelection;
        togSwitchColOff = togButtonCol.OnDeselection;

        selectedIdx = GameObject.Find("ToggleSwitch").GetComponent<InteractiveRadialSet>();
        IndexToggle = selectedIdx.SelectedIndex;

        if (IndexToggle == 0)
        { 
        togSwitchVec.Invoke();
        togSwitchVecOff.Invoke();
        }
        else if (IndexToggle == 1)
        {
        togSwitchUni.Invoke();
        togSwitchUniOff.Invoke();
        }
        else if (IndexToggle == 2)
        {
        togSwitchCol.Invoke();
        togSwitchColOff.Invoke();
        }
    }
}
