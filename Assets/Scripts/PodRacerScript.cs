using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PodRacerScript : MonoBehaviour {
    public GameObject[] hoverPoints;
    public LayerMask hoverMask; // so we don't Raycast to ourself

    // acceleration settings
    private float forwardAcceleration = 3500f;
    private float backwardAcceleration = 1000f;

    private float currentForwardAccel = 0f;
    private float currentBackwardAccel = 0f;

    // turn settings
    private float turnForce = 50f;

    private float currentTurnForce = 0f;
    
    // hover settings
    private float hoverHeight = 0.5f;
    private float hoverForce = 1500f;

    // controls
    private bool forward;
    private bool backward;
    private bool left;
    private bool right;


    private Rigidbody rbody;

    private void Start () {
        rbody = GetComponent<Rigidbody>();
        //podRacersMask = ~(1 << LayerMask.NameToLayer("Pods"));
    }

    private void Update () {
        forward = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow);
        backward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        currentForwardAccel = forward ? forwardAcceleration : 0f;
        currentBackwardAccel = backward ? backwardAcceleration : 0f;

        currentTurnForce = 0f;
        currentTurnForce -= left ? turnForce : 0f;
        currentTurnForce += right ? turnForce : 0f;
    }

    private void FixedUpdate()
    {
        foreach(GameObject hoverPoint in hoverPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(hoverPoint.transform.position, Vector3.down, out hit, hoverHeight, hoverMask.value)) // if we are close enough to the ground, apply anti-gravity effect
            {
                Debug.Log("touché "+hit.collider.gameObject.name);
                rbody.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
            }
            else // if we are not close to the ground, make that part of the pod fall
            {
                Debug.Log("pas touché");
 
                    rbody.AddForceAtPosition(Vector3.up * Physics.gravity.y * (rbody.mass / hoverPoints.Length), hoverPoint.transform.position); // apply gravity toward down
            }
        }

        rbody.AddForce(transform.forward * currentForwardAccel);
        rbody.AddForce(-transform.forward * currentBackwardAccel);

        rbody.AddRelativeTorque(Vector3.up * currentTurnForce);
    }
}
