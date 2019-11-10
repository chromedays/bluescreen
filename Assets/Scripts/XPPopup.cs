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

    public void AddSizeAnchoredTopLeft(float deltaX, float deltaY)
    {
        Rect.sizeDelta = Rect.sizeDelta + new Vector2(deltaX, deltaY);

        Box2D.size = Rect.sizeDelta;
        Box2D.offset = new Vector2(Box2D.size.x / 2, -Box2D.size.y/2);
    }
                                
    IEnumerator Resize(float time, float deltaSizePerSec)
    {
        float timeLeft = time;
        GetComponent<Rigidbody2D>().simulated = false;
        for (timeLeft = time; timeLeft >= Time.deltaTime; timeLeft -= Time.deltaTime)
        {
            float dt = Time.deltaTime * deltaSizePerSec;
            AddSizeAnchoredTopLeft(-dt, -dt);
            yield return null;
        }

        AddSizeAnchoredTopLeft(-timeLeft* deltaSizePerSec, timeLeft* deltaSizePerSec);
        OnResizeEnd?.Invoke();

        GetComponent<Rigidbody2D>().simulated = true;
    }
      
    public void AnimateResize(float time, float deltaSizePerSec, Func calledAfter)
    {
        OnResizeEnd = calledAfter;

        float unit = time * deltaSizePerSec;

        AddSizeAnchoredTopLeft(unit, unit);
                                                  
        StartCoroutine(Resize(time, deltaSizePerSec));
    }
}
