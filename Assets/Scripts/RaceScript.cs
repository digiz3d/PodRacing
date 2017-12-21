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
    public Text statusUI;
    public CheckpointScript[] checkpoints;
    public int countdownTime = 4;
    public ParticleSystem sparksPrefab;

    private Pod pod;
    private GameObject playerPod;
    private PodRacerScript podRacerScript;
    private PodRacerScript winner;
    private int currentCountdown;
    private float timeSinceStart = 0f;

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
        minimapScript.Follow(playerPod.transform);
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
            yield return new WaitForSecondsRealtime(1f);
            currentCountdown--;
            statusUI.text = currentCountdown + "";
        }
        statusUI.text = "GO !";
    }

    private IEnumerator Racing()
    {
        podRacerScript.EnableControls();
        yield return new WaitForSecondsRealtime(2f);
        statusUI.enabled = false;
        while (winner == null) {
            timeSinceStart += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator RaceEnding()
    {
        statusUI.text = "Finished in "+ timeSinceStart +"";
        statusUI.enabled = true;
        yield return new WaitForSecondsRealtime(3f);
        podRacerScript.DisableControls();
        statusUI.text = "Going back to the lobby";
        yield return new WaitForSecondsRealtime(3f);
        LevelManager.instance.GoBackToMainMenu();
    }

    public void SpawnSparks(Vector3 position, Quaternion rotation)
    {
        Instantiate(sparksPrefab, position, rotation);
    }
}
