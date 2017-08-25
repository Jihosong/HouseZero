using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;

[System.Serializable]
public class CFD_JSON : MonoBehaviour
{
    public List<p0> cfd = new List<p0>();                                     //list or array?!?!
    public float vMin, vMax, yMin=1250, yMax;
    public SliderGestureControl sliderSection; 
    //public Slider slider;
    public TextMesh status;
    //public Toggle section;
    public InteractiveToggle toggleSection;
    public bool start;
    //public bool showSection;
    public string url;

    IEnumerator Start()
    {
        sliderSection = GameObject.Find("SliderSection").GetComponent<SliderGestureControl>();
        //slider = GetComponentInChildren<Slider>();
        //section = GetComponentInChildren<Toggle>();
        //section.isOn = false;
        toggleSection = GameObject.Find("ToggleSection").GetComponent<InteractiveToggle>();
        //toggleSection.HasSelection = false;
        status = GameObject.Find("Status").GetComponent<TextMesh>();
        status.text = "LOADING ...";
        //status.active = false;    

        //string url = "http://140.247.162.82:9000/point";
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);
            status.text = "ERROR: " + www.error + " ...";
            //waiting.enabled = true;
        }
        else
        {
            status.text = "DOWNLOADING CFD ...";
            //waiting.enabled = true;

            cfd = ArrayJson.getJsonArray<p0>(www.text);
            //StartCoroutine(CheckData());
            CheckData();
        }
        //CheckData();
    }

    //IEnumerator CheckData()
    public void CheckData()
    {
        if (cfd.Count != 0)
        {
            float[] vmag = new float[cfd.Count()];
            float[] y = new float[cfd.Count()];
            for (var i = 0; i < cfd.Count(); i++)
            {
                vmag[i] = cfd[i].vmeg;
                y[i] = cfd[i].y;

                //vMin = (cfd[i].vmeg < vMin) ? cfd[i].vmeg : vMin;
                //vMax = (cfd[i].vmeg > vMax) ? cfd[i].vmeg : vMax;
                //yMin = (cfd[i].vmeg < yMin) ? cfd[i].y : yMin;
                //yMax = (cfd[i].vmeg > yMax) ? cfd[i].y : yMax;
            }

             vMin = vmag.Min();
             vMax = vmag.Max();
             yMin = y.Min();
             yMax = y.Max();

            //Debug.Log(cfd[3578].x + "   " + cfd[3578].y + "   " + cfd[3578].z);
            //Debug.Log(cfd[3578].vx + "   " + cfd[3578].vy + "   " + cfd[3578].vz);
            //Debug.Log(yMin + "  " + yMax);

            //yield return start = true;
            start = true;
            status.text = "CFD LOADED!";
        }
        else
        {
            status.text = "DOWNLOADING CFD ...";
            //status.enabled = true;
            //yield return start = false;
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
            float position = remap((float)sliderSection.SliderValue, 1, 0, yMin, yMax);

            for (var i=0; i<cfd.Count(); i++)
            {
                if (cfd[i].y-0.5f < position && position< cfd[i].y + 0.5f)
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
}





