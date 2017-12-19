using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        PodRacerScript podRacer = other.gameObject.GetComponent<PodRacerScript>();
        if (podRacer != null)
        {
            podRacer.PassFinishLine();
        }
    }
}
