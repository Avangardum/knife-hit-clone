using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public event Action<int> SuccessfulHitsLeftChanged; 
    
    private static int LevelsPassed = 0;

    public int SuccessfulHitsLeft
    {
        get => _successfulHitsLeft;
        private set
        {
            _successfulHitsLeft = value;
            SuccessfulHitsLeftChanged?.Invoke(_successfulHitsLeft);
        }
    }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private KnifeLauncher knifeLauncher;
    [SerializeField] private WoodLogController woodLogController;
    [SerializeField] private int knivesAtLevel1 = 1;
    [SerializeField] private int addKnivesPerLevel = 1;
    [SerializeField] private float delayBeforeNextLevel = 1f;
    [SerializeField] private float gameOverDelay = 1f;

    private int _successfulHitsLeft;

    private void OnEnable()
    {
        SuccessfulHitsLeft = knivesAtLevel1 + LevelsPassed * addKnivesPerLevel;
        KnifeController.KnifeCollidedWithLog += OnSuccessfulHit;
        KnifeController.KnifeCollidedWithKnife += OnKnifeHitsKnife;
    }

    private void OnDisable()
    {
        KnifeController.KnifeCollidedWithLog -= OnSuccessfulHit;
        KnifeController.KnifeCollidedWithKnife -= OnKnifeHitsKnife;
    }

    private void OnSuccessfulHit()
    {
        SuccessfulHitsLeft--;
        if (SuccessfulHitsLeft <= 0)
        {
            knifeLauncher.DestroyCurrentKnife();
            knifeLauncher.StopKnifeSpawning = true;
            woodLogController.Destroy();
            Invoke(nameof(NextLevel), delayBeforeNextLevel);
        }
    }

    private void NextLevel()
    {
        LevelsPassed++;
        if (LevelsPassed > SaveManager.HighScore)
        {
            SaveManager.HighScore = LevelsPassed;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnKnifeHitsKnife()
    {
        Invoke(nameof(GameOver), gameOverDelay);
    }
    
    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}
