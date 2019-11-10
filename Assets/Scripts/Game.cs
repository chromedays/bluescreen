using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Game : MonoBehaviour
{
    public static Game Inst = null;

    public StickMan StickMan = null;
    public StickManLifeBar StickManLifeBar = null;
    public WindowsHP WindowsHP = null;
    public FragmentGenerator FragmentGenerator = null;
    public WindowsXP WindowsXP = null;
    public CameraShake CameraShake = null;

    void Awake()
    {
        Assert.IsNull(Inst);
        Inst = this;
    }
}
