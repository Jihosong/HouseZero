using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSystem : MonoBehaviour {

    private bool showing;
    public Color highlight = Color.magenta;
    private Canvas[] R;
    //private GameObject sensor;
    private MeshRenderer[] sR;

    void Awake()
    {

    }

    void Start()
    {
        sR = GetComponentsInChildren<MeshRenderer>();
        for (int j = 0; j < sR.Length; j++)
        {
            sR[j].enabled = true;
            //print(sR[j].name);
        }

        R = GetComponentsInChildren<Canvas>();
        for (int i = 0; i < R.Length; i++)
        {
            R[i].enabled = false;
            //print(R[i].name);
        }
    }

    public void OnSensorData()
    {
        if (!showing)
        {
            for (int i = 0; i < R.Length; i++)
            {
                R[i].enabled = true;
            }

            for (int j = 0; j < sR.Length; j++)
            {
                sR[j].material.color = highlight;
            }

            showing = true;
        }
        else
        {
            for (int i = 0; i < R.Length; i++)
            {
                R[i].enabled = false;
            }

            for (int j = 0; j < sR.Length; j++)
            {
                sR[j].material.color = Color.white;
            }
            showing = false;
        }
    }
}
