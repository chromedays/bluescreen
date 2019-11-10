using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFitter : MonoBehaviour
{
    public Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float canvasHeight = Cam.orthographicSize * 2;
        float canvasWidth = canvasHeight * Cam.aspect;
        GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);
    }
}
