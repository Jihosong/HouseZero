using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeatMapSystem : MonoBehaviour {

    private Shader heatmapShader;
    //private GameObject[] floors;
    private MeshRenderer[] R;
    public float[] sensors = new float[2];
    public bool showing = false;
    public Material floorMat;

    // Use this for initialization
    void Start()
    {
        //heatmapShader = Shader.Find("HeatMap_Linked");
        //floorMat = GetComponent<Material>();

        //floors = GameObject.FindGameObjectsWithTag("quad");

        R = GetComponentsInChildren<MeshRenderer>();
        print(R.Count());
        floorMat.SetColor("_Color1", Color.white);
        floorMat.SetColor("_Color2", Color.white);

        for (int i=0; i < R.Count(); i++){
            R[i].material = floorMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //gaze toggle
        if (showing == true)
        {
            if (GazeGestureManager.Instance.FocusedObject == floor)
            {
                OnHeatMap(true);
            }
            else
            {
                OnHeatMap(false);
            }
        }
        */
    }

    public static float remap(float x, float dataMin, float dataMax, float targetMin, float targetMax)
    {
        return targetMin + (targetMax - targetMin) * (x - dataMin) / (dataMax - dataMin);
    }


    public void OnFloorHeatMap()
    {
        if (!showing)
        {
            float smin = remap(sensors.Min(), sensors.Min(), sensors.Max(), 0.25f, 0.8f);
            float smax = remap(sensors.Max(), sensors.Min(), sensors.Max(), 0.25f, 0.8f);

            Color colMin = new Color((1 - smax), 0.5f, 0.5f, 1);
            Color colMax = new Color(0.9f, 0.6f, (1 - smin), 1);

            floorMat.SetColor("_Color1", colMin);

            floorMat.SetColor("_Color2", colMax);

            showing = true;
        }
        else
        {
            floorMat.SetColor("_Color1", Color.white);
            floorMat.SetColor("_Color2", Color.white);
            showing = false;
        }
    }
}
