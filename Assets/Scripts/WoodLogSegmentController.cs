using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WoodLogSegmentController : MonoBehaviour
{
    [SerializeField] private float forceOnDestroy = 1;
    
    public void OnLogDestroy()
    {
        var rigidbody = gameObject.AddComponent<Rigidbody2D>();
        Vector2 forceDirection = -transform.right;
        Vector2 force = forceDirection.normalized * forceOnDestroy;
        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
