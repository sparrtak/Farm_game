using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("money", _moneyCount);
       // PlayerPrefs.SetInt("datatime", 0);
    }

    void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("datatime", 0);
        //PlayerPrefs.SetInt("money", 0);
        //PlayerPrefs.Save();
    }
}
