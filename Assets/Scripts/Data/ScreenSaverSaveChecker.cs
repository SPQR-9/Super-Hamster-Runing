using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSaverSaveChecker : DataBase
{
    [SerializeField] private bool _isRemoveDataOnStart = false;
    [SerializeField] private AmplitudeScreenSaver _amplitudeScreenSaver;

    private void Awake()
    {
        if(_isRemoveDataOnStart)
            RemoveData();
        CheckSaveFile();
        AddSession();
        SetLastLoginDate(DateTime.Now);
        _amplitudeScreenSaver.OnGameInitialize(Options.SessionCount);
        Save();
        SceneManager.LoadScene(Options.CurrentLevelNumber);
    }

    

    private void CheckSaveFile()
    {
        if (PlayerPrefs.HasKey(DataKeyName))
        {
            Debug.Log("Load old file");
            Load();
        }
        else
        {
            Debug.Log("Create new save file");
            Save();
            SetDateRegistration(DateTime.Now);
        }
    }
}
