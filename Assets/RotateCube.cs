using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Examples.InteractiveElements

{
    public class RotateCube : MonoBehaviour
    {

        public float speed = 0f;
        SliderGestureControl slider;
        GameObject house;

        // Update is called once per frame
        void Start()
        {
            house = GameObject.Find("Slider_RotationSpeed");
            slider = house.GetComponent<SliderGestureControl>();
        }
        void Update()
        {
            speed = slider.SliderValue;
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}


