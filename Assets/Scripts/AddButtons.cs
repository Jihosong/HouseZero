using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtons : MonoBehaviour {

    [SerializeField]
    private Transform buttonField;

    public GameObject buttonProto;
    private GameObject[] sys;
    public List<GameObject> btns = new List<GameObject>();
    //private List<string> names = new List<string> {"Structure", "Temperature","Floor Heat Map", "Geo-Thermal", "Air Flow", "Robo Windows" };
    public List<string> names = new List<string>();


    void Awake()
    {
        //buttonField = GetComponentInChildren<Canvas>().transform;

        sys = GameObject.FindGameObjectsWithTag("sys");
        
        for (int i=0; i<sys.Length; i++)
        {
            names.Add(sys[i].name);
            sys[i].transform.SetParent(GameObject.Find("ShowSystems").transform);
        }
        

        foreach(string s in names)
        {
            GameObject btn = Instantiate(buttonProto,buttonField,false);
            //Button btn = Instantiate(buttonProto.GetComponent<Button>());        //buttonObj.GetComponent<Button>();
            btn.transform.SetParent(buttonField, false);
            btn.name = s;
            
            btn.GetComponentInChildren<Text>().text = s;
            btns.Add(btn);
        }
    }

    void Start()
    {
        AddListeners();
    }

    void AddListeners()
    {
        foreach(GameObject bo in btns)
        {
            int i = btns.IndexOf(bo);
            Button b = bo.GetComponent<Button>();
            b.onClick.AddListener(delegate { setToggle(sys[i]); });
        }
    }

    
    public static void setToggle(GameObject go)
    {
        //GameObject btnObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        go.SendMessage("On"+go.name);
    }
}
