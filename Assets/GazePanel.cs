//using UnityEngine;
//using HoloToolkit.Examples.InteractiveElements;


//public class GazePanel: MonoBehaviour
//{
//    public bool gaze;
//    GameObject gazeTest;
//    Interactive gazeText;

//    //Use this for initialization
//    void Start()
//    {
//        gazeText = gazeTest.GetComponent<Interactive>();
//        gaze = gazeText.HasGaze;
//    }

//    //Update is called once per frame
//    void Gaze()
//    {
//        if (gaze == true)
//        {
//            GetComponent<TextMesh>().text = "Has Gaze";
//            Debug.Log("Has Gaze");
//        }
//        else
//        {
//            Debug.Log("No Gaze");
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;


public class GazePanel : MonoBehaviour
{
    GameObject sphereTest;

    //Use this for initialization
    void Start()
    {
        sphereTest = GameObject.Find("GazeText");
    }

    //Update is called once per frame
    void Update()
    {
    }

    public void OnGaze()
    {
        sphereTest.SetActive(true);
        GetComponent<TextMesh>().text = "Has Gaze";
        Debug.Log("Has Gaze");
    }

    public void OffGaze()
    {
        sphereTest.SetActive(false);
        Debug.Log("No Gaze");
    }
}

