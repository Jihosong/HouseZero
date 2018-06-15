using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;
using System;

public class vectorField : MonoBehaviour
{

    private List<p0> CFD = new List<p0>();
    static Material lineMaterial;

    public InteractiveToggle toggleCfd;
    public InteractiveToggle toggleSection;
    public getJSON c;
    public float r;
    private float frac;

    void Start()
    {
        // toggleCfd = GameObject.Find("ToggleVectorField").GetComponent<InteractiveToggle>();
        // section = GameObject.Find("ToggleSection").GetComponent<InteractiveToggle>();
        // control = GameObject.Find("CfdControl").GetComponent<toggleControl>();
    }

    public void OnRenderObject()
    {
        if (c.start == true && toggleCfd.HasSelection == true)
        {
            //CheckData();
            // c.waiting.enabled = false;
            CFD = (toggleSection.HasSelection == true) ? c.Section() : c.cfd;
            // CFD = c.cfd;

            CreateLineMaterial();
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);

            // Cycle time: the interval is equal to the remainder of the time in seconds divided by 1.
            float frac = (Time.time) % 1.0f;

            foreach (p0 p in CFD)
            {
                // float a = CFD_JSON.remap((float)Math.Pow(p.vmeg,.4), (float)Math.Pow(c.vMin,.4), (float)Math.Pow(c.vMax,.4), 0, 1);
                Color vcolor = Color.Lerp( new Color(1, 0.2f, 0), new Color(0, 0.5f, 1), (p.t-293)/17);
                GL.Color(vcolor);

                // Create the start and end positions of the vector
                Vector3 start = new Vector3(p.x, p.z, p.y);
                Vector3 v = new Vector3(p.vx, p.vz, p.vy);

                // We adjust the length of the vector by a factor R
                Vector3 end = start + r * v;

                // Interpolate the animation
                Vector3 start2 = Vector3.Lerp(end, start, frac);
                Vector3 end2 = start2 + v.normalized * 0.15f;
                GL.Vertex3(start2.x, start2.y, start2.z);
                GL.Vertex3(end2.x, end2.y, end2.z);
            }

            GL.End();
            GL.PopMatrix();
        }

        /*
        else
        {
            c.waiting.text = "waiting to download data";
            c.waiting.enabled = true;
            //Debug.Log("waiting for connection");
        }
        */
    }

    // functions...
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            Shader lineShader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(lineShader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;

            //shader config: add alpha blending, turn off back face cull and depth writes
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }
}

