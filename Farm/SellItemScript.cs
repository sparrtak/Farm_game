using UnityEngine;
using UnityEngine.UI;

public class SellItemScript : MonoBehaviour
{
    [SerializeField]
    private Plant _plant;
    [SerializeField]
    private Text _name, _plantCount, _plantPrice;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private InputField _count;
    private void Start()
    {
        _name.text = _plant.name;
        _image.sprite = _plant.MainImage;
        _plantPrice.text = _plant.Price.ToString();
        _plant.OnChangeCountEvent += ChangeCount;
        ChangeCount();
    }

    public void Sell()
    {
        //якщо кількість товару більша нуля і менша за максимум то товар можна продати 
        if (int.TryParse(_count.text, out var temp))
            if (temp > 0 && temp <= _plant.GetPlantCount)
            {
                Garner.Instance.AddMoney(temp * _plant.Price);
                _plant.SellPlant(temp);
            }
    }
    //змінюються кількість грошей на рахунку
    public void ChangeCount()
    {
        _plantCount.text = _plant.GetPlantCount.ToString();
    }
}
