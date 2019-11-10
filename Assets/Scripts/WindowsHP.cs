using UnityEngine;
using UnityEngine.Assertions;

public class WindowsHP : MonoBehaviour
{
    public int MaxLifeCount = 100;
    public GlitchEffect Glitch;

    private int _lifeCount;

    void Start()
    {
        _lifeCount = MaxLifeCount;

        Assert.IsNull(Game.Inst.WindowsHP);
        Game.Inst.WindowsHP = this;
    }

    public void ReduceLife()
    {
        if (_lifeCount <= 0)
            return;
        --_lifeCount;

        float intensity = 1 - ((float)_lifeCount / (float)MaxLifeCount);
        Glitch.intensity = intensity;
        Glitch.flipIntensity = intensity;
        Glitch.colorIntensity = intensity * 0.25f;
    }
}
