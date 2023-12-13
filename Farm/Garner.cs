using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garner : MonoBehaviour
{
    private static Garner _instance; // реал≥зац≥€ патерну singleton
    public static Garner Instance => _instance; // 

    [SerializeField]
    private List<SellItemScript> _itemsToSell = new List<SellItemScript>();
    [SerializeField]
    private GameObject _garner;

    [SerializeField]
    private Text _moneyCounter;

    private int _moneyCount;
    //показуЇ к≥льк≥сть грошей гравц€ €к т≥льки показуЇтьс€ сцена
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        _moneyCount = PlayerPrefs.GetInt("money", 0);
        _moneyCounter.text = _moneyCount.ToString();
    }
    //добавленн€ грошей
    public void AddMoney(int money)
    {
        _moneyCount += money;
        _moneyCounter.text = _moneyCount.ToString();
    }
    //витрата грошей
    public void SpendMoney(int money)
    {
        _moneyCount -= money;
        _moneyCounter.text = _moneyCount.ToString();
    }

    public int GetMoney => _moneyCount;

    //оновленн€ к≥лькост≥ товару п≥сл€ продажу 
    public void OnEnable()
    {
        foreach (var item in _itemsToSell)
            item.ChangeCount();
    }
    void OnApplicationQuit()
    {
        //збереженн€ даних про к≥льк≥сть грошей при виход≥
        PlayerPrefs.SetInt("money", _moneyCount);
        PlayerPrefs.SetInt("checer", 0);
        PlayerPrefs.Save();
    }
}
