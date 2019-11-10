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

    public StickMan Target;

    public float IdleTime = 2f;
    public float DegreesPerSec = 360f;
    public float Speed = 10f;

    private float _idleT = 0f;
    private Vector2 _velocity = new Vector2();
    private State _state = State.Idle;
    private float _rotationT = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Target = Game.Inst.StickMan;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.Idle:
                _idleT += Time.deltaTime;
                if (_idleT >= IdleTime * 0.5f)
                    UpdateRotation();
                if (_idleT >= IdleTime)
                {
                    _idleT = 0f;
                    if (Target)
                        _velocity = (Target.transform.position - transform.position).normalized * Speed;
                    _state = State.Bullet;
                }
                break;
            case State.Bullet:
                UpdateRotation();
                transform.position = transform.position + _velocity.ToVector3() * Time.deltaTime;
                break;
        }
    }

    void UpdateRotation()
    {
        _rotationT += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _rotationT * DegreesPerSec);
    }
}
