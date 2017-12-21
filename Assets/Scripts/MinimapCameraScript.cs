using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraScript : MonoBehaviour {
    public Transform followedTransform;

    private bool rotations = true;

    //used in order to hide the shadows on the minimap
    private ShadowQuality savedShadowQuality;

	// Update is called once per frame
	void LateUpdate () {
        transform.position = followedTransform.position + new Vector3(0f, 3f, 0f);
        if (rotations)
        {
            transform.eulerAngles = new Vector3(90, followedTransform.eulerAngles.y, 0);
        }
	}

    private void OnPreRender()
    {
        savedShadowQuality = QualitySettings.shadows;
        QualitySettings.shadows = ShadowQuality.Disable;
    }

    private void OnPostRender()
    {
        QualitySettings.shadows = savedShadowQuality;
    }

    public void Follow(Transform t)
    {
        followedTransform = t;
    }
}
