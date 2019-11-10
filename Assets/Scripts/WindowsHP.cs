using UnityEngine;
using UnityEngine.Assertions;

public class WindowsHP : MonoBehaviour
{
    public int MaxLifeCount = 100;

    private int _lifeCount;

    void Start()
    {
        _lifeCount = MaxLifeCount;

        Assert.IsNull(Game.Inst.WindowsHP);
        Game.Inst.WindowsHP = this;
    }

    void ReduceLife()
    {
        --_lifeCount;
    }
}
