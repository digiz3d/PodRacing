using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    public MoneyManager moneyManager;
    public PodManager podManager;
    public LevelManager levelManager;

    public Text info;
    public Text stats;
    public Text engineName;
    public Text injectorName;
    public GameObject previousButton;
    public GameObject nextButton;

	// Use this for initialization
	void Start () {
        DisplaySelectedPod();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PreviousPod()
    {
        if (podManager.selectedPod - 1 < 0)
        {
            Debug.Log("no previous pod");
            return;
        }
        podManager.selectedPod--;
        DisplaySelectedPod();
    }
    public void NextPod()
    {
        if (podManager.selectedPod + 1 >= podManager.collection.Count)
        {
            Debug.Log("no next pod");
            return;
        }
        podManager.selectedPod++;
        DisplaySelectedPod();
    }

    private void DisplaySelectedPod()
    {
        Pod pod = podManager.collection[podManager.selectedPod];
        info.text = "Name : " + pod.name + "\nDescripton : " + pod.description;
        stats.text = "Maximum speed : " + pod.GetMaxSpeed().ToString() + "\n Time required to full speed : " + pod.GetTimeToFullSpeed().ToString();
        engineName.text = pod.engine.name;
        injectorName.text = pod.injector.name;

        previousButton.SetActive(podManager.selectedPod - 1 < 0 ? false : true);
        nextButton.SetActive(podManager.selectedPod + 1 >= podManager.collection.Count ? false : true);
    }

    public void Play()
    {
        levelManager.LoadTheOnlyMap();
    }
}
