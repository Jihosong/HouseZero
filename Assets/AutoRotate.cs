using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;

public class AutoRotate : MonoBehaviour {

    public bool _rotate;

    void Update()
    {
        if (_rotate)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    public void OnSelection()
    {
        _rotate = true;
    }

    public void OnDeselection()
    {
        _rotate = false;
    }

}
