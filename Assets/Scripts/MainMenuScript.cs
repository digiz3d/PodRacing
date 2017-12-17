using UnityEngine;

public class MainMenuScript : MonoBehaviour {
    public MenuPodSelectionScript menuPodSelection;
    public MenuShop menuShop;

    public MoneyManager moneyManager;
    public PodManager podManager;
    public LevelManager levelManager;

    // Use this for initialization
    private void Start () {
        menuShop.gameObject.SetActive(false);
	}
    
    public void Play()
    {
        Debug.Log("loading.");
        DisableAll();
        levelManager.LoadTheOnlyMap();
    }

    public void DisplayShop()
    {
        DisableAll();
        menuShop.gameObject.SetActive(true);
    }
    public void DisplayPodSelection()
    {
        DisableAll();
        menuPodSelection.gameObject.SetActive(true);
    }
    private void DisableAll()
    {
        menuPodSelection.gameObject.SetActive(false);
        menuShop.gameObject.SetActive(false);
    }
}
