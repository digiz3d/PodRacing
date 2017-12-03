using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PodRacerScriptAlternative : MonoBehaviour {
    public GameObject[] hoverPoints;
    public LayerMask hoverMask; // so we don't Raycast to ourself
    public Text speedIndicator;

    // acceleration settings
    public float forwardAcceleration = 350f;
    public float backwardAcceleration = 100f;

    private float currentForwardAccel = 0f;
    private float currentBackwardAccel = 0f;

    // turn settings
    public float turnForce = 50f;

    private float currentTurnForce = 0f;

    // hover settings
    public float hoverHeight = 0.5f;
    public float hoverForce = 1f;

    // controls
    private bool forward;
    private bool backward;
    private bool left;
    private bool right;


    private Rigidbody rb;

    private void Start () {
        rb = GetComponent<Rigidbody>();
        //podRacersMask = ~(1 << LayerMask.NameToLayer("Pods"));
    }

    private void Update () {
        forward = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow);
        backward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        currentForwardAccel = forward ? forwardAcceleration * rb.mass : 0f;
        currentBackwardAccel = backward ? backwardAcceleration * rb.mass : 0f;

        currentTurnForce = 0f;
        currentTurnForce -= left ? turnForce * rb.mass : 0f;
        currentTurnForce += right ? turnForce * rb.mass : 0f;

        speedIndicator.text =  (int)rb.velocity.magnitude+"";
    }

    private void FixedUpdate()
    {
        foreach(GameObject hoverPoint in hoverPoints)
        {
            Debug.DrawRay(hoverPoint.transform.position, Vector3.down*hoverHeight, Color.red, 0.1f);
            RaycastHit hit;
            if (Physics.Raycast(hoverPoint.transform.position, Vector3.down, out hit, hoverHeight, hoverMask.value)) // if we are close enough to the ground, apply anti-gravity effect
            {
                Debug.Log("touché "+hit.collider.gameObject.name);
                rb.AddForceAtPosition(hit.normal * hoverForce * rb.mass * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
            }
            else // if we are not close to the ground, make that part of the pod fall
            {
                Debug.Log("pas touché");
                rb.AddForceAtPosition(hit.normal * -hoverForce * rb.mass * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
            }
        }

        rb.AddForce(transform.forward * currentForwardAccel);
        rb.AddForce(-transform.forward * currentBackwardAccel);

        rb.AddRelativeTorque(Vector3.up * currentTurnForce);
    }
}
