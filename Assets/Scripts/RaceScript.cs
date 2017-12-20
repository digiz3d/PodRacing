using System.Collections;
using UnityEngine;

public class RaceScript : MonoBehaviour {
    public static RaceScript instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple RaceScript scripts !!");
            return;
        }
        instance = this;
    }

    public int laps = 1;
    public Transform spawn;
    public CameraScript cameraScript;
    public UIPodSpeedScript uiPodScript;
    public MinimapCameraScript minimapScript;

    private Pod pod;
    private GameObject playerPod;
    private PodRacerScript podRacerScript;

    public CheckpointScript[] checkpoints;

    private PodRacerScript winner;

    // Use this for initialization
    private void Start () {
        pod = PodManager.instance.GetSelectedPod();
        SpawnPod();
        StartCoroutine(RaceLoop());
    }

    private void SpawnPod()
    {
        playerPod = Instantiate(pod.prefab, spawn.position, spawn.rotation);
        podRacerScript = playerPod.GetComponent<PodRacerScript>();
        podRacerScript.DisableControls();
        podRacerScript.SetPodCamera(cameraScript);
        podRacerScript.SetPod(pod);
        cameraScript.SetAimPoint(playerPod.transform.GetChild(1));
        cameraScript.SetObjectToFocus(playerPod);
        uiPodScript.SetPod(podRacerScript);
        minimapScript.playerPodTransform = playerPod.transform;
    }

    public void PassFinishLine(PodRacerScript podRacerScript)
    {
        if (podRacerScript.GetCheckpointsNumber() == checkpoints.Length) {
            if (podRacerScript.GetCurrentLap()+1 == laps)
            {
                winner = podRacerScript;
                return;
            }
            podRacerScript.NextLap();
            podRacerScript.ResetCheckpoints();
        }
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

        while (winner == null) {
            yield return null;
        }
    }

    private IEnumerator RaceEnding()
    {
        Debug.Log("gg "+podRacerScript.name);
        yield return new WaitForSecondsRealtime(5f);
        LevelManager.instance.GoBackToMainMenu();
    }
}
