using System;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private SaveOptions _options = new SaveOptions();

    private const string _dataKeyName = "DataSuperHamsterRunning";

    public string DataKeyName => _dataKeyName;
    public SaveOptions Options => _options;

    public void RemoveData()
    {
        PlayerPrefs.DeleteKey(_dataKeyName);
        _options = new SaveOptions();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_options);
        PlayerPrefs.SetString(DataKeyName, json);
    }

    public void Load()
    {
        _options = JsonUtility.FromJson<SaveOptions>(PlayerPrefs.GetString(DataKeyName));
    }

    public void SetCurrentLevelNumber(int currentLevelNumber)
    {
        _options.CurrentLevelNumber = currentLevelNumber;
    }

    public void SetDateRegistration(DateTime date)
    {
        _options.RegistrationDate = date.ToString();
    }

    public void SetLastLoginDate(DateTime date)
    {
        _options.LastLoginDate = date.ToString();
    }

    public void AddSession()
    {
        _options.SessionCount++;
    }

    public int GetNumberDaysAfterRegistration()
    {
        return (DateTime.Parse(_options.LastLoginDate) - DateTime.Parse(_options.RegistrationDate)).Days;
    }
}

[Serializable]

public class SaveOptions
{
    public int CurrentLevelNumber = 1;
    public int SessionCount;
    public string LastLoginDate;
    public string RegistrationDate;
}
