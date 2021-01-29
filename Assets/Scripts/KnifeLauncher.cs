using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeLauncher : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    
    private GameObject _currentKnife;
    private KnifeController _currentKnifeController;
    private bool _isReadyToThrow = false;

    private void OnEnable()
    {
        SpawnKnife();
        KnifeController.KnifeCollidedWithLog += SpawnKnife;
        InputManager.Instance.Tap += ThrowKnife;
    }

    private void OnDisable()
    {
        DestroyCurrentKnife();
        KnifeController.KnifeCollidedWithLog -= SpawnKnife;
        InputManager.Instance.Tap -= ThrowKnife;
    }

    private void SpawnKnife()
    {
        _currentKnife = Instantiate(knifePrefab, transform.position, transform.rotation);
        _currentKnifeController = _currentKnife.GetComponent<KnifeController>();
        _isReadyToThrow = true;
    }

    private void DestroyCurrentKnife()
    {
        Destroy(_currentKnife);
        _currentKnife = null;
        _currentKnifeController = null;
        _isReadyToThrow = false;
    }
    
    private void ThrowKnife()
    {
        if (!_isReadyToThrow)
        {
            return;
        }
        _currentKnifeController.Throw();
        _currentKnife = null;
        _currentKnifeController = null;
        _isReadyToThrow = false;
    }
}
