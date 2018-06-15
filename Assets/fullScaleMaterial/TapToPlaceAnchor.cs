using UnityEngine;
using HoloToolkit.Unity;

public class TapToPlaceAnchor : MonoBehaviour
{
	bool placing = false;
    private bool initialized;

	void Awake() {
        initialized = false;
	}

    private void InitializeAnchor()
    {
        WorldAnchorManager.Instance.AttachAnchor(this.gameObject);
        initialized = true;
        Debug.Log("Added anchor to: " + this.name);
    }

	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect()
	{
        if (!switchAdministratorMode.administratorOn) return;

		// On each Select gesture, toggle whether the user is in placing mode.
		placing = !placing;

		// If the user is in placing mode, display the spatial mapping mesh.
		if (placing)
		{
			HoloToolkit.Unity.SpatialMapping.SpatialMappingManager.Instance.DrawVisualMeshes = true;
			if (WorldAnchorManager.IsInitialized) {
				WorldAnchorManager.Instance.RemoveAnchor (this.gameObject);
			}
		}
		// If the user is not in placing mode, hide the spatial mapping mesh.
		else
		{
			HoloToolkit.Unity.SpatialMapping.SpatialMappingManager.Instance.DrawVisualMeshes = false;
			if (WorldAnchorManager.IsInitialized) {
				WorldAnchorManager.Instance.AttachAnchor (this.gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
        if (!initialized)
        {
            if (WorldAnchorManager.IsInitialized)
            {
                InitializeAnchor();
            }
        }

		// If the user is in placing mode,
		// update the placement to match the user's gaze.

		if (placing)
		{
			// Do a raycast into the world that will only hit the Spatial Mapping mesh.
			var headPosition = Camera.main.transform.position;
			var gazeDirection = Camera.main.transform.forward;

			RaycastHit hitInfo;
			if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
				30.0f, HoloToolkit.Unity.SpatialMapping.SpatialMappingManager.Instance.LayerMask))
			{
				//Debug.Log ("hit spatial mapping");
				// Move this object to
				// where the raycast hit the Spatial Mapping mesh.
				this.transform.position = hitInfo.point;

				// Rotate
				Quaternion toQuat = Quaternion.FromToRotation(Vector3.back, hitInfo.normal);
				toQuat.x = 0;
				toQuat.z = 0;
				this.transform.rotation = toQuat;
			}
		}
	}
}