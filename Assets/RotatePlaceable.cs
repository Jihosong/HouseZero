using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;

public class RotatePlaceable : MonoBehaviour
{
    public bool toggleRotate;
    public float rotateDegree = 0f;
    SliderGestureControl rotateSlider;
    GameObject house;

    // Update is called once per frame
    void Start()
    {
        house = GameObject.Find("Slider_RotatePlaceable");
        rotateSlider = house.GetComponent<SliderGestureControl>();
    }

    void Update()
    {
        if (toggleRotate)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
        else
        {
            rotateDegree = rotateSlider.SliderValue;
            rotateDegree += Input.GetAxis("Horizontal");
            transform.eulerAngles = new Vector3(0, rotateDegree, 0);
        }
    }

    public void OnSelection()
    {
        toggleRotate = true;
    }

    public void OnDeselection()
    {
        toggleRotate = false;
    }
}
