using UnityEngine;
using UnityEngine.UI;

public class MenuPodSelectionScript : MonoBehaviour {
    public Text info;
    public Text stats;
    public Text engineName;
    public Text injectorName;
    public Text money;

    public Button previousButton;
    public Button nextButton;
    public Button shopButton;
    public Button goButton;

    private PodManager podManager;
    private MoneyManager moneyManager;
    private MainMenuScript mainMenuScript;
    
    private void Start()
    {
        goButton.enabled = false;
        mainMenuScript = transform.parent.gameObject.GetComponent<MainMenuScript>();
        podManager = mainMenuScript.podManager;
        moneyManager = mainMenuScript.moneyManager;

        nextButton.onClick.AddListener(NextPod);
        previousButton.onClick.AddListener(PreviousPod);
        shopButton.onClick.AddListener(GoToShop);
        goButton.onClick.AddListener(Play);
        DisplaySelectedPod();
        money.text = moneyManager.money+" Truguts";
    }

    public void PreviousPod()
    {
        if (!PreviousPodExists())
        {
            Debug.Log("no previous pod");
            return;
        }
        podManager.selectedPod--;
        DisplaySelectedPod();
    }

    public void NextPod()
    {
        if (!NextPodExists())
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
        stats.text = "Maximum speed : " + ((int)(pod.GetMaxSpeed() * 3.6f)).ToString() + "km/h\n Time required to full speed : " + pod.GetTimeToFullSpeed().ToString();
        engineName.text = pod.engine.name;
        injectorName.text = pod.injector.name;

        previousButton.gameObject.SetActive(PreviousPodExists());
        nextButton.gameObject.SetActive(NextPodExists());
    }

    private bool PreviousPodExists()
    {
        return podManager.selectedPod - 1 >= 0;
    }

    private bool NextPodExists()
    {
        return podManager.selectedPod + 1 < podManager.collection.Count;
    }

    public void Play()
    {
        mainMenuScript.Play();
    }

    public void GoToShop()
    {
        mainMenuScript.DisplayShop();
    }
}
