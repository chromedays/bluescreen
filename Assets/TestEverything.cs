using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEverything : MonoBehaviour
{
    WindowsXP Xp;

    public float WindowDropResizeTime;
    public float WindowSummonInterval;
    public int WindowCountToDrop;
    public float windowResizeDelta;

    // Start is called before the first frame update
    void Start()
    {
        Xp = GameObject.FindObjectOfType<WindowsXP>();   
    }
                                       
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (Xp)
            {
                Xp.DropWindows(WindowDropResizeTime, WindowSummonInterval, WindowCountToDrop, windowResizeDelta);
            }     
        }
    }
}
