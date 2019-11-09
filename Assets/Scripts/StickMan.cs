using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    public float Speed = 2f;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.velocity = Vector2.right * Speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.velocity = Vector2.left * Speed;
        }
    }
}
