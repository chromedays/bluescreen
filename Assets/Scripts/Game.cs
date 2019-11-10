using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Inst = null;

    public StickMan StickMan = null;
    public StickManLifeBar StickManLifeBar = null;
    public WindowsHP WindowsHP = null;
    public FragmentGenerator FragmentGenerator = null;
    public WindowsXP WindowsXP = null;
    public CameraShake CameraShake = null;
    public SpriteRenderer Bluescreen;

    public bool IsGameEnd = false;

    void Awake()
    {
        Assert.IsNull(Inst);
        Inst = this;
        Screen.SetResolution(1024, 768, false);
    }

    public void Win()
    {
        foreach (var sfx in FindObjectsOfType<AudioSource>())
            sfx.Stop();
        SceneManager.LoadScene("BluescreenEnd");
    }
}
