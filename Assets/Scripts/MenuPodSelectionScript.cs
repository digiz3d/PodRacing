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

    private MainMenuScript mainMenuScript;
    
    private void Start()
    {
        mainMenuScript = transform.parent.gameObject.GetComponent<MainMenuScript>();

        // bind functions to buttons
        nextButton.onClick.AddListener(NextPod);
        previousButton.onClick.AddListener(PreviousPod);
        shopButton.onClick.AddListener(GoToShop);
        goButton.onClick.AddListener(Play);

        Refresh();
    }


    public void PreviousPod()
    {
        if (!PreviousPodExists())
        {
            Debug.Log("no previous pod");
            return;
        }
        PodManager.instance.selectedPod--;
        RefreshSelectedPod();
    }

    public void NextPod()
    {
        if (!NextPodExists())
        {
            Debug.Log("no next pod");
            return;
        }
        PodManager.instance.selectedPod++;
        RefreshSelectedPod();
    }

    

    private bool PreviousPodExists()
    {
        return PodManager.instance.selectedPod - 1 >= 0;
    }

    private bool NextPodExists()
    {
        return PodManager.instance.selectedPod + 1 < PodManager.instance.collection.Count;
    }

    public void Play()
    {
        mainMenuScript.Play();
    }

    public void GoToShop()
    {
        mainMenuScript.DisplayShop();
    }


    #region Refreshes

    public void Refresh()
    {
        RefreshSelectedPod();
        RefreshMoney();
    }
    private void RefreshMoney()
    {
        money.text = MoneyManager.instance.money + " Truguts";
    }
    private void RefreshSelectedPod()
    {
        Pod pod = PodManager.instance.collection[PodManager.instance.selectedPod];
        info.text = "Name : " + pod.name + "\nDescripton : " + pod.description;
        stats.text = "Maximum speed : " + ((int)(pod.GetMaxSpeed() * 3.6f)).ToString() + "km/h\n Time required to full speed : " + pod.GetTimeToFullSpeed().ToString();
        engineName.text = pod.engine.name;
        injectorName.text = pod.injector.name;
        previousButton.gameObject.SetActive(PreviousPodExists());
        nextButton.gameObject.SetActive(NextPodExists());
    }

    #endregion
}
