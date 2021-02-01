using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainController : MonoBehaviour
{
    // Start is called before the first frame update
    private string status;
    private float bpm;
    public GameObject myMap;
    private int timer;
    public float Bpm
    {
        get => bpm;
        set => bpm = value;
    }
    public string Status
    {
        get => status;
        set => status = value;
    }

    void Start()
    {
        timer = 0;
        this.status = "running";
        StartCoroutine(Loop());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status!="running") //若因外界条件被告知游戏结束
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator Loop()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (this.status == "running")
            {
                timer++;//计时器+1
                myMap.GetComponentInChildren<DynamicMapController>().Drop();
                myMap.GetComponentInChildren<StaticMapController>().LoadAnimation(timer);
                myMap.GetComponentInChildren<LaserController>().LoadLaser(timer);
                yield return new WaitForSeconds(60 / bpm);

            }
            //drop     
        }
    }

}
