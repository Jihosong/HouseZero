using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSystem : MonoBehaviour {
    private bool showing;
    private MeshRenderer[] R;
    public Color highlight = Color.red;

    void Start()
    {
        R = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < R.Length; i++)
        {
            R[i].enabled = false;
        }
    }

    public void OnStructure()
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
