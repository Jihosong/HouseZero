using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance { get; private set; }
    public GameObject FocusedObject { get; private set; }
    public GameObject OverrideFocusedObject { get; internal set; }

    public delegate void FocusedAction();
    public static event FocusedAction OnFocused;

    //public delegate void WindowFocusedAction();
    //public static event WindowFocusedAction OnFocusedWindow;

    GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {

        Instance = this;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };

        recognizer.StartCapturingGestures();
	}
	
	void Update () {

        GameObject oldFocusObject = FocusedObject;

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }


        Vector2 headPos2D = new Vector2(headPosition.x, headPosition.y);
        Vector2 gazeDir2D = new Vector2(gazeDirection.x, gazeDirection.y);

        RaycastHit2D hit2D = Physics2D.Raycast(headPos2D, gazeDir2D);

        if (hit2D.collider != null)
        {
            FocusedObject = hit2D.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }


        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();

            if (OnFocused != null)
                OnFocused();
        }
        
	}
}
