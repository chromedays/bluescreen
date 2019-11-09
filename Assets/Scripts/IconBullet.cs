using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBullet : MonoBehaviour
{
    public enum State
    {
        Idle,
        Bullet
    }

    public float DegreesPerSec = 360f;
    public float Speed = 10f;

    private Vector2 _velocity;
    private State _state = State.Bullet;
    private float _t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //var playerPos = new Vector2();
        _velocity = Vector2.down * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == State.Bullet)
        {
            _t += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, _t * DegreesPerSec);
        }
    }
}
