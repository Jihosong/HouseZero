using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoThermalSystem : MonoBehaviour {

    private bool showing;
    private MeshRenderer[] R;
    public Color highlight = Color.cyan;

    void Awake()
    {
        R = GetComponentsInChildren<MeshRenderer>();
        for (int i=0; i<R.Length; i++)
        {
            R[i].enabled = false;
        }
    }

    public void OnGeoThermal()
    {
        if (!showing)
        {
            for (int i = 0; i < R.Length; i++)
            {
                R[i].material.color = highlight;
                R[i].enabled = true;
            }
            showing = true;
        }
        else
        {
            for (int i = 0; i < R.Length; i++)
            {
                R[i].enabled = false;
            }
            showing = false;
        }
    }
}
