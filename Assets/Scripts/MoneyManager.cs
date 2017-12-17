using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

    public static MoneyManager instance;

    public int money = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple MoneyManager scripts !!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        //money = PlayerPrefs.GetInt("money");
    }

    public void AddMoney(int quantity)
    {
        money += Mathf.Abs(quantity);
    }

    public bool WithdrawMoney(int quantity)
    {
        if (Mathf.Abs(quantity) > money)
        {
            return false;
        }
        money -= Mathf.Abs(quantity);
        return true;
    }

    private void OnDestroy()
    {
        //PlayerPrefs.SetInt("money", money);
    }
}
