using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Examples.InteractiveElements

{
    public class ScalePlaceable : MonoBehaviour
    {

        public float scale;
        SliderGestureControl scaleSlider;
        GameObject scaleObj;
        GameObject house;

        // Update is called once per frame
        void Start()
        {
            scaleObj = GameObject.Find("Slider_ScalePlaceable");
            scaleSlider = scaleObj.GetComponent<SliderGestureControl>();
            house = this.gameObject;
            scaleSlider.SliderValue = 1f;
        }

        void Update()
        {
            scale = scaleSlider.SliderValue;
            house.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
