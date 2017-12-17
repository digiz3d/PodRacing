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
        levelManager.LoadTheOnlyMap();
    }

    public void DisplayShop()
    {
        menuPodSelection.gameObject.SetActive(false);
        menuShop.gameObject.SetActive(true);
    }
}
