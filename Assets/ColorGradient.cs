using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using HoloToolkit.Examples.InteractiveElements;

public class ColorGradient : MonoBehaviour {
    
    private List<p0> CFD = new List<p0>();
    //public Toggle showCG;
    public InteractiveToggle toggleCfd;
    CFD_JSON c;
    static Material QuadMaterial;

    public float s=0.1f;
    //Shader quadShader;

    private GameObject quad;
    private Renderer quadRender;

    public float t;
    private float[] samples;
    private float[] circle;

    private float w, h;
    private int r=1;

    void Start()
    {
        c = GetComponent<CFD_JSON>();
        toggleCfd = GameObject.Find("ToggleColorGradient").GetComponent<InteractiveToggle>();
        //s = 0.1f;

        /*
        quad = GameObject.Find("Quad");
        quadRender = quad.GetComponent<Renderer>();
        w = quadRender.bounds.size.x;
        h = quadRender.bounds.size.y;
        */
        //quadShader = Shader.Find("ColorMap");

        if (c.start == true && toggleCfd.HasSelection == true && c.toggleSection.HasSelection == true)
        {
            //c.waiting.enabled = false;
            CFD = (c.toggleSection.HasSelection == true) ? c.Section() : c.cfd;
        }
    }

    /*
    public ColorGradient(float width, float height, float radius)
    {
        width = w;
        height = h;
        radius = (float)r;
        samples = new float[(int) (w*h)];
        CreateCircleMap();
    }

    void CreateCircleMap()
    {
        circle = new float[(r * r * 4)];
        for(int x=-r; x<r; x++)
        {
            for (int y =-r; y<r; y++)
            {
                float l = (x * x * +y * y) / (float)(r * r);
                float v = 0;
                if (l < 1)
                {
                    v = 1 - l;
                    circle[x + r + r * x * y] = v;
                }
            }
        }
    }

    public void AddPoint(Vector2 pos)
    {
        int px = Mathf.RoundToInt(pos.x);
        int py = Mathf.RoundToInt(pos.y);
        for (int x = -r; x<r; x++)
        {
            for(int y=-r;y<r; y++)
            {
                int ix = px + x;
                int iy = py + y;
                if (ix < 0 || iy < 0 || ix >= w || iy >= h) continue;
                samples[ix + iy * (int)w] += circle[x + r + r * 2 * y];
            }
        }
    }

    public Texture2D GetHeatMap(Gradient g)
    {
        Texture2D tex = new Texture2D((int)w, (int)h, TextureFormat.ARGB32, false);
        float scale = 1 / c.vMax;
        Color[] col = new Color[samples.Length];

        for (int i = 0; i < col.Length; i++) col[i] = g.Evaluate(samples[i] * scale);
        tex.SetPixels(col);
        tex.Apply();
        return tex;
    }

    public void OnHeatMap()
    {
    

    }
    */

    public void OnRenderObject()
    {
        if (c.start == true  && toggleCfd.HasSelection == true)
        {
            // c.waiting.enabled = false;
            CFD = (c.toggleSection.HasSelection == true) ? c.Section() : c.cfd;

            CreateQuadMaterial();
            QuadMaterial.SetPass(0);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.QUADS);

            foreach (p0 p in CFD)
            {
                float a = CFD_JSON.remap(p.vmeg, c.vMin, c.vMax, 0, 1);
                Color vcolor = new Color(a, 1-a, 1-a, 0.5f);
                GL.Color(vcolor);

                Vector3 start = new Vector3(p.x, p.z, p.y);
               
                GL.Vertex3(start.x-s, start.y-s, start.z);
                GL.Vertex3(start.x + s, start.y - s, start.z);
                GL.Vertex3(start.x + s, start.y + s, start.z);
                GL.Vertex3(start.x - s, start.y + s, start.z);
            }

            GL.End();
            GL.PopMatrix();

            /*
            float st = t * (float)(col.Count - 1);
            Color prevC = col[(int)st];
            Color nextC = col[(int)st];
            float newt =  st - (float)((int)st);

            if (t != 1)
            {
                quadRender.material.color = Color.Lerp(prevC, nextC, newt);
            }
            else
            {
                quadRender.material.color = col[col.Count-1];
            }
            */
        }
    }

    static void CreateQuadMaterial()
    {
        if (!QuadMaterial)
        {
            Shader QuadShader = Shader.Find("Hidden/Internal-Colored");
            QuadMaterial = new Material(QuadShader);
            QuadMaterial.hideFlags = HideFlags.HideAndDontSave;

            //shader config: add alpha blending, turn off back face cull and depth writes
            QuadMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            QuadMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            QuadMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            QuadMaterial.SetInt("_ZWrite", 0);
        }
    }
}
