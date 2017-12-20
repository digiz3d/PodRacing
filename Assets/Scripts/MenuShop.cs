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

    public void Awake()
    {
        
    }

    public void Start()
    {
        mainMenuScript = transform.parent.gameObject.GetComponent<MainMenuScript>();
        // bind functions to buttons
        goBackButton.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        mainMenuScript.DisplayPodSelection();
    }

    private void BuyItem(PodPart podPart)
    {
        if (MoneyManager.instance.WithdrawMoney(podPart.price)) {
            Debug.Log("bought an item : " + podPart.name);
            PodManager.instance.collection[PodManager.instance.selectedPod].InstallPodPart(podPart);
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
        money.text = "Shop - " + MoneyManager.instance.money + " Truguts";
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
            
            if (part.price > MoneyManager.instance.money)
            {
                item.transform.GetChild(2).GetComponent<Button>().enabled = false;
                item.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.red;
                item.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = part.price +" Truguts";
                item.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
            if (PodManager.instance.collection[PodManager.instance.selectedPod].injector.name == part.name || PodManager.instance.collection[PodManager.instance.selectedPod].engine.name == part.name)
            {
                item.transform.GetChild(2).GetComponent<Button>().enabled = false;
                item.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Installed";
                item.transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
