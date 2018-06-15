using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine.Networking;

[System.Serializable]
public class getJSON : MonoBehaviour
{
    public List<p0> cfd = new List<p0>();                                     //list or array?!?!
    public float vMin, vMax, xMin = 1250, xMax;
    public SliderGestureControlNew sliderSection;
    public TextMesh status;
    public InteractiveToggle toggleSection;
    public bool start;
    public bool hasInternet;
    public string url;
    public string localURL;

    IEnumerator Start()
    {
        if ( hasInternet) // Ping.hasInternet )
        {
            // Debug.Log("MAKING HTTP CONNECTION");

            status.text = "LOADING VIA HTTP ...";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            /*
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                status.text = "ERROR NETWORK: " + www.error + " ...";
            }
            if (www.isHttpError)
            {
                Debug.Log(www.error);
                status.text = "ERROR: " + www.error + " ...";

            }
            */

            // Debug.Log("Getting CFD");
            status.text = "GETTING CFD ...";

            cfd = ArrayJson.getJsonArray<p0>(www.downloadHandler.text);
        }

        else
        {
            cfd = ArrayJson.getJsonArray <p0>(Resources.Load<TextAsset>(localURL).text);
        }

        CheckData();
    }
    
    public void CheckData()
    {
        if (cfd.Count != 0)
        {
            float[] vmag = new float[cfd.Count()];
            float[] x = new float[cfd.Count()];
            for (var i = 0; i < cfd.Count(); i++)
            {
                vmag[i] = cfd[i].vmeg;
                x[i] = cfd[i].x;
            }

            vMin = vmag.Min();
            vMax = vmag.Max();
            xMin = x.Min();
            xMax = x.Max();

            start = true;
            status.text = "CFD LOADED!";
            // Debug.Log("CFD loaded");
        }
        else
        {
            status.text = "DOWNLOADING CFD ...";
            start = false;
        }
    }

    public List<p0> Section()
    {
        List<p0> newCFD = new List<p0>();

        //if (cfd.Count != 0 & section.isOn == true)
        if (start == true & toggleSection.HasSelection == true)
        {
            CheckData();
            float position = remap((float)sliderSection.SliderValue, 1, 0, xMin, xMax);
            // float position = 0.5f;

            for (var i = 0; i < cfd.Count(); i++)
            {
                if (cfd[i].x - 0.5f < position && position < cfd[i].x + 0.5f)
                {
                    newCFD.Add(cfd[i]);
                }
            }
        }
        return (newCFD);
    }

    public static float remap(float x, float dataMin, float dataMax, float targetMin, float targetMax)
    {
        return targetMin + (targetMax - targetMin) * (x - dataMin) / (dataMax - dataMin);
    }

}


//............................JSON Parsing..............................
[System.Serializable]
public class ArrayJson
{
    public static List<p0> getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";                          //put a property wrapper around the original array
        var wrapper = JsonUtility.FromJson<Wrapper>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<p0> array = new List<p0>();
    }
}



[System.Serializable]                                                       //should i keep these inside arrayjson or out???
public struct p0
{
    public float x;
    public float y;
    public float z;
    public float vmeg;
    public float vx;
    public float vy;
    public float vz;
    public float t;
}





