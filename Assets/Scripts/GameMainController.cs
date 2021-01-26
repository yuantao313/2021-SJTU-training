using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainController : MonoBehaviour
{
    // Start is called before the first frame update
    private DateTime time;
    private bool running;
    public Text gameOverText;
    public bool Running
    {
        get => running;
        set => running = value;
    }
    void Start()
    {
        time=DateTime.Now;
        this.running = true;
        this.gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan oTime = DateTime.Now.Subtract(this.time);
        if (!running)//若因外界条件被告知游戏结束
        {
            this.gameOverText.gameObject.SetActive(true);
        }
    }
    
}
