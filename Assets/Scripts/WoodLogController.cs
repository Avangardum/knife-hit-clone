using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

public class WoodLogController : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject leftHalf;
    [SerializeField] private GameObject rightHalf;
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private int initialKnivesAmount = 3;
    [SerializeField] private float initialKnivesMinAngleDifferenceDeg = 10;
    [SerializeField] private AppleData appleData;

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
        
        float[] initialKnifesAngles = CreateInitialKnives();
        if (Random.value <= appleData.SpawnChance)
        {
            CreateApple(initialKnifesAngles);
        }
    }

    private void FixedUpdate()
    {
        if (!_isDestroyed)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.fixedDeltaTime));
            if (_testDestroy)
            {
                Destroy();
                _testDestroy = false;
            }
        }
    }

    public void Destroy()
    {
        _isDestroyed = true;
        foreach (WoodLogSegmentController segmentController in _segmentControllers)
        {
            segmentController.OnLogDestroy();
        }
    }
    
    private float[] CreateInitialKnives()
    {
        float minAngleDifferenceRad = initialKnivesMinAngleDifferenceDeg * Mathf.Deg2Rad;
        float[] initialKnivesAngles = new float[initialKnivesAmount];
        for (int i = 0; i < initialKnivesAmount; i++)
        {
            float angle;
            bool isValid;
            do
            {
                angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                //check if any existing angle is too close
                isValid = true;
                for (int j = 0; j < i; j++)
                {
                    if (Mathf.Abs(angle - initialKnivesAngles[j]) < minAngleDifferenceRad)
                    {
                        isValid = false;
                        break;
                    }
                }
            } while (!isValid);
            initialKnivesAngles[i] = angle;
        }

        foreach (float knifeAngle in initialKnivesAngles)
        {
            Transform knifeParentSegment;
            if (knifeAngle > Mathf.PI / 2 && knifeAngle < Mathf.PI * 1.5f)
            {
                knifeParentSegment = leftHalf.transform;
            }
            else
            {
                knifeParentSegment = rightHalf.transform;
            }

            GameObject knife = GameObject.Instantiate(knifePrefab);
            float knifeDistanceFromLogCenter = 1.7f;
            knife.transform.position = new Vector3(Mathf.Cos(knifeAngle), Mathf.Sin(knifeAngle), 0) * knifeDistanceFromLogCenter;
            knife.transform.parent = knifeParentSegment;
            LookAt2D(knife.transform, transform, -90);
            KnifeController knifeController = knife.GetComponent<KnifeController>();
            knifeController.InitialiseAsInitialKnife();
        }

        return initialKnivesAngles;
    }
    
    private void CreateApple(float[] initialKnivesAngles)
    {
        float minAngleDifferenceRad = appleData.MinimalAngleDifferenceWithInitialKnifeDeg * Mathf.Deg2Rad;
        float angle;
        bool isValid;
        do
        {
            angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
            //check if any existing angle is too close
            isValid = true;
            for (int i = 0; i < initialKnivesAngles.Length; i++)
            {
                if (Mathf.Abs(angle - initialKnivesAngles[i]) < minAngleDifferenceRad)
                {
                    isValid = false;
                    break;
                }
            }
        } while (!isValid);

        GameObject apple = Instantiate(applePrefab);
        apple.transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * appleData.DistanceFromLogCenter;
        LookAt2D(apple.transform, transform, 90);
        Transform appleParentSegment;
        if (angle > Mathf.PI / 2 && angle < Mathf.PI * 1.5f)
        {
            appleParentSegment = leftHalf.transform;
        }
        else
        {
            appleParentSegment = rightHalf.transform;
        }

        apple.transform.parent = appleParentSegment;
    }
    
    private void LookAt2D(Transform lookingObject, Transform target, float rotationOffset = 0f)
    {
        var dir = target.position - lookingObject.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookingObject.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        lookingObject.Rotate(new Vector3(0, 0, rotationOffset));
    }
}
