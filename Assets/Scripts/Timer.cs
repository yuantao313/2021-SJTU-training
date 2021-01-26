using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text secondText;
    private DateTime time;
    void Start()
    {
        time=DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan oTime = DateTime.Now.Subtract(this.time);
        secondText.text = oTime.TotalSeconds.ToString();
    }
}
