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
    public bool Destorying = false;

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

    IEnumerator FadeOutAndDestroy(float aliveTime, float fadeoutTime)
    {
        yield return new WaitForSeconds(aliveTime);        

        Destorying = true;

        Image[] images = GetComponentsInChildren<Image>(false);

        foreach (Image img in images)
            img.color += new Color(0, 0, 0, -img.color.a / 3);

        float dA = 1.0f / fadeoutTime; 
        while (fadeoutTime > 0)
        {
            foreach (Image img in images)
                img.color += new Color(0,0,0,-dA * Time.deltaTime);
            fadeoutTime -= Time.deltaTime;
            yield return null;
        }             
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {                                 
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            Game.Inst.FragmentGenerator.CreateFragment(transform.position, 1);

            Game.Inst.CameraShake.ShakeCamera();
            
            StartCoroutine(FadeOutAndDestroy(Game.Inst.WindowsXP.WindowAliveTime, 
                Game.Inst.WindowsXP.WindowFadeOutTime));
        }
    }
}
