using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class XPPopup : MonoBehaviour
{                                     
    public RectTransform Rect;

    public bool HitPlayer = false;
    public bool Destorying = false;

    public Vector2 EndSize;
    public float EndSizeArea = 1;
                                               

    // Start is called before the first frame update
    void Start()
    {
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

        GetComponent<Rigidbody2D>().simulated = true;
    }

    public void AnimateResize(float time, Vector2 endSize)
    {
        EndSize = endSize;
        EndSizeArea = endSize.x * endSize.y;
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
                img.color += new Color(0, 0, 0, -dA * Time.deltaTime);
            fadeoutTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            CameraShake cs = Game.Inst.CameraShake;
            WindowsXP xp = Game.Inst.WindowsXP;
            FragmentGenerator fg = Game.Inst.FragmentGenerator;


            Vector2 offset = new Vector2(
                Random.Range(0, EndSize.x), 
                Random.Range(0, -EndSize.y));   
            fg.CreateFragment(transform.position.ToVector2() + offset, EndSizeArea * xp.FragmentScaler);

            cs.ShakeAmount = xp.CamShakAmountScaler * EndSizeArea;
            cs.ShakeCamera();

            StartCoroutine(FadeOutAndDestroy(xp.WindowAliveTime, xp.WindowFadeOutTime));

            Game.Inst.WindowsHP.ReduceLife();
        }
    }
}
