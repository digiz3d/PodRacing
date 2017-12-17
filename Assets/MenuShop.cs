using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuShop : MonoBehaviour {
    public List<PodPart> availableParts;

    public Text money;
    public Button goBackButton;

    private MainMenuScript mainMenuScript;
    private MoneyManager moneyManager;

    public void Start()
    {
        mainMenuScript = transform.parent.gameObject.GetComponent<MainMenuScript>();
        moneyManager = mainMenuScript.moneyManager;

        goBackButton.onClick.AddListener(GoBack);

        money.text = "Shop - "+ moneyManager.money + " Truguts";
    }

    private void GoBack()
    {
        mainMenuScript.DisplayPodSelection();
    }
}
