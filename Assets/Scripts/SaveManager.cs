using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    private const string AppleKey = "apples";
    private const string HighScoreKey = "highScore";
    
    public static event Action<int> ApplesChanged;
    public static event Action<int> HighScoreChanged; 
    
    public static int Apples
    {
        get => _apples;
        set
        {
            _apples = value;
            PlayerPrefs.SetInt(AppleKey, _apples);
            PlayerPrefs.Save();
            ApplesChanged?.Invoke(_apples);
        }
    }

    public static int HighScore
    {
        get => _highScore;
        set
        {
            _highScore = value;
            PlayerPrefs.SetInt(HighScoreKey, _highScore);
            PlayerPrefs.Save();
            HighScoreChanged?.Invoke(_highScore);
        }
    }

    private static int _apples;
    private static int _highScore;
    
    static SaveManager()
    {
        _apples = PlayerPrefs.GetInt(AppleKey);
        _highScore = PlayerPrefs.GetInt(HighScoreKey);
    }
}
