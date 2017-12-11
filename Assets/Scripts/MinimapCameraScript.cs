using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraScript : MonoBehaviour {
    public Transform playerPodTransform;
	// Update is called once per frame
	void LateUpdate () {
        transform.position = playerPodTransform.position + new Vector3(0f, 2f, 0f);
	}
}
