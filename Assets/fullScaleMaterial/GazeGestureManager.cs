using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeGestureManager : MonoBehaviour {

	public static GazeGestureManager Instance { get; private set; }

	GestureRecognizer recognizer;
	public GameObject FocusedObject { get; private set; }

	void Start()
	{
		Instance = this;

		// Set up a GestureRecognizer to detect Select gestures.
		recognizer = new GestureRecognizer();
		recognizer.TappedEvent += (source, tapCount, ray) =>
		{
			// Send an OnSelect message to the focused object and its ancestors.
			Debug.Log("tap event");
			if (FocusedObject != null)
			{
				FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
				//Debug.Log("sent click to " + FocusedObject.name);
			}
		};
		recognizer.StartCapturingGestures();
	}

	// Update is called once per frame
	void Update()
	{
		// Do a raycast into the world based on the user's
		// head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;
		FocusedObject = null;
		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
		{
			FocusedObject = hitInfo.collider.gameObject;
		}


		// If the focused object changed this frame,
		// start detecting fresh gestures again.
		if (FocusedObject == null)
		{
			recognizer.CancelGestures();
			recognizer.StartCapturingGestures();
		}
	}
}
