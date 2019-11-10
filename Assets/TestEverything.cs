using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEverything : MonoBehaviour
{
    WindowsXP Xp;
                                               
    public Vector2 DropIntervalRange;
    float dropInterval;
    float elapsedTime;
    int currpattern = 0;
    bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        Xp = GameObject.FindObjectOfType<WindowsXP>();
        dropInterval = 0;
        elapsedTime = 0;
    }
                                       
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            stop = !stop;
        }

        if (!stop)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= dropInterval)
            {
                if (currpattern >= Xp.Patterns.Count)
                    currpattern = 0;

                Xp.DropWindows(currpattern);

                ++currpattern;
                dropInterval = Random.Range(DropIntervalRange.x, DropIntervalRange.y);
                elapsedTime = 0;
            }
        }
    }
    
}
