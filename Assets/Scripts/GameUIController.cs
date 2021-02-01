using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text appleCounter;
    [SerializeField] private Text knifeCounter;

    private void Awake()
    {
        SetAppleCounterValue(SaveManager.Apples);
        SaveManager.ApplesChanged += SetAppleCounterValue;
        
        SetKnifeCounterValue(gameManager.SuccessfulHitsLeft);
        gameManager.SuccessfulHitsLeftChanged += SetKnifeCounterValue;
    }

    private void OnDestroy()
    {
        SaveManager.ApplesChanged -= SetAppleCounterValue;
        gameManager.SuccessfulHitsLeftChanged -= SetKnifeCounterValue;
    }

    private void SetAppleCounterValue(int value) => appleCounter.text = value.ToString();

    private void SetKnifeCounterValue(int value) => knifeCounter.text = value.ToString();
}
