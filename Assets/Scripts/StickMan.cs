using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class ExtensionMethods
{
    public static Vector2 ToVector2(this Vector3 @this) => new Vector2(@this.x, @this.y);
    public static Vector3 ToVector3(this Vector2 @this) => new Vector3(@this.x, @this.y, 0);
}

public class StickMan : MonoBehaviour
{
    public float MovementSpeed = 9;
    public float MovementAcceleration = 75;
    public float MovementDeceleration = 70;
    public float JumpHeight = 4;

    private BoxCollider2D _collider;
    private bool _isGrounded = false;

    private Vector2 _velocity = new Vector2();

    // Life control

    public int MaxLifeCount = 5;

    private int _lifeCount;
    private StickManLifeBar _lifeBar = null;

    // Animation
    public Animator Anim;

    // Blink Effect
    public Color BlinkColor;
    public int BlinkCount = 5;
    public float BlinkDuration = 0.1f;
    private bool _isBlinking;
    private SpriteRenderer[] _sprites;

    // DeathEffect
    public GameObject DeathEffectPrefab;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _lifeCount = MaxLifeCount;
        _sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void TryPlayAnim(string name)
    {
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(name))
            Anim.Play(name);
    }

    void Update()
    {
        if (_lifeBar == null && Game.Inst.StickManLifeBar)
        {
            _lifeBar = Game.Inst.StickManLifeBar;
            _lifeBar.InitLife(_lifeCount);
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, MovementSpeed * moveInput,
                MovementAcceleration * Time.deltaTime);
            var oldScale = GetComponentInChildren<Transform>().localScale;
            oldScale.x = _velocity.x > 0 ? -1f : 1f;
            GetComponentInChildren<Transform>().localScale = oldScale;
        }
        else
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, MovementDeceleration * Time.deltaTime);
        }

        if (_isGrounded)
        {
            _velocity.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                _velocity.y = Mathf.Sqrt(2 * JumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }
        _velocity.y += Physics2D.gravity.y * Time.deltaTime;

        transform.Translate(_velocity.ToVector3() * Time.deltaTime);

        var colliderSize = _collider.size * transform.localScale;

        var hits = Physics2D.OverlapBoxAll(transform.position.ToVector2() + _collider.offset, colliderSize, 0);

        _isGrounded = false;
        foreach (var hit in hits)
        {
            if (hit == _collider)
                continue;

            if (hit.gameObject.layer == LayerMask.NameToLayer("Windows"))
            {
                var popup = hit.gameObject.GetComponent<XPPopup>();
                if (popup.HitPlayer == false && popup.Destorying == false && _isBlinking == false)
                {
                    Blink();
                    --_lifeCount;
                    _lifeBar.ReduceLife();
                    if (_lifeCount <= 0)
                    {
                        Destroy(_lifeBar.gameObject);
                        Game.Inst.StickManLifeBar = null;
                        Destroy(gameObject);
                        Game.Inst.StickMan = null;
                        Instantiate(DeathEffectPrefab, transform.position, Quaternion.identity);
                    }
                    popup.HitPlayer = true;
                }
            }
            else
            {
                var colliderDistance = hit.Distance(_collider);

                if (colliderDistance.isOverlapped)
                {
                    if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && _velocity.y < 0)
                        _isGrounded = true;

                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                }
            }

        }

        if (!_isGrounded)
            TryPlayAnim("Jump");
        else if (_velocity.x != 0)
            TryPlayAnim("Run");
        else
            TryPlayAnim("Idle");

        if (_lifeBar)
            _lifeBar.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }

    void Blink()
    {
        if (_isBlinking)
            return;

        _isBlinking = true;
        var oldColors = _sprites.Select(s => s.color).ToArray();
        StartCoroutine(BlinkCo(BlinkCount * 2 - 1, _sprites, oldColors));
    }

    IEnumerator BlinkCo(int stopAt, SpriteRenderer[] sprites, Color[] oldColors)
    {
        for (int i = 0; i <= stopAt; ++i)
        {
            for (int si = 0; si < sprites.Length; ++si)
            {
                if (i <= stopAt)
                {
                    if (i % 2 == 0)
                        sprites[si].color = BlinkColor;
                    else
                        sprites[si].color = oldColors[si];

                }
            }
            yield return new WaitForSeconds(BlinkDuration / (float)BlinkCount);
        }

        _isBlinking = false;
    }
}
