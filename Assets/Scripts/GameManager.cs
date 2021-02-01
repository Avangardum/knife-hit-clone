using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static int LevelsPassed = 0;
    
    public int SuccessfulHitsLeft { get; private set; }

    [SerializeField] private KnifeLauncher knifeLauncher;
    [SerializeField] private WoodLogController woodLogController;
    [SerializeField] private int knivesAtLevel1 = 1;
    [SerializeField] private int addKnivesPerLevel = 1;
    [SerializeField] private float delayBeforeNextLevel = 1f;

    private void OnEnable()
    {
        SuccessfulHitsLeft = knivesAtLevel1 + LevelsPassed * addKnivesPerLevel;
        KnifeController.KnifeCollidedWithLog += OnSuccessfulHit;
    }

    private void OnDisable()
    {
        KnifeController.KnifeCollidedWithLog -= OnSuccessfulHit;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
