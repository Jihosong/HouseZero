using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLeader : MonoBehaviour {

    LineRenderer line;
    LineRenderer underline;
    Transform end;
    Transform start;
    Transform pivot;

    public float length;
    public bool right;
    public string title;

	// Use this for initialization
	void Start () {

        pivot = this.transform.Find("Pivot").transform;
        underline = pivot.transform.Find("Underline").GetComponent<LineRenderer>();
        end = this.transform.Find("Endpoint").transform;
        line = this.transform.Find("Line").GetComponent<LineRenderer>();

        pivot.Find("Title").GetComponent<TextMesh>().text = title;

        if (right == false) {
        }

        else
        {
            pivot.transform.Find("Underline").transform.localPosition = pivot.transform.Find("Underline").transform.localPosition + new Vector3(-length,0,0);
            pivot.transform.Find("Title").transform.localPosition = pivot.transform.Find("Title").transform.localPosition + new Vector3(-length, 0, 0);
            pivot.transform.Find("Text").transform.localPosition = pivot.transform.Find("Text").transform.localPosition + new Vector3(-length, 0, 0);
        }

        underline.SetPositions(new[] { new Vector3(0, 0, 0), new Vector3(length, 0, 0) });
        line.SetPositions(new[] { new Vector3(0, 0, 0), end.localPosition });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
