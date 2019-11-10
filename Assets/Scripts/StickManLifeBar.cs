using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManLifeBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Game.Inst.StickManLifeBar = this;
    }

    // Update is called once per frame
    void PreUpdate()
    {
        transform.position = Game.Inst.StickMan.transform.position + new Vector3(0, 0.5f, 0);
    }
}
