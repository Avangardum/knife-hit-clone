using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WoodLogController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100;

    [Header("Test")]
    [SerializeField] private bool _testDestroy;

    private GameObject[] _segments;
    private WoodLogSegmentController[] _segmentControllers;
    private bool _isDestroyed = false;

    private void Awake()
    {
        List<Transform> childen = new List<Transform>();
        foreach (Transform child in transform)
        {
            childen.Add(child);
        }

        _segmentControllers = childen.
            Select(x => x.GetComponent<WoodLogSegmentController>()).
            Where(x => x != null).
            ToArray();

        _segments = _segmentControllers.
            Select(x => x.gameObject).
            ToArray();
    }

    private void FixedUpdate()
    {
        if (!_isDestroyed)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.fixedDeltaTime));
            if (_testDestroy)
            {
                Destroy();
            }
        }
    }

    private void Destroy()
    {
        _isDestroyed = true;
        foreach (WoodLogSegmentController segmentController in _segmentControllers)
        {
            segmentController.OnLogDestroy();
        }
    }
}
