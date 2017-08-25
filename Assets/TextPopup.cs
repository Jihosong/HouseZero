using UnityEngine;
using System.Collections;

namespace HoloToolkit.Examples.InteractiveElements
{

    public class TextPopup : InteractiveWidget
    {
        public TextMesh TextField;
        public string TextPop;

        public Vector3 startPosition;
        public Vector3 endPosition;
        public Transform endPoint;
        Vector3 endPointPos;
        public float speed = 10;
        private float startTime;
        private float journeyLength;

        private void Start()
        {
            startPosition = new Vector3(0, 0, 0);
            endPosition = new Vector3(0, 0, 0);

            if (TextField == null)
            {
                TextField = GetComponent<TextMesh>();
            }
        }

        public override void SetState(Interactive.ButtonStateEnum state)
        {
            /*
            string stateString = "";
            switch (state)
            {
                case Interactive.ButtonStateEnum.Selected:
                    stateString = TextPop;
                    break;
            }
            TextField.text = stateString;
            */
        }

        private void Update()
        {
            if (startPosition != endPosition)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, fracJourney);
            }
            
            TextField.text = transform.localPosition == new Vector3(0, 0, 0) ? "" : TextPop;
        }

        public void HoverOut()
        {
            // TextToggle();
            startTime = Time.time;
            startPosition = transform.localPosition;
            endPosition = new Vector3(6, 6, 6);
            journeyLength = Vector3.Distance(startPosition, endPosition);
        }

        public void HoverIn()
        {
            // TextToggle();
            startTime = Time.time;
            startPosition = transform.localPosition;
            endPosition = new Vector3(0, 0, 0);
            journeyLength = Vector3.Distance(startPosition, endPosition);
        }


        public void TextToggle()
        {
        }
    }
}
