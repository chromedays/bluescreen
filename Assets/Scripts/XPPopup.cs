using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class XPPopup : MonoBehaviour
{
    public Button CloseButton;
    public Text Message;
    public RectTransform Rect;
    public BoxCollider2D Box2D;
                                      
    public delegate void Func();
    public Func OnResizeEnd;
                                      
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(CloseButton, "close Button should not be null!");
        Assert.IsNotNull(Message, "message Text should not be null!");

        Assert.IsNotNull(Rect, "Rect should not be null!");
        Assert.IsNotNull(Box2D, "Box2D should not be null!");

    }

    // Update is called once per frame
    void Update()
    {                   
                               
    }

    public void SetSizeAnchoredTopLeft(Vector2 size)
    {
        Rect.sizeDelta = size;
    }
                                
    IEnumerator Resize(float time, float deltaSizePerSec)
    {
        float timeLeft = time;
        GetComponent<Rigidbody2D>().simulated = false;
        for (timeLeft = time; timeLeft >= Time.deltaTime; timeLeft -= Time.deltaTime)
        {
            float dt = Time.deltaTime * deltaSizePerSec;
            SetSizeAnchoredTopLeft(Rect.sizeDelta + new Vector2(-dt, -dt));
            yield return null;
        }

        SetSizeAnchoredTopLeft(Rect.sizeDelta + new Vector2(-timeLeft* deltaSizePerSec, timeLeft* deltaSizePerSec));
        OnResizeEnd?.Invoke();

        GetComponent<Rigidbody2D>().simulated = true;
    }
      
    public void AnimateResize(float time, float deltaSizePerSec, Func calledAfter)
    {
        OnResizeEnd = calledAfter;

        float unit = time * deltaSizePerSec;
                           
        Vector2 newSize = Rect.sizeDelta + new Vector2(unit, unit);

        SetSizeAnchoredTopLeft(newSize);
                                                  
        StartCoroutine(Resize(time, deltaSizePerSec));
    }
}
