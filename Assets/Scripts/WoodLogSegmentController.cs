using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WoodLogSegmentController : MonoBehaviour
{
    [SerializeField] private float forceOnDestroy = 1;
    
    public void OnLogDestroy()
    {
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.simulated = true;
        rigidbody.gravityScale = 1;
        Vector2 forceDirection = -transform.right;
        Vector2 force = forceDirection.normalized * forceOnDestroy;
        rigidbody.AddForce(force, ForceMode2D.Impulse);

        var collider = gameObject.GetComponent<PolygonCollider2D>();
        collider.isTrigger = false;
    }
}
