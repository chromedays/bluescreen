using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEverything : MonoBehaviour
{
    WindowsXP Xp;
                                     
    float elapsedTime;

    int currpattern = 0;

    bool pause = true;


    // Start is called before the first frame update
    void Start()
    {
        Xp = GameObject.FindObjectOfType<WindowsXP>();
        elapsedTime = 0;
    }
               
    // Update is called once per frame
    void Update()
    {
    }
    
}
