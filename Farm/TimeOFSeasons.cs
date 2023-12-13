using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class TimeOFSeasons : MonoBehaviour
{
    private static TimeOFSeasons instance;
    public static TimeOFSeasons Instance => instance;
    public DateTime StartTime { get; set; }
    public UnityEngine.UI.Text season_text;
    public UnityEngine.UI.Text season_text_mon;
    public TimeSystem TimeSystem;
    public int day_offline;
    public int hour_offline;
    public int minute_offline;
    public int second_offline;
    public double all_time_afk;
    public int time_afk_hour;
    public int time_afk_day;
    public int time_afk_minute;
    public int day;
    public int mon;
    public int hour;
    public int minute;
    public int delta = 0;
    public string real_time;
    string winter = "зима";
    string spring = "весна";
    string summer = "л≥то";
    string autumn = "ос≥нь";
    int[] dayArray = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        season_text_mon.text = winter;
    }

    public void Time_offline()
    {

        if (TimeSystem.Instance.hour_glob != null)
        {
            day_offline = TimeSystem.Instance.day_glob;
            hour_offline = TimeSystem.Instance.hour_glob;
            minute_offline = TimeSystem.Instance.minute_glob;
            second_offline = TimeSystem.Instance.second_glob;
            if (day_offline != 0 || hour_offline >=8 )
            {
                all_time_afk = 8 * 3600 * 90;
            }
            else
            {
                all_time_afk = ((hour_offline * 3600) + (minute_offline * 60) + second_offline) * 90;
            }
            time_afk_day = Convert.ToInt32(Math.Floor(all_time_afk / 86400));
            time_afk_hour = Convert.ToInt32(Math.Floor((all_time_afk - (time_afk_day * 86400)) / 3600));
            time_afk_minute = Convert.ToInt32(Math.Floor((all_time_afk - ((time_afk_day * 86400) + (time_afk_hour * 3600))) / 60));
        }
    }

    IEnumerator Coroutine()
    {
        mon = PlayerPrefs.GetInt("mon", 0);
        minute = PlayerPrefs.GetInt("minute", 0);
        hour = PlayerPrefs.GetInt("hour", 0);
        day = PlayerPrefs.GetInt("day", 1);
        while (true)
        {
            if (mon == 12)
            {
                mon = 0;
                PlayerPrefs.SetInt("mon", mon);
            }
            while (mon <= 11)
            {
                if (mon <= 1)
                {
                    season_text_mon.text = winter;
                }
                else if (mon > 1 && mon <= 4)
                {
                    season_text_mon.text = spring;
                }
                else if (mon > 4 && mon <= 7)
                {
                    season_text_mon.text = summer;
                }
                else if (mon > 7 && mon <= 10)
                {
                    season_text_mon.text = autumn;
                }
                else if (mon > 10)
                {
                    season_text_mon.text = winter;
                }
                while (day <= dayArray[mon])
                {
                    while (hour < 24 )
                    {
                        while(minute < 60)
                        {
                            UnityEngine.UI.Text season_text = GetComponent<UnityEngine.UI.Text>();
                            string day_hour_minute = string.Format("{0}:{1}\nƒень: {2}", hour, minute,day);
                            season_text.text = day_hour_minute;
                            PlayerPrefs.SetInt("minute", minute);
                            yield return new WaitForSeconds(1);
                            minute += 90;
                        }
                        minute = minute - 60;
                        hour++;
                        PlayerPrefs.SetInt("hour", hour);
                    }
                    hour = 0;
                    day += 1;
                    PlayerPrefs.SetInt("day", day);
                }
                day = 0;
                mon++;
                PlayerPrefs.SetInt("mon", mon);
            }
        }
    }

    void Start()
     {
        Time_offline();
        DateTime StartTime = DateTime.Now;
        print("–еальний час: " + StartTime);
        StartCoroutine(Coroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnApplicationQuit()
    {
        string dateString = StartTime.ToString("yyyy-MM-dd HH:mm:ss");
        PlayerPrefs.SetString("StartTime", dateString);
        PlayerPrefs.SetInt("day", day);
        PlayerPrefs.Save();
    }
}
