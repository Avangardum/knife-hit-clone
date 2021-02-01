using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    public void OnKnifeHit()
    {
        SaveManager.Apples++;
        Destroy(gameObject);
    }
}
