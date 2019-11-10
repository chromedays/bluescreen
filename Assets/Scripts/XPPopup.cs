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

    public bool HitPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(CloseButton, "close Button should not be null!");
        Assert.IsNotNull(Message, "message Text should not be null!");
        Assert.IsNotNull(Rect, "Rect should not be null!");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Resize(float time, Vector2 deltaShrink)
    {
        float timeLeft = time;
        GetComponent<Rigidbody2D>().simulated = false;

        for (timeLeft = time; timeLeft >= Time.deltaTime; timeLeft -= Time.deltaTime)
        {
            Rect.sizeDelta = Rect.sizeDelta + deltaShrink * Time.deltaTime;
            yield return null;
        }

        Rect.sizeDelta = Rect.sizeDelta + deltaShrink * timeLeft;
        //OnResizeEnd?.Invoke();

        GetComponent<Rigidbody2D>().simulated = true;
    }
                              
    public void AnimateResize(float time, Vector2 endSize)
    {
        if (time != 0.0f)
        {
            Vector2 deltaShrink = (endSize - Rect.sizeDelta) / time;
            StartCoroutine(Resize(time, deltaShrink));
        }
    }
}
