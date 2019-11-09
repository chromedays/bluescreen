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

    public float DeltaSizePerSec;      


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
                                
    IEnumerator Resize(float time)
    {
        float timeLeft = time;
        for (timeLeft = time; timeLeft >= Time.deltaTime; timeLeft -= Time.deltaTime)
        {
            float dt = Time.deltaTime * DeltaSizePerSec;
            AddSizeAnchoredTopLeft(-dt, -dt);
            yield return null;
        }

        AddSizeAnchoredTopLeft(-timeLeft*DeltaSizePerSec, timeLeft*DeltaSizePerSec);
        OnResizeEnd?.Invoke();
    }
      
    public void AnimateResize(float time, Func calledAfter)
    {
        OnResizeEnd = calledAfter;

        float unit = time * DeltaSizePerSec;

        AddSizeAnchoredTopLeft(unit, unit);

        StartCoroutine("Resize", time);
    }
}
