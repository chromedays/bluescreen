using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class WindowsXP : MonoBehaviour
{                                     
    public Canvas ScreenCanvas;
    public GameObject PopupPrefab;
    public AudioSource PopupErrorSound;
    public GameObject MouseGameObj;

    public Vector2 WindowDefaultSize;
                                     
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(ScreenCanvas, "Screen Canvas should not be null!");
        Assert.IsNotNull(PopupPrefab, "Popup Prefab should not be null!");
        Assert.IsNotNull(PopupErrorSound, "PopupErrorSound should not be null!");
        Assert.IsNotNull(MouseGameObj, "MouseGameObj should not be null!");
    }                             

    public void DropWindows(Vector2 resizeTimeRange, float summonInterval, int windowCounts,
        Vector2 resizeDeltaRange, Vector2 initialSizeXRange, Vector2 initialSizeYRang)
    {                                   
        StartCoroutine(SummonWindows(resizeTimeRange, summonInterval, windowCounts, resizeDeltaRange,
            initialSizeXRange, initialSizeYRang));
    }

    IEnumerator SummonWindows(Vector2 resizeTimeRange, 
        float summonInterval, int windowCounts, Vector2 resizeDeltaRange,
        Vector2 initialSizeXRange, Vector2 initialSizeYRange)
    {                                             
        float elapstedTime = summonInterval;

        Vector2 canvasPos = ScreenCanvas.transform.position;
        Vector2 canvasSize = ScreenCanvas.GetComponent<RectTransform>().sizeDelta;      
        Vector2 position = new Vector2(canvasPos.x - canvasSize.x/2, canvasPos.y + canvasSize.y/2);

        float deltaPosX = canvasSize.x / windowCounts;
        int c = 0;
       
        while(c < windowCounts)
        {
            elapstedTime += Time.deltaTime;
            if (elapstedTime >= summonInterval)
            {
#if true  // Random everything
                position.x = Random.Range(canvasPos.x-canvasSize.x/2, canvasPos.x + canvasSize.x / 2);
                float WindowDropResizeTime = Random.Range(resizeTimeRange.x, resizeTimeRange.y);
                float WindowResizeDelta = Random.Range(resizeDeltaRange.x, resizeDeltaRange.y);
                Vector2 WindowInitialSize = new Vector2(
                    Random.Range(initialSizeXRange.x, initialSizeXRange.y),
                    Random.Range(initialSizeXRange.x, initialSizeXRange.y));
#endif
                //position += deltaPosX; 

                CreatePopUp(position, WindowInitialSize).AnimateResize(WindowDropResizeTime, WindowResizeDelta, null);
                elapstedTime -= summonInterval;
                ++c;
            }
            yield return null;
        }
    }

    XPPopup CreatePopUp(Vector2 position, Vector2 size)
    {
        GameObject Popup = Instantiate(PopupPrefab, ScreenCanvas.transform);

        RectTransform rect = Popup.GetComponent<RectTransform>();
        rect.localPosition = position;
        rect.sizeDelta = size;

        XPPopup popupComp = Popup.GetComponent<XPPopup>();

        //PopupErrorSound.Play();
  
        return popupComp;
    }
                

    // Update is called once per frame
    void Update()
    {
        
    }
}
