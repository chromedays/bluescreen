using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEverything : MonoBehaviour
{
    WindowsXP Xp;

    public Vector2 WindowDropResizeTimeMinMax;
    public Vector2 WindowSummonIntervalMinMax;
    public Vector2 WindowCountToDropMinMax;
    public Vector2 windowResizeDeltaMinMax;
    public Vector2 windowInitialSizeXRange;
    public Vector2 windowInitialSizeYRange;

    public Vector2 DropIntervalRange;
    float dropInterval;
    float elapsedTime;
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
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= dropInterval)
        {
#if false // Random everything
            float WindowResizeDelta = Random.Range(windowResizeDeltaMinMax.x, windowResizeDeltaMinMax.y);
            float WindowDropResizeTime = Random.Range(WindowDropResizeTimeMinMax.x, WindowDropResizeTimeMinMax.y);
#endif
            float WindowSummonInterval = Random.Range(WindowSummonIntervalMinMax.x, WindowSummonIntervalMinMax.y);
            float WindowCountToDrop = Random.Range(WindowCountToDropMinMax.x, WindowCountToDropMinMax.y);

            Xp.DropWindows(WindowDropResizeTimeMinMax,
                WindowSummonInterval, 
                (int)WindowCountToDrop,
                windowResizeDeltaMinMax,
                windowInitialSizeXRange,
                windowInitialSizeYRange);

            dropInterval = Random.Range(DropIntervalRange.x, DropIntervalRange.y);
            elapsedTime = 0;
        }
    }
    
}
