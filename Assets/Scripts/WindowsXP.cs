using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

[System.Serializable]
public class SummonInfo
{                               
    public Vector2 pos; // radio 
    public Vector2 initSize;
    public float sizeDelta;
    public float resizeTime;      
};

[System.Serializable]
public class PatternInfo    
{
    public SummonInfo[] summons;
    public float summonInterval;
}
            

public class WindowsXP : MonoBehaviour
{                                     
    public Canvas ScreenCanvas;
    public GameObject PopupPrefab;
    public AudioSource PopupErrorSound;
    public GameObject MouseGameObj;
                                       
    public List<PatternInfo> Patterns = new List<PatternInfo>();

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(ScreenCanvas, "Screen Canvas should not be null!");
        Assert.IsNotNull(PopupPrefab, "Popup Prefab should not be null!");
        Assert.IsNotNull(PopupErrorSound, "PopupErrorSound should not be null!");
        Assert.IsNotNull(MouseGameObj, "MouseGameObj should not be null!");
    }

    public void DropWindows(int patternIndex)
    {
        Assert.IsNotNull(Patterns, "Pattern is Empty!");

        IEnumerator summonWindows = SummonWindows(Patterns[patternIndex].summonInterval, Patterns[patternIndex].summons);

        StartCoroutine(summonWindows);                      
    }

    
    IEnumerator SummonWindows(float summonInterval, SummonInfo[] summons)
    {                                             
        float elapstedTime = summonInterval;
        Vector2 canvasSize = ScreenCanvas.GetComponent<RectTransform>().sizeDelta;
        Vector2 canvasPos = ScreenCanvas.transform.position;
        canvasPos.x -= canvasSize.x / 2;
        canvasPos.y += canvasSize.y / 2; 
        int windowCount = summons.Length;
        float deltaPosX = canvasSize.x / (windowCount);
                                                            
        while (windowCount > 0)
        {
            elapstedTime += Time.deltaTime;
            if (elapstedTime >= summonInterval)
            {
                SummonInfo info = summons[summons.Length - windowCount];
                CreatePopUp(new Vector2(canvasPos.x + deltaPosX * windowCount, canvasPos.y), info.initSize).AnimateResize(info.resizeTime, info.sizeDelta, null);
                elapstedTime -= summonInterval;
                --windowCount;
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
