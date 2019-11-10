using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Game : MonoBehaviour
{
    public static Game Inst = null;

    public StickMan StickMan = null;

    void Awake()
    {
        Assert.IsNull(Inst);
        Inst = this;
    }
}
