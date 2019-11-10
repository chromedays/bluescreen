using UnityEngine;
using UnityEngine.Assertions;

public class WindowsHP : MonoBehaviour
{
    public float MaxLifeSpanInSeconds = 70f;
    public GlitchEffect Glitch;

    private float _lifeT = 0f;

    void Start()
    {
        Assert.IsNull(Game.Inst.WindowsHP);
        _lifeT = 0f;
        Game.Inst.WindowsHP = this;
    }

    void Update()
    {
        _lifeT += Time.deltaTime;
        if (_lifeT > MaxLifeSpanInSeconds)
            _lifeT = MaxLifeSpanInSeconds;

        float intensity = _lifeT / MaxLifeSpanInSeconds;
        Glitch.intensity = intensity;
        Glitch.flipIntensity = Mathf.Max(intensity - 0.02f, 0f);
        //Glitch.colorIntensity = intensity * 0.25f;
    }
}
