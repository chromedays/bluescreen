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

    public float WindowResizeTime;
 
 
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(ScreenCanvas, "Screen Canvas should not be null!");
        Assert.IsNotNull(PopupPrefab, "Popup Prefab should not be null!");
        Assert.IsNotNull(PopupErrorSound, "PopupErrorSound should not be null!");
        Assert.IsNotNull(MouseGameObj, "MouseGameObj should not be null!");
    }                                                             
    public void DropWindows(float resizeTime, float summonInterval, float windowCounts)
    {                                   
        StartCoroutine(SummonWindows(resizeTime, summonInterval, windowCounts));
    }

    IEnumerator SummonWindows(float resizeTime, float summonInterval, float windowCounts)
    {                                             
        float elapstedTime = summonInterval;
        Vector2 position = new Vector2(-5, 10);
        int c = 0;
        while(c < windowCounts)
        {
            elapstedTime += Time.deltaTime;
            if (elapstedTime >= summonInterval)
            {
                Debug.Log(c + "th windows: ");
                CreatePopUp(position).AnimateResize(resizeTime, null);
                elapstedTime -= summonInterval;
                position += new Vector2(0.5f, 0);
                ++c;
            }
            yield return null;
        }
    }

    XPPopup CreatePopUp(Vector2 position)
    {
        GameObject Popup = Instantiate(PopupPrefab, ScreenCanvas.transform);

        RectTransform rect = Popup.GetComponent<RectTransform>();
        rect.localPosition = position;

        XPPopup popupComp = Popup.GetComponent<XPPopup>();

        PopupErrorSound.Play();
  
        return popupComp;
    }
                

    // Update is called once per frame
    void Update()
    {
        
    }
}
