using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    private static int maxWorkers = 40;
    private static int workerPrice = 200;

    [SerializeField]
    private GameObject _shop;

    [SerializeField]
    private Text _growStage;
    [SerializeField]
    private Image _currentPlantImage;
    [SerializeField]
    private Text _workersCountText;

    private Plant _currentPlant;
    private int _countsToReady = 0;
    private int _workersCount = 0;

    private void Start()
    {
        //текст який буде видний на кнопці
        _growStage.text = "Виберіть рослину";
        //проба загрузити дані про кількість робітників
        _workersCount = PlayerPrefs.GetInt("workers", 0);
    }

    public void AddWorker()
    {
        //якщо ціна на балансі більша за необхідну суму для найма робочого то виконується покупка
        if (_workersCount < maxWorkers)
            if ((_workersCount + 1) * workerPrice <= Garner.Instance.GetMoney)
            {
                Garner.Instance.SpendMoney((_workersCount + 1) * workerPrice);
                _workersCount++;
            }
        //вивід кількості робітників найманих
        _workersCountText.text = _workersCount.ToString();
    }

    public void RemoveWorker()
    {//якщо кількість робочих більше нуля то можна продати робочого щоб поповнити гроші
        if (_workersCount > 0)
        {
            _workersCount--;
            Garner.Instance.AddMoney((_workersCount + 1) * workerPrice / 2);
        }

        _workersCountText.text = _workersCount.ToString();
    }

    public void OpenShop()
    {
        PlantItemScript.OwnerFarm = this;
        _shop.SetActive(true);
        GrowPlant();
    }

    public void CloseShop()
    {
        _shop.SetActive(false);
    }

    public void SelectPlant(Plant plant)
    {
        if (_currentPlant != null)
            FarmGrownController.Controller.OnFixedUpdateEvent -= GrowPlantPerSecond;
        _currentPlantImage.sprite = plant.MainImage;//показуємо іконку рослини
        _currentPlant = plant;//вибираємо рослину
        _countsToReady = _currentPlant.ClickCount;//вказуємо кількість кліків до дозрівання
        _growStage.text = $"Стадія росту №{_countsToReady}";//показуємо стадію росту
        FarmGrownController.Controller.OnFixedUpdateEvent += GrowPlantPerSecond;
    }

    public void GrowPlantPerSecond()
    {//для підрахування роботи робітників за кожного робочого викликається метод по збору рослин
        for (int i = 0; i < _workersCount; i++)
        {
            GrowPlant();
        }
    }

    public void GrowPlant()
    {
        //якщо є рослина то то значення -1 кожної секунди якщо число переходить в відємні значення рослина садиться автоматично ще раз,якщо рівна 0 стадія росту заміняється на зібрати кослину
        if (_currentPlant != null)
        {
            _countsToReady--;
            _growStage.text = $"Стадія росту №{_countsToReady}";

            if (_countsToReady < 0)
            {
                _currentPlant.AddPlant();
                _countsToReady = _currentPlant.ClickCount;
                _growStage.text = $"Стадія росту №{_countsToReady}";
            }

            if (_countsToReady == 0)
                _growStage.text = "Зібрати рослину";
        }

    }
    void OnApplicationQuit()
    {//збереження даних кількості робочих при виході
        PlayerPrefs.SetInt("workers", _workersCount);
        PlayerPrefs.Save();
    }
}
