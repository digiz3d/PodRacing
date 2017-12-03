using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PodRacerScript : MonoBehaviour {
    public GameObject[] hoverPoints;
    public LayerMask hoverMask; // so we don't Raycast to ourself
    public Text speedIndicator;

    #region Acceleration settings
    public AnimationCurve accelCurve;
    public AnimationCurve brakeCurve;
    public AnimationCurve speedCurve;
    public float timeToFullspeed = 20.0f;
    public float timeToFullAcceleration = 1f;

    private float accelFactor = 0f;
    private float brakeFactor = 0f;
    private float speedFactor = 0f;
    private float speed = 0f;
    private Dictionary<int, float> speedCurveApproximation;         // key = speed in m/s , value = factor from 0f to 1f;
    private float speedCurveApproximationPrecision = 0.0000001f;    // lower =  more accurate speeds but slower loading times
    #endregion

    // turn settings
    public float maxTurnSpeed = 5f;
    public float turnSpeedFactor = 2.0f;
    private float currentTurnSpeed = 0f;

    // hover settings
    private float hoverHeight = 0.5f;
    private float hoverForce = 10f;

    // controls
    private bool forward;
    private bool brake;
    private bool left;
    private bool right;

    private Rigidbody rb;

    private void Start () {
        rb = GetComponent<Rigidbody>();
        
        #region Speed Approximation
        speedCurveApproximation = new Dictionary<int, float>();

        int speed = 0;
        float lastFactor = 0f;
        while(speed <= (speedCurve.Evaluate(1f)*10))
        {
            float factor = GetFactorForSpeed(speed, ref lastFactor);
            lastFactor = factor;
            speedCurveApproximation.Add(speed, factor);
            speed += 1;
            Debug.Log("<color=green>added "+ speed +" = "+ factor +"</color>");
        }
        Debug.Log("speed 238 m/s (or 23.8 unity units/s) is achieved at ratio " + GetRatioForSpeed(238));
        #endregion
    }


    private void Update () {
        forward = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow);
        brake = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (forward)
        {
            accelFactor += Time.deltaTime / timeToFullAcceleration;                                      // timeToFullAcceleration to reach max acceleration
            accelFactor = Mathf.Clamp(accelFactor, 0f, 1f);
            speedFactor += accelCurve.Evaluate(accelFactor) * Time.deltaTime / timeToFullspeed;   // timeToFullspeed to reach max speed (at max acceleration)
            speedFactor = Mathf.Clamp(speedFactor, 0f, 1f);
            speed = speedCurve.Evaluate(speedFactor);
        }
        else
        {
            accelFactor -= Time.deltaTime / timeToFullAcceleration;                                      // timeToFullAcceleration to loose acceleration. could be another number
            accelFactor = Mathf.Clamp(accelFactor, 0f, 1f);
            speed *= 0.9994f;
            speedFactor = GetRatioForSpeed((int)(speed*10f));                                    // get the approximated speed factor
            speedFactor = Mathf.Clamp(speedFactor, 0f, 1f);
        }

        if (left)
        {
            currentTurnSpeed -= Time.deltaTime * turnSpeedFactor;
            currentTurnSpeed = Mathf.Clamp(currentTurnSpeed, -maxTurnSpeed, maxTurnSpeed);
        }
        if (right)
        {
            currentTurnSpeed += Time.deltaTime * turnSpeedFactor;
            currentTurnSpeed = Mathf.Clamp(currentTurnSpeed, -maxTurnSpeed, maxTurnSpeed);
        }
        if (!left && !right)
        {
            currentTurnSpeed *= 0.95f;
        }
        
    }

    private void FixedUpdate()
    {
        /*
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
        */
        rb.velocity = transform.forward *speed;
        transform.eulerAngles += new Vector3(0f, currentTurnSpeed, 0);
        /*
        if (currentBrakeFactor > 0f)
        {
            if (rb.velocity.z > 0f)
            {
                rb.velocity -= new Vector3(0f, 0f, brakeCurve.Evaluate(currentBrakeFactor));
                Debug.Log(brakeCurve.Evaluate(currentBrakeFactor));
            }
            Debug.Log("on freine !");
        }

        rb.AddRelativeTorque(Vector3.up * currentTurnForce);
        */
    }

    private void LateUpdate()
    {
        speedIndicator.text = (int)(rb.velocity.magnitude * 10*3.6) + " km/h";
    }




    #region Speed Approximation methods
    private float GetFactorForSpeed(int speed, ref float factor)
    {
        float speedFromCurve = speedCurve.Evaluate(factor) * 10;
        while (speed > speedFromCurve)
        {
            factor += speedCurveApproximationPrecision;
            speedFromCurve = speedCurve.Evaluate(factor) * 10;
        }
        return factor;
    }

    private float GetRatioForSpeed(int speed)
    {
        while (!speedCurveApproximation.ContainsKey(speed))
        {
            speed -= 1;
        }
        //Debug.Log("returning the closest lower speed ratio : " + speed);
        return speedCurveApproximation[speed];
    }
    #endregion
}
