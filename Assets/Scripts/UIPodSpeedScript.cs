using UnityEngine;
using UnityEngine.UI;

public class UIPodSpeedScript : MonoBehaviour {
    public PodRacerScript pod;
    public Text speedUI;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
                    
        if (pod == null || speedUI == null)
        {
            Destroy(this);                              // this script is destroyed if we didn't specify an UI element and a object to follow
        }
        rb = pod.gameObject.GetComponent<Rigidbody>();
	}

    // Using this to update UI
    private void LateUpdate()
    {
        Vector3 speedS = rb.velocity;
        speedS.y = 0;
        speedUI.text = (int)(speedS.magnitude * 10f * 3.6f) + " km/h";
    }
}
