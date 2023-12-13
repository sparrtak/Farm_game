using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItemScript : MonoBehaviour
{
    public static Farm OwnerFarm;
    [SerializeField]
    private Plant _plant;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private Image _currentImage;

    private void Start()
    {
        //����� ������� � �� ��������
        _name.text = _plant.name;
        _currentImage.sprite = _plant.MainImage;
    }

    public void Click()
    {//������� ������� � �������� ��������
        OwnerFarm.SelectPlant(_plant);
        OwnerFarm.CloseShop();
    }
}
