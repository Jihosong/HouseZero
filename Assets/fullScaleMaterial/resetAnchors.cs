using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class resetAnchors : MonoBehaviour {

    private float timer;
    private bool removing;
    private string[] ids;

    void Start()
    {
        timer = 0;
        removing = false;
    }

    void Update()
    {
        if (removing && Time.time - timer >= 4 && WorldAnchorManager.Instance.AnchorStore.anchorCount == 0)
        {
            for (int i = 0; i < ids.Length; ++i)
            {
                GameObject anchored = GameObject.Find(ids[i]);
                anchored.transform.position = Camera.main.transform.position;
                WorldAnchorManager.Instance.AttachAnchor(anchored.gameObject);
            }
            removing = false;
        }
    }

    public void resetAllAnchors()
    {
        if (switchAdministratorMode.administratorOn && WorldAnchorManager.Instance.AnchorStore.anchorCount > 0)
        {
            ids = new string[WorldAnchorManager.Instance.AnchorStore.anchorCount];
            ids = WorldAnchorManager.Instance.AnchorStore.GetAllIds();
            timer = Time.time;
            removing = true;
            //WorldAnchorManager.Instance.RemoveAllAnchors();
            for (int i = 0; i < ids.Length; ++i)
            {
                WorldAnchorManager.Instance.RemoveAnchor(GameObject.Find(ids[i]).gameObject);
            }
            Debug.Log(WorldAnchorManager.Instance.AnchorStore.anchorCount);
        }
    }

}