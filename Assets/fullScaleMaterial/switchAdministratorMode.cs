using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchAdministratorMode : MonoBehaviour {

    public GameObject adminIndicator;
    public static bool administratorOn = false;
    private List<GameObject> hiddenElements = new List<GameObject>();

	// Use this for initialization
	void Start () {
        foreach (GameObject hiddenUI in GameObject.FindGameObjectsWithTag("adminOnlyUI"))
        {
            hiddenElements.Add(hiddenUI);
            hiddenUI.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enableAdministrator()
    {
        adminIndicator.SetActive(true);
        administratorOn = true;
        foreach (GameObject hiddenUI in hiddenElements)
        {
            hiddenUI.SetActive(true);
            Debug.Log("set active one button: " + hiddenUI.name);
        }
    }

    public void disableAdnimistrator()
    {
        adminIndicator.SetActive(false);
        administratorOn = false;
        foreach (GameObject hiddenUI in hiddenElements)
        {
            hiddenUI.SetActive(false);
        }
    }

}
