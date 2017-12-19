using UnityEngine;
using UnityEngine.UI;

public class UIPodSpeedScript : MonoBehaviour {
    public PodRacerScript pod;
    public Text speedUI;
    private Rigidbody rb;

	// Use this for initialization
	public void SetPod(PodRacerScript pod) {
        rb = pod.gameObject.GetComponent<Rigidbody>();
	}

    // Using this to update UI
    private void LateUpdate()
    {
        if (rb != null)
        {
            Vector3 speedS = rb.velocity;
            speedS.y = 0;
            speedUI.text = (int)(speedS.magnitude * 10f * 3.6f) + " km/h";
        }
    }
}
