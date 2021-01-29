using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeLauncher : MonoBehaviour
{
    public bool StopKnifeSpawning = false;
    
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject knifePrefab;
    
    private GameObject _currentKnife;
    private KnifeController _currentKnifeController;
    private bool _currentKnifeExists = false;

    
    
    private void OnEnable()
    {
        KnifeController.KnifeCollidedWithLog += SpawnKnife;
        inputManager.Tap += ThrowKnife;
        SpawnKnife();
    }

    private void OnDisable()
    {
        KnifeController.KnifeCollidedWithLog -= SpawnKnife;
        inputManager.Tap -= ThrowKnife;
    }

    public void OnLevelStart()
    {
        SpawnKnife();
    }
    
    private void SpawnKnife()
    {
        if (StopKnifeSpawning)
        {
            return;
        }
        _currentKnife = Instantiate(knifePrefab, transform.position, transform.rotation);
        _currentKnifeController = _currentKnife.GetComponent<KnifeController>();
        _currentKnifeExists = true;
    }

    public void DestroyCurrentKnife()
    {
        if (!_currentKnifeExists)
        {
            return;
        }
        Destroy(_currentKnife);
        _currentKnife = null;
        _currentKnifeController = null;
        _currentKnifeExists = false;
    }
    
    private void ThrowKnife()
    {
        if (!_currentKnifeExists)
        {
            return;
        }
        _currentKnifeController.Throw();
        _currentKnife = null;
        _currentKnifeController = null;
        _currentKnifeExists = false;
    }
}
