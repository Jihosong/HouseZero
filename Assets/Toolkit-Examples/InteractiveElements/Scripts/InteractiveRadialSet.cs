using UnityEngine;
using UnityEngine.Events;

namespace HoloToolkit.Examples.InteractiveElements
{
    public class InteractiveRadialSet : MonoBehaviour
    {

        public InteractiveToggle[] Interactives;

        public int SelectedIndex = 0;

        public UnityEvent OnSelectionEvents;

        private bool mHasInit = false;
        internal InteractiveRadialSet hasSelection;

        private void Start()
        {
            for (int i = 0; i < Interactives.Length; ++i)
            {
                int itemIndex = i;
                // add selection event handler to each button
                Interactives[i].OnSelectEvents.AddListener(() => HandleOnSelection(itemIndex));
                Interactives[i].AllowDeselect = false;
            }

            HandleOnSelection(SelectedIndex);
        }

        /// <param name="index"></param>
        public void SetSelection(int index)
        {
            if (!isActiveAndEnabled ||
                (index < 0 || Interactives.Length <= index))
            {
                return;
            }

            Interactives[index].OnInputClicked(null);
        }

        /// <param name="index"></param>
        private void HandleOnSelection(int index)
        {
            for (int i = 0; i < Interactives.Length; ++i)
            {
                if (i != index)
                {
                    Interactives[i].HasSelection = false;
                }
            }

            if (!mHasInit)
            {
                Interactives[index].HasSelection = true;
                mHasInit = true;
            }

            SelectedIndex = index;

            OnSelectionEvents.Invoke();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < Interactives.Length; ++i)
            {
                int itemIndex = i;
                Interactives[i].OnSelectEvents.RemoveListener(() => HandleOnSelection(itemIndex));
            }
        }
    }
}
