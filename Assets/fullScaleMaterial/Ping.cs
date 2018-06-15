using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
#if WINDOWS_UWP
using System.Net.NetworkInformation;
#endif

public class Ping : MonoBehaviour {

    public GameObject InternetObject1;
    public static bool hasInternet = false;

	// Use this for initialization
	void Start () {
        if (CheckForInternetConnection())
        {
            InternetObject1.SetActive(true);
            hasInternet = true;
        }
        else
        {
            InternetObject1.SetActive(false);
            hasInternet = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool CheckForInternetConnection()
    {
#if UNITY_EDITOR
        try
        {
            using (var client = new WebClient())
            {
                using (client.OpenRead("http://google.com"))
                {
                    Debug.Log("got internet connection!");
                    return true;
                }
            }
        }
        catch
        {
            Debug.Log("could not get internet connection...");
            return false;
        }
#endif

#if WINDOWS_UWP
        try
        {
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            Debug.Log("Internet connection: " + isInternetConnected);
            return isInternetConnected;
        }
        catch
        {
            Debug.Log("could not get internet connection...");
            return false;
        }
#endif
    }

}
