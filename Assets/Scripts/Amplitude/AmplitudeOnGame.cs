using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeOnGame : MonoBehaviour
{
    private float _levelTime = 0f;
    private DataBase _data;

    public void Inicialize(DataBase dataBase)
    {
        _data = dataBase;
    }

    private void Update()
    {
        _levelTime += Time.deltaTime;
    }

    private void OnApplicationQuit()
    {
        Amplitude.Instance.setUserProperty("level_start", _data.Options.CurrentLevelNumber);
        Amplitude.Instance.setUserProperty("session_count", _data.Options.SessionCount);
        Amplitude.Instance.setUserProperty("reg_day", _data.Options.LastLoginDate.ToString());
        Amplitude.Instance.setUserProperty("days_in_game", _data.Options.RegistrationDate);
    }


    public void OnLevelStart()
    {
        Amplitude.Instance.logEvent("level_start", new Dictionary<string, object>()
        {
            {"level",_data.Options.CurrentLevelNumber }
        });
    }

    public void OnLevelComplete()
    {
        Amplitude.Instance.logEvent("level_complete", new Dictionary<string, object>()
        {
            {"level",_data.Options.CurrentLevelNumber },
            {"time_spent",(int)_levelTime }
        });
    }

    public void OnFail()
    {
        Amplitude.Instance.logEvent("fail", new Dictionary<string, object>()
        {
            {"level",_data.Options.CurrentLevelNumber },
            {"time_spent",(int)_levelTime }
        });
    }

    public void OnLevelRestart()
    {
        Amplitude.Instance.logEvent("restart", new Dictionary<string, object>()
        {
            {"time_spent",(int)_levelTime }
        });
    }
}
