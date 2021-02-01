using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    private void Awake()
    {
        KnifeController.KnifeCollidedWithKnife += Handheld.Vibrate;
        KnifeController.KnifeCollidedWithLog += Handheld.Vibrate;
    }

    private void OnDestroy()
    {
        KnifeController.KnifeCollidedWithKnife -= Handheld.Vibrate;
        KnifeController.KnifeCollidedWithLog -= Handheld.Vibrate;
    }
}
