using System.Collections;
using UnityEngine;

public class RaceScript : MonoBehaviour {
    public Transform spawn;
    public int laps = 1;
    public CameraScript cameraScript;
    public UIPodSpeedScript uiPodScript;
    public MinimapCameraScript minimapScript;

    private Pod pod;
    private GameObject playerPod;
    private PodRacerScript podRacerScript;

    public int currentLap = 0;
    public int checkedpoints = 0;
    public CheckpointScript[] checkpoints;

    // Use this for initialization
    private void Start () {
        pod = PodManager.instance.GetSelectedPod();
        SpawnPod();
        StartCoroutine(RaceLoop());
    }

    private void SpawnPod()
    {
        playerPod = Instantiate(pod.prefab, spawn, true);
        podRacerScript = playerPod.GetComponent<PodRacerScript>();
        podRacerScript.DisableControls();
        podRacerScript.SetPodCamera(cameraScript);
        podRacerScript.SetPod(pod);
        cameraScript.SetAimPoint(playerPod.transform.GetChild(1));
        cameraScript.SetObjectToFocus(playerPod);
        uiPodScript.SetPod(podRacerScript);
        minimapScript.playerPodTransform = playerPod.transform;
    }

    public void PassFinishLine()
    {
        currentLap++;
    }

    private IEnumerator RaceLoop()
    {
        yield return StartCoroutine(RaceStarting());

        yield return StartCoroutine(Racing());

        yield return StartCoroutine(RaceEnding());
    }

    private IEnumerator RaceStarting()
    {
        yield return new WaitForSecondsRealtime(5f);
    }

    private IEnumerator Racing()
    {
        podRacerScript.EnableControls();

        while (currentLap != laps+1 && checkedpoints != checkpoints.Length) {
            yield return null;
        }
    }

    private IEnumerator RaceEnding()
    {
        yield return new WaitForSecondsRealtime(5f);
    }
}
