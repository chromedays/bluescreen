using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StickMan))]
public class StickManHP : MonoBehaviour
{
    public int MaxLifeCount = 5;

    private int _lifeCount;
    private List<int> _collidedInstanceIds = new List<int>();
    private StickManLifeBar _lifeBar = null;

    void OnCollision(int id)
    {
        if (_collidedInstanceIds.Contains(id))
            return;
        _collidedInstanceIds.Add(id);

        --_lifeCount;

        if (_lifeCount <= 0)
        {
            Destroy(gameObject);
            Game.Inst.StickMan = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StickMan>().OnProjectileCollision.AddListener(OnCollision);

        _lifeCount = MaxLifeCount;
    }

    void OnDestroy()
    {
        GetComponent<StickMan>().OnProjectileCollision.RemoveListener(OnCollision);
    }

    // Update is called once per frame
    void Update()
    {
        if (_lifeBar == null && Game.Inst.StickManLifeBar)
        {
            _lifeBar = Game.Inst.StickManLifeBar;
            _lifeBar.InitLife(_lifeCount);
        }
        Game.Inst.StickManLifeBar.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }
}
