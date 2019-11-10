using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StickMan))]
public class StickManHP : MonoBehaviour
{
    public int MaxLife = 5;

    private int _life;
    private List<int> _collidedInstanceIds = new List<int>();

    void OnCollision(int id)
    {
        if (_collidedInstanceIds.Contains(id))
            return;
        _collidedInstanceIds.Add(id);

        --_life;

        if (_life <= 0)
        {
            Destroy(gameObject);
            Game.Inst.StickMan = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StickMan>().OnProjectileCollision.AddListener(OnCollision);

        _life = MaxLife;
    }

    void OnDestroy()
    {
        GetComponent<StickMan>().OnProjectileCollision.RemoveListener(OnCollision);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
