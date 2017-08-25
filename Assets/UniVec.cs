//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class UniVec : MonoBehaviour {

//    private List<p0> CFD = new List<p0>();
//    static Material lineMaterial;
//    public Toggle showUV;
//    CFD_JSON c;
//    public float len;

//    void Start()
//    {
//        c = GetComponent<CFD_JSON>();
//        showUV = GameObject.Find("Uniformed Vector").GetComponent<Toggle>();
//    }

//    public void OnRenderObject()
//    {
//        if (c.start == true && showUV.isOn == true)
//        {
//            //CheckData();
//            c.waiting.enabled = false;

//            CFD = (c.section.isOn == true) ? c.Section() : c.cfd;

//            CreateLineMaterial();
//            lineMaterial.SetPass(0);

//            GL.PushMatrix();
//            GL.Begin(GL.LINES);

//            foreach (p0 p in CFD)
//            {
//                float a = CFD_JSON.remap(p.vmeg, c.vMin, c.vMax, 0, 1);
//                Color vcolor = new Color(a, 1 - a, 0, 0.5f);
//                GL.Color(vcolor);

//                Vector3 start = new Vector3(p.x, p.z, p.y);
//                Vector3 move = new Vector3(p.vx, p.vz, p.vy);
//                Vector3 end = start + move.normalized*len;

//                Vector3 proj = Vector3.ProjectOnPlane(end, Vector3.Cross(start, end));
//                //Vector3 projN = len*Vector3.Normalize(proj);

//                GL.Vertex3(start.x, start.y, start.z);
//                GL.Vertex3(proj.x, proj.y, proj.z);
//            }

//            GL.End();
//            GL.PopMatrix();
//        }

//        /*
//        else
//        {
//            c.waiting.text = "waiting to download data";
//            c.waiting.enabled = true;
//            //Debug.Log("waiting for connection");
//        }
//        */
//    }

//    // functions...
//    static void CreateLineMaterial()
//    {
//        if (!lineMaterial)
//        {
//            Shader lineShader = Shader.Find("Hidden/Internal-Colored");
//            lineMaterial = new Material(lineShader);
//            lineMaterial.hideFlags = HideFlags.HideAndDontSave;

//            //shader config: add alpha blending, turn off back face cull and depth writes
//            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
//            lineMaterial.SetInt("_ZWrite", 0);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Examples.InteractiveElements;
using System;

public class UniVec : MonoBehaviour
{

    private List<p0> CFD = new List<p0>();
    static Material lineMaterial;
    //public Toggle showUV;
    InteractiveToggle toggleCfd;
    //GameObject toggleOn;
    CFD_JSON c;
    public float len;

    void Start()
    {

        c = GetComponent<CFD_JSON>();
        //showUV = GameObject.Find("Uniformed Vector").GetComponent<Toggle>();
        toggleCfd = GameObject.Find("ToggleUniVec").GetComponent<InteractiveToggle>();
    }

    public void OnRenderObject()
    {
        if (c.start == true && toggleCfd.HasSelection == true)
        {
            //CheckData();
            // c.waiting.enabled = false;
            CFD = (c.toggleSection.HasSelection == true) ? c.Section() : c.cfd;

            CreateLineMaterial();
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);

            foreach (p0 p in CFD)
            {
                float a = CFD_JSON.remap((float)Math.Pow(p.vmeg, .5), (float)Math.Pow(c.vMin, .5), (float)Math.Pow(c.vMax, .5), 0, 1);
                Color vcolor = new Color(a, 1 - a, 0, 0.5f);
                GL.Color(vcolor);

                Vector3 start = new Vector3(p.x, p.z, p.y);
                Vector3 move = new Vector3(p.vx, p.vz, p.vy);
                Vector3 end = start + move.normalized * len;

                Vector3 proj = Vector3.ProjectOnPlane(end, Vector3.Cross(start, end));
                //Vector3 projN = len*Vector3.Normalize(proj);

                GL.Vertex3(start.x, start.y, start.z);
                GL.Vertex3(proj.x, proj.y, proj.z);
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

