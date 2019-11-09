using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StickMan : MonoBehaviour
{
    public float MovementSpeed = 2f;
    public float JumpSpeed = 2f;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("haha");
        if (collision.gameObject.tag == "Ground" & _isGrounded == false)
            _isGrounded = true;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("haha2");
        if (collision.gameObject.tag == "Ground" & _isGrounded == false)
            _isGrounded = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody.velocity = Vector2.right * MovementSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody.velocity = Vector2.left * MovementSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(new Vector2(0, 2) * JumpSpeed, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }
}
