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
        //����� ���� ���� ������ �� ������
        _growStage.text = "������� �������";
        //����� ��������� ��� ��� ������� ��������
        _workersCount = PlayerPrefs.GetInt("workers", 0);
    }

    public void AddWorker()
    {
        //���� ���� �� ������ ����� �� ��������� ���� ��� ����� �������� �� ���������� �������
        if (_workersCount < maxWorkers)
            if ((_workersCount + 1) * workerPrice <= Garner.Instance.GetMoney)
            {
                Garner.Instance.SpendMoney((_workersCount + 1) * workerPrice);
                _workersCount++;
            }
        //���� ������� �������� ��������
        _workersCountText.text = _workersCount.ToString();
    }

    public void RemoveWorker()
    {//���� ������� ������� ����� ���� �� ����� ������� �������� ��� ��������� �����
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
        _currentPlantImage.sprite = plant.MainImage;//�������� ������ �������
        _currentPlant = plant;//�������� �������
        _countsToReady = _currentPlant.ClickCount;//������� ������� ���� �� ���������
        _growStage.text = $"����� ����� �{_countsToReady}";//�������� ����� �����
        FarmGrownController.Controller.OnFixedUpdateEvent += GrowPlantPerSecond;
    }

    public void GrowPlantPerSecond()
    {//��� ����������� ������ �������� �� ������� �������� ����������� ����� �� ����� ������
        for (int i = 0; i < _workersCount; i++)
        {
            GrowPlant();
        }
    }

    public void GrowPlant()
    {
        //���� � ������� �� �� �������� -1 ����� ������� ���� ����� ���������� � ���� �������� ������� �������� ����������� �� ���,���� ���� 0 ����� ����� ���������� �� ������ �������
        if (_currentPlant != null)
        {
            _countsToReady--;
            _growStage.text = $"����� ����� �{_countsToReady}";

            if (_countsToReady < 0)
            {
                _currentPlant.AddPlant();
                _countsToReady = _currentPlant.ClickCount;
                _growStage.text = $"����� ����� �{_countsToReady}";
            }

            if (_countsToReady == 0)
                _growStage.text = "ǳ����� �������";
        }

    }
    void OnApplicationQuit()
    {//���������� ����� ������� ������� ��� �����
        PlayerPrefs.SetInt("workers", _workersCount);
        PlayerPrefs.Save();
    }
}
