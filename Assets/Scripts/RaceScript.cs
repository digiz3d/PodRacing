using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Text countdown;
    public CheckpointScript[] checkpoints;
    public int countdownTime = 4;

    private Pod pod;
    private GameObject playerPod;
    private PodRacerScript podRacerScript;
    private PodRacerScript winner;
    private int currentCountdown;

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
        yield return StartCoroutine(RacePreparation());

        yield return StartCoroutine(DisplayCountDown());

        yield return StartCoroutine(Racing());

        yield return StartCoroutine(RaceEnding());
    }

    private IEnumerator RacePreparation()
    {
        yield return new WaitForSecondsRealtime(5f);
    }

    private IEnumerator DisplayCountDown()
    {
        currentCountdown = countdownTime;
        while(currentCountdown > 0)
        {
            Debug.Log("ah");
            yield return new WaitForSecondsRealtime(1f);
            currentCountdown--;
            countdown.text = currentCountdown + "";
            Debug.Log("bh");
        }
        countdown.text = "GO !";
        Destroy(countdown.gameObject, 2f);
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
