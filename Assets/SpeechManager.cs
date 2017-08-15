using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour {

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public List<string> names;
    GameObject[] sys;
    private List<Button> btns;

    void Awake()
    {
        sys = GameObject.FindGameObjectsWithTag("sys");

        for (int i = 0; i < sys.Length; i++)
        {
            names.Add(sys[i].name);

        }
    }

	// Use this for initialization
	void Start() {

        foreach (string n in names)
        {
            int i = names.IndexOf(n);

            keywords.Add(n, () =>
            {
                AddButtons.setToggle(sys[i]);

            }
            );
        }
            /*
            keywords.Add("Open", () =>
            {
                var focusObject = GazeGestureManager.Instance.FocusedObject;
                if (focusObject != null)
                {
                    focusObject.SendMessage("OnOpen");
                }
            });

            keywords.Add("Close", () =>
            {
                var focusObject = GazeGestureManager.Instance.FocusedObject;
                if (focusObject != null)
                {
                    focusObject.SendMessage("OnClose");
                }
            });

            //controls the width of opening
            keywords.Add("A Quarter Open", () =>
            {
                var focusObject = GazeGestureManager.Instance.FocusedObject;
                if (focusObject != null)
                {
                    focusObject.SendMessage("OnQuarter");
                }
            });

            keywords.Add("Half Open", () =>
            {
                var focusObject = GazeGestureManager.Instance.FocusedObject;
                if (focusObject != null)
                {
                    focusObject.SendMessage("OnHalf");
                }
            });

            keywords.Add("Three Quarters Open", () =>
            {
                var focusObject = GazeGestureManager.Instance.FocusedObject;
                if (focusObject != null)
                {
                    focusObject.SendMessage("OnThreeQuarters");
                }
            });
        }
        */
       

        /*
        keywords.Add("Dashboard", () =>
        {
            var UIobj = TriggerUI.TriggerObj;
            UIobj.SendMessage("OnDashboard");
        });

        keywords.Add("Hide", () =>
        {
            var UIobj = TriggerUI.TriggerObj;
            UIobj.SendMessage("OnHide");

            var UIpop = GameObject.Find("UIpop");
            UIpop.SendMessage("OnHide");

            //var UIpopout = UIPop.TriggerObj;
            //UIpopout.SendMessage("OnHide");
        });

        
        keywords.Add("Heat Map", () =>
        {
            var floor = GameObject.Find("Quad");
            floor.GetComponent<HeatMap>().OnHeatMap(true);
        });

        keywords.Add("Heat Map Off", () =>
        {
            var floor = GameObject.Find("Quad");
            floor.GetComponent<HeatMap>().OnHeatMap(false);
        });

        keywords.Add("Air Flow", () =>
        {
            //Camera.main.SendMessage("OnCFDToggle");
            Camera.main.GetComponent<CSV3DMainArray>().showCFD = true;
        });

        keywords.Add("Air Flow Off", () =>
        {
            //Camera.main.SendMessage("OnCFDToggle");
            Camera.main.GetComponent<CSV3DMainArray>().showCFD = false;
        });
        */

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }
	
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
