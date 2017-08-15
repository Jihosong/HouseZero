using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCursor : MonoBehaviour {

    public static SimpleCursor Instance;
    private GameObject cursorObj;
    private float focusDist;
    public GameObject FocusedObj;
    private MeshRenderer meshRenderer;
    public float n=3;
    public float s = 0.2f;

    void Awake () {

	    if (Instance == null)
        {
            Instance = this;
        }

        cursorObj = this.gameObject;
	}

    void Start()
    {
        focusDist = Camera.main.farClipPlane / n;
        Vector3 focusTranslate = focusDist * Camera.main.transform.forward;
        Instance.gameObject.transform.position = Camera.main.transform.position+ focusTranslate;
        transform.localScale = new Vector3(s, s, s);
        //meshRenderer = cursorObj.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObj = hitInfo.collider.gameObject;
            
            transform.position = hitInfo.point;
            float factor = hitInfo.point.magnitude / (Camera.main.transform.position+ focusDist * Camera.main.transform.forward).magnitude;

            transform.localScale = new Vector3(factor*s, factor*s, factor*s);
            
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            FocusedObj = null;

            Vector3 focusTranslate = focusDist * Camera.main.transform.forward;

            transform.position = Camera.main.transform.position +focusTranslate;
            transform.localScale = new Vector3(s, s, s);
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            //meshRenderer.enabled = false;
        }
	}
}
