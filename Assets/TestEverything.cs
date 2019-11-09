using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEverything : MonoBehaviour
{
    WindowsXP xp;

    // Start is called before the first frame update
    void Start()
    {
        xp = GameObject.FindObjectOfType<WindowsXP>();   
    }
                                                   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (xp)
            {
                xp.CreatePopUp( Random.insideUnitCircle * Random.Range(20, 50));
            }
            else
            {
                xp = GameObject.FindObjectOfType<WindowsXP>();
            }
        }
    }
}
