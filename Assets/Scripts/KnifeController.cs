using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public static event Action KnifeCollidedWithLog;
    public static event Action KnifeCollidedWithKnife;
    
    [SerializeField] private float speed = 1;
    
    private bool _isMovingUp = false;
    private bool _isAttachedToLog = false;
    private bool _hasCollidedWithOtherKnife = false;

    private void FixedUpdate()
    {
        if (_isMovingUp)
        {
            transform.Translate(Vector2.up * (speed * Time.fixedDeltaTime), Space.World);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAttachedToLog)
            return;
        
        if (other.CompareTag("Wood Log"))
        {
            OnWoodLogCollision(other.gameObject);
        }
        else if (other.CompareTag("Knife"))
        {
            OnKnifeCollision(other.gameObject);
        }
        else
        {
            Debug.LogError("A knife collided with an unknown object");
        }
    }

    private void OnWoodLogCollision(GameObject woodLogSegment)
    {
        _isMovingUp = false;
        _isAttachedToLog = true;
        transform.parent = woodLogSegment.transform;
        KnifeCollidedWithLog?.Invoke();
    }

    private void OnKnifeCollision(GameObject otherKnife)
    {
        if (_hasCollidedWithOtherKnife)
        {
            return;
        }
        _isMovingUp = false;
        _hasCollidedWithOtherKnife = true;
        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.down * speed;
        KnifeCollidedWithKnife?.Invoke();
    }

    public void Throw()
    {
        _isMovingUp = true;
    }
}
