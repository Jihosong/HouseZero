using UnityEngine;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity.InputModule;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// GestureManager creates a gesture recognizer and signs up for a tap gesture.
    /// When a tap gesture is detected, GestureManager uses GazeManager to find the game object.
    /// GestureManager then sends a message to that game object.
    /// </summary>
    [RequireComponent(typeof(GazeManager))]
    public partial class GestureManager : Singleton<GestureManager>
    {
        /// <summary>
        /// To select even when a hologram is not being gazed at,
        /// set the override focused object.
        /// If its null, then the gazed at object will be selected.
        /// </summary>
        public GameObject OverrideFocusedObject
        {
            get; set;
        }

        /// <summary>
        /// Gets the currently focused object, or null if none.
        /// </summary>
        public GameObject FocusedObject
        {
            get { return focusedObject; }
        }

        private GestureRecognizer gestureRecognizer;
        private GameObject focusedObject;

        void Start()
        {
            // Create a new GestureRecognizer. Sign up for tapped events.
            gestureRecognizer = new GestureRecognizer();
            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

            gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;

            // Start looking for gestures.
            gestureRecognizer.StartCapturingGestures();
        }

        private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
        {
            if (focusedObject != null)
            {
                focusedObject.SendMessage("OnSelect");
            }
        }

        void LateUpdate()
        {
            GameObject oldFocusedObject = focusedObject;

            if (GazeManager.Instance.IsGazingAtObject &&
                OverrideFocusedObject == null &&
                GazeManager.Instance.HitInfo.collider != null)
            {
                // If gaze hits a hologram, set the focused object to that game object.
                // Also if the caller has not decided to override the focused object.
                focusedObject = GazeManager.Instance.HitInfo.collider.gameObject;
                //focusedObject = GazeManager.Instance.HitInfo.collider.gameObject;
            }
            else
            {
                // If our gaze doesn't hit a hologram, set the focused object to null or override focused object.
                focusedObject = OverrideFocusedObject;
            }

            if (focusedObject != oldFocusedObject)
            {
                // If the currently focused object doesn't match the old focused object, cancel the current gesture.
                // Start looking for new gestures.  This is to prevent applying gestures from one hologram to another.
                gestureRecognizer.CancelGestures();
                gestureRecognizer.StartCapturingGestures();
            }
        }

        void OnDestroy()
        {
            gestureRecognizer.StopCapturingGestures();
            gestureRecognizer.TappedEvent -= GestureRecognizer_TappedEvent;
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.VR.WSA.Input;
//using HoloToolkit.Unity.InputModule;

//public class GestureManager : MonoBehaviour
//{

//    public static GestureManager Instance;
//    public bool isManipulating;

//    private GestureRecognizer Recognizer;

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }

//        isManipulating = false;
//        Recognizer = new GestureRecognizer();
//        //click
//        Recognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

//        //the event to listen to
//        Recognizer.ManipulationStartedEvent += Started;
//        Recognizer.ManipulationUpdatedEvent += Updated;
//        Recognizer.ManipulationCanceledEvent += Ended;
//        Recognizer.ManipulationCompletedEvent += Ended;

//        Recognizer.StartCapturingGestures();
//    }

//    //unsubscribe
//    private void OnDestroy()
//    {
//        Recognizer.ManipulationStartedEvent -= Started;
//        Recognizer.ManipulationUpdatedEvent -= Updated;
//        Recognizer.ManipulationCanceledEvent -= Ended;
//        Recognizer.ManipulationCompletedEvent -= Ended;
//    }

//    private void Started(InteractionSourceKind source, Vector3 position, Ray headRay)
//    {
//        if (SimpleCursor.Instance.FocusedObj != null)
//        {
//            GameObject fobj = SimpleCursor.Instance.FocusedObj;

//            isManipulating = true;

//            if (fobj.GetType() == typeof(UnityEngine.UI.Button))
//            {

//                AddButtons.setToggle(fobj);
//            }
//        }
//    }

//    private void Updated(InteractionSourceKind source, Vector3 position, Ray headRay)
//    {
//        if (SimpleCursor.Instance.FocusedObj != null)
//        {
//            GameObject fobj = SimpleCursor.Instance.FocusedObj;

//            isManipulating = true;

//            if (fobj.GetType() == typeof(UnityEngine.UI.Button))
//            {

//                AddButtons.setToggle(fobj);
//            }
//        }
//    }

//    private void Ended(InteractionSourceKind source, Vector3 position, Ray headRay)
//    {
//        isManipulating = false;
//    }

//    public void Stop()
//    {
//        Recognizer.StopCapturingGestures();
//    }
//}
