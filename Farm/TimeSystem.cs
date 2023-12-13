using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class TimeSystem : MonoBehaviour
{
    private static TimeSystem instance;
    public static TimeSystem Instance => instance;
    public int day_glob;
    public int hour_glob;
    public int minute_glob;
    public int second_glob;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            instance = this;
        CheckOnline();
        //StartCoroutine(CheckOnline());
    }

    private void CheckOnline()
    {
       // WWW www = new WWW("https://sparrtakys.000webhostapp.com/");
       // yield return www;
       // if (www.error != null)
       // {
       //     print("Error: " + www.error);
       //     yield break;
       // }
       // string time = www.text;
       //// print(time);
        string format = "yyyy-MM-dd-HH-mm-ss";
        TimeSpan ts;

        //print(PlayerPrefs.GetString("LastSession", time));
        if (PlayerPrefs.HasKey("LastSession"))
        {

            ts = DateTime.Now/*DateTime.ParseExact(time, format, CultureInfo.InvariantCulture)*/ - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            print(string.Format("Час: {0},{1},{2},{3}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds));
            //print(ts.Seconds);
            day_glob = ts.Days;
            hour_glob = ts.Hours;
            minute_glob = ts.Minutes;
            second_glob = ts.Seconds;

        }
        if (TimeOFSeasons.Instance != null)
        {
            TimeOFSeasons.Instance.StartTime = DateTime.Now;
            //print("Ігровий час" + TimeOFSeasons.Instance.StartTime);
        }
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString()); //time);
    }
    void OnApplicationQuit()
    {
        string dateString = TimeOFSeasons.Instance.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
        PlayerPrefs.SetString("StartTime", dateString);
        PlayerPrefs.Save();
    }
}
