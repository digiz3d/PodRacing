using UnityEngine;

public class MainMenuScript : MonoBehaviour {
    public MenuPodSelectionScript menuPodSelection;
    public MenuShop menuShop;

    // Use this for initialization
    private void Start () {
        DisplayPodSelection();
    }
    
    public void Play()
    {
        DisableAll();
        LevelManager.instance.LoadTheOnlyMap();
    }

    public void DisplayShop()
    {
        DisableAll();
        menuShop.gameObject.SetActive(true);
        menuShop.Refresh();
    }

    public void DisplayPodSelection()
    {
        DisableAll();
        menuPodSelection.gameObject.SetActive(true);
        menuPodSelection.Refresh();
    }

    private void DisableAll()
    {
        menuPodSelection.gameObject.SetActive(false);
        menuShop.gameObject.SetActive(false);
    }
}
