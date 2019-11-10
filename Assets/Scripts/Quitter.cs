﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndCo());
    }

    // Update is called once per frame
    IEnumerator EndCo()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
