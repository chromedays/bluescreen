using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEverything : MonoBehaviour
{
    WindowsXP Xp;

    public float WindowResizeTime;        

    // Start is called before the first frame update
    void Start()
    {
        Xp = GameObject.FindObjectOfType<WindowsXP>();   
    }
                                       
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (Xp)
            {
                XPPopup popup = Xp.CreatePopUp( Random.insideUnitCircle* Random.Range(1, 5));
                popup.AnimateResize(WindowResizeTime, null);
            }
            else
            {
                Xp = GameObject.FindObjectOfType<WindowsXP>();
            }
        }
    }
}
