using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class XPPopup : MonoBehaviour
{
    public Button closeButton;
    public Text message;                        
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(closeButton, "close Button should not be null!");
        Assert.IsNotNull(message, "message Text should not be null!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
