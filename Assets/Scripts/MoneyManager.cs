using UnityEngine;

public class MoneyManager : MonoBehaviour {

    public static MoneyManager instance;

    public int money = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple MoneyManager scripts !!");
            Destroy(gameObject);
        }
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
}
