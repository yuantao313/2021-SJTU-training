using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool running;
    private String status;
    public int timer;
    public double T;
    public string Status
    {
        get => status;
        set => status = value;
    }

    void Start()
    {
        this.status = "running";
        this.timer = -1;
        StartCoroutine(counter());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status!="running") //若因外界条件被告知游戏结束
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator counter()
    {
        while (true)
        {
            if (this.status == "running")
            {
                this.timer++;
                yield return new WaitForSeconds(1);
            }
        }
    }

}
