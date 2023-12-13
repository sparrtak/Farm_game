using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnChangeCount();

[CreateAssetMenu(menuName = "Plants/New Plant", fileName = "New Plant")]
public class Plant : ScriptableObject
{
    [SerializeField]
    private Sprite _mainImage;
    public Sprite MainImage => _mainImage;

    [SerializeField]
    private int _clickCount = 1;
    public int ClickCount => _clickCount;

    [SerializeField]
    private int _price = 1;
    public int Price => _price;

    private int _currentCount = 0;
    public event OnChangeCount OnChangeCountEvent;
    //���� ������� ������� ������� ��� ���������� ���������� ����� ��� ��,� ���� ������������ � �����
    public void AddPlant(int count = 1)
    {
        _currentCount += count;
        OnChangeCountEvent?.Invoke();
    }
    //��� ������ ������,����� ������� ��������
    public void SellPlant(int count = 1)
    {
        _currentCount -= count;
        OnChangeCountEvent?.Invoke();
    }

    public int GetPlantCount => _currentCount;
}
