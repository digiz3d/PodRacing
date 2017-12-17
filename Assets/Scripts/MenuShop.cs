using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuShop : MonoBehaviour {
    public List<PodPart> availableParts;
    public GameObject prefabPodPartItem;
    public GridLayoutGroup shopPodPartGrid;

    public Text money;
    public Button goBackButton;

    private MainMenuScript mainMenuScript;
    private MoneyManager moneyManager;
    private PodManager podManager;

    public void Awake()
    {
        mainMenuScript = transform.parent.gameObject.GetComponent<MainMenuScript>();
        moneyManager = mainMenuScript.moneyManager;
        podManager = mainMenuScript.podManager;
    }

    public void Start()
    {
        // bind functions to buttons
        goBackButton.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        mainMenuScript.DisplayPodSelection();
    }

    private void BuyItem(PodPart podPart)
    {
        if (moneyManager.WithdrawMoney(podPart.price)) {
            Debug.Log("bought an item : " + podPart.name);
            podManager.collection[podManager.selectedPod].InstallPodPart(podPart);
            Refresh();
        }
        else
        {
            Debug.Log("couldn't buy " + podPart.name);
        }
    }

    public void Refresh()
    {
        RefreshMoney();
        RefreshPodPartList();
    }

    private void RefreshMoney()
    {
        money.text = "Shop - " + moneyManager.money + " Truguts";
    }

    private void RefreshPodPartList()
    {
        foreach(Transform child in shopPodPartGrid.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("RefreshPodPartList");
        foreach (PodPart part in availableParts)
        {
            GameObject item = Instantiate(prefabPodPartItem, shopPodPartGrid.transform);
            item.transform.GetChild(0).GetComponent<Text>().text = part.name;
            item.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Buy for " + part.price;
            item.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { BuyItem(part); });
            
            if (part.price > moneyManager.money)
            {
                item.transform.GetChild(2).GetComponent<Button>().enabled = false;
                item.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.red;
                item.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = part.price +" Truguts";
                item.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
            if (podManager.collection[podManager.selectedPod].injector.name == part.name || podManager.collection[podManager.selectedPod].engine.name == part.name)
            {
                item.transform.GetChild(2).GetComponent<Button>().enabled = false;
                item.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Installed";
                item.transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
