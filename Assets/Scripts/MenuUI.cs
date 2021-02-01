using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Text appleCounter;
    [SerializeField] private Text highScore;

    private void Awake()
    {
        SetAppleCounterValue(SaveManager.Apples);
        SaveManager.ApplesChanged += SetAppleCounterValue;
        SetHighScore(SaveManager.HighScore);
        SaveManager.HighScoreChanged += SetHighScore;
    }

    private void OnDestroy()
    {
        SaveManager.ApplesChanged -= SetAppleCounterValue;
        SaveManager.HighScoreChanged -= SetHighScore;
    }

    private void SetAppleCounterValue(int value) => appleCounter.text = value.ToString();
    private void SetHighScore(int value) => highScore.text = "High score: " + value;
}
