using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
                     
public enum POS_TYPE
{          
    _0,
    _1_2,
    _1_3,
    _1_4,
    _1_5,
    _1_6,
    _1_7,
    _1_8,
    _1_9,
    _1_10,
    _1_11,
    _1_12,
    _1_13,
    _1_14,
    _1_15,
    _1_16,
    _1_17,
    _1_18,
    _1_19,
    _1_20,
};



public class WindowsXP : MonoBehaviour
{                             
    public RectTransform ScreenCanvas;
    public RectTransform WindowsParent;
    public GameObject PopupPrefab;

    [Header("Pattern")]
    public List<PatternInfo> Patterns = new List<PatternInfo>();
    public delegate void OnPatternEnd();
    public OnPatternEnd onPatternEnd;
    public int startPattern = 0;
    public bool loop = false;


    [Header("Layout")]
    public Vector2 Margin = new Vector2(0.05f, 0.5f);

    [Header("Common Window Attribute")]
    public float WindowAliveTime = 1.0f;
    public float WindowFadeOutTime = 1.0f;
    public float CamShakAmountScaler = 1.0f;
    public float FragmentScaler = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(ScreenCanvas, "Screen Canvas should not be null!");
        Assert.IsNotNull(WindowsParent, "WindowsParent should not be null!");
        Assert.IsNotNull(PopupPrefab, "Popup Prefab should not be null!");


        StartAllPatternsLoop();

    }
                                      
    public Coroutine DropWindows(int patternIndex)
    {
        Assert.IsNotNull(Patterns, "Pattern is Empty!");
        Assert.IsFalse(Patterns.Count <= patternIndex, "Congrate! Out of Index!");

        IEnumerator runPattern = RunPattern(Patterns[patternIndex]);

        return StartCoroutine(runPattern);
    }
                               
    Vector2 ComputeSize(SizeInfo data)
    {
        Vector2 availableSpace = ScreenCanvas.sizeDelta - 2*Margin;
        return new Vector2(availableSpace.x * data.sizeX, availableSpace.y * data.sizeY);
    }

    public void StartAllPatternsLoop()
    {
        onPatternEnd = () => {
            DropWindows(startPattern);
            if (!loop)
            {
                ++startPattern;
                if (Patterns.Count == startPattern)
                    startPattern = 0;
            }
        };
            
        DropWindows(startPattern);
        if(!loop)
            ++startPattern;
    }


    IEnumerator RunPattern(PatternInfo pattern)
    {
        float elapstedTime = pattern.summonInterval;
                                          
        int Count = 0;

        while (pattern.data.Length > Count)
        {
            elapstedTime += Time.deltaTime;
            if (elapstedTime >= pattern.summonInterval)
            {
                SummonWindow(pattern.data[Count]);
                elapstedTime -= pattern.summonInterval;
                ++Count;
            }
            yield return null;
        }

        yield return new WaitForSeconds(pattern.coolTime);

        onPatternEnd?.Invoke();
    }

    void SummonWindow(SummonInfo info)
    {                                                         
        Vector2 StartSize = ComputeSize(info.startSize);
        Vector2 EndSize = ComputeSize(info.endSize);

        float availableSpaceX = ScreenCanvas.sizeDelta.x - Margin.x*2 -Mathf.Max(StartSize.x, EndSize.x);

        Vector2 Pos = new Vector2(0, 0);
        for (int i = 0; i < info.scaler.Length; ++i)
        {
            if (info.pos != 0)
                Pos.x = Margin.x + availableSpaceX * (info.scaler[i] / (float)info.pos);
            else
                Pos.x = 0.0f;

            CreatePopUp(Pos, StartSize).AnimateResize(info.resizeTime, EndSize);
        }
    }

    XPPopup CreatePopUp(Vector2 position, Vector2 size)
    {
        // rect.anchoredPosition 
        // TopLeft = [0, 0]
        // TopRight = [canvasSize.x, 0]
        // BotLeft = [0, -canvasSize.y]
        GameObject Popup = Instantiate(PopupPrefab, WindowsParent.transform);

        RectTransform rect = Popup.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        XPPopup popupComp = Popup.GetComponent<XPPopup>();
        return popupComp;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
