using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


[System.Serializable]
public enum WindowSize
{
    eExtraSmall, // 1 
    eSmall,     // 2 
    eMedium,    // 3 
    eLarge,     // 5 
    eExtraLarge,// 8 
    eCOUNT
}

[System.Serializable]
public class SummonInfo
{                                
    public WindowSize startWidth;
    public WindowSize startHeight;
    public WindowSize endWidth;
    public WindowSize endHeight;
    public float resizeTime;
    public float posX;
    public bool useThis;
};

[System.Serializable]
public class PatternInfo
{
    public int count;
    public float summonInterval;
    public SummonInfo common;
    public SummonInfo[] summons;
}
                                                
public class WindowsXP : MonoBehaviour
{                                     
    public RectTransform ScreenCanvas;
    public RectTransform WindowsParent;
    public GameObject PopupPrefab;      

    public List<PatternInfo> Patterns = new List<PatternInfo>();

    public int XPosDivisor = 20;
    public Vector2 Margin = new Vector2(0.05f, 0.5f);

    public float WindowAliveTime = 1.0f;
    public float WindowFadeOutTime = 1.0f;
    public float CamShakAmountScaler = 1.0f;
    public float FragmentScaler = 1.0f;

    //Testing
    WindowSize currWin = WindowSize.eExtraSmall;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(ScreenCanvas, "Screen Canvas should not be null!");
        Assert.IsNotNull(WindowsParent, "WindowsParent should not be null!");
        Assert.IsNotNull(PopupPrefab, "Popup Prefab should not be null!");

    }

    float GetSize(WindowSize type)
    {
        const float fixedX = 14.1f;
        float radioX = ScreenCanvas.sizeDelta.x / fixedX;
        switch (type)
        {
            case WindowSize.eExtraSmall:
                return 1 * radioX;
            case WindowSize.eSmall:
                return 2 * radioX;
            case WindowSize.eMedium:
                return 3 * radioX;
            case WindowSize.eLarge:
                return 5 * radioX;
            case WindowSize.eExtraLarge:
                return 8 * radioX;
            default:
                return 4;
        }
    }

    public void DropWindows(int patternIndex)
    {
        Assert.IsNotNull(Patterns, "Pattern is Empty!");
        Assert.IsFalse(Patterns.Count <= patternIndex, "Congrate! Out of Index!");

        IEnumerator summonWindows = SummonWindows(Patterns[patternIndex].summonInterval,
             Patterns[patternIndex].count,
            Patterns[patternIndex].common,
            Patterns[patternIndex].summons);

        StartCoroutine(summonWindows);
    }

    Vector2 ValidateSizeGetNewPos(Vector2 Pos, Vector2 Size)
    {
        Vector2 canvasSize = ScreenCanvas.sizeDelta;

        if (Pos.x + Size.x >= canvasSize.x- Margin.x)
        {
            return new Vector2(canvasSize.x - Size.x - Margin.x, Pos.y);
        }
        else
        {
            return Pos;
        }
    }

    IEnumerator SummonWindows(float summonInterval, int Count, SummonInfo common, SummonInfo[] summons)
    {
        float elapstedTime = summonInterval;

        Vector2 canvasSize = ScreenCanvas.sizeDelta;
        float deltaX = canvasSize.x / XPosDivisor;
                                    
        float posX = common.posX;
        float DX = XPosDivisor / Count;

        while (Count > 0)
        {
            elapstedTime += Time.deltaTime;
            if (elapstedTime >= summonInterval)
            {                           
                SummonInfo info = common;
                // Use children info
                if (summons.Length != 0 && summons[Count - 1].useThis == true)
                {
                    info = summons[Count-1];
                    posX = info.posX;
                }
                else
                {

                    posX = common.posX + DX * (Count-1);
                }

                Vector2 Pos = new Vector2(deltaX*posX, -Margin.y);
                Vector2 Size = new Vector2(GetSize(info.startWidth), GetSize(info.startHeight));
                Vector2 EndSize = new Vector2(GetSize(info.endWidth), GetSize(info.endHeight));

                Pos = ValidateSizeGetNewPos(Pos, Size);
                Vector2 Pos2 = ValidateSizeGetNewPos(Pos, EndSize);
                Pos = new Vector2( Mathf.Min(Pos.x, Pos2.x), Mathf.Min(Pos.y, Pos2.y));
                CreatePopUp(Pos, Size).AnimateResize(info.resizeTime, EndSize);
                elapstedTime -= summonInterval;
                --Count;
            }
            yield return null;
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
        if (Input.GetKeyUp(KeyCode.T))
        {
            XPPopup comp = CreatePopUp(new Vector2(0, 0), new Vector2(GetSize(currWin), GetSize(currWin)));

            RectTransform rect = comp.GetComponent<RectTransform>();
            Debug.Log(rect.position);
            Debug.Log(rect.anchoredPosition);

            ++currWin;
            if (currWin == WindowSize.eCOUNT)
                currWin = WindowSize.eExtraSmall;
        }
    }
}
