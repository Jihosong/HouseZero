using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GetRequest : MonoBehaviour
{
    IEnumerator Start()
    {
        string url = "http://samples.openweathermap.org/data/2.5/weather?id=2172797&appid=b1b15e88fa797225412429c1c50c122a1";
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.Log("error");
        }
        else
        {
            //JsonProcess(www.text);
            string jString = www.text;

            //DataInfo wData = new DataInfo();
            //wData.CreateFromJSON(jString);

            DataInfo wData = DataInfo.CreateFromJSON(jString);
            Debug.Log("id "+wData.id +"   "+ "name "+wData.name + "   ");
            Debug.Log("lat " + wData.coord.lat);
        }
    }

    /*
    private void JsonProcess(string json)
    {
        json
    }
    */

}

[System.Serializable]
public class DataInfo
{
    public Coord coord;
    public string name;
    public int id;

    public static DataInfo CreateFromJSON(string json)                             //not using static (all instance of datainfo will hsare the same json) 
    {                                                                       //cuz in the future might be multiple instances, each with its own web call/json file
        return JsonUtility.FromJson<DataInfo>(json);
    }
}

[System.Serializable]
public class Coord                                                         //for nested JSON structure...but maybe use a library to auto parse?
{
    public float lon;
    public float lat;
}



