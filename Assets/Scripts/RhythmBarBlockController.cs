using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBarBlockController : MonoBehaviour
{
    public GameObject RhythmBarBlock;
    private float ix;

    private float width;

    private float bpm = 60;

    private DateTime time;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = RhythmBarBlock.transform.position;
        this.ix = position.x;
        time=DateTime.Now;
        width = 7f;

    }

    // Update is called once per frame
    void Update()
    {
        float T = this.bpm / 60;
        TimeSpan oTime = DateTime.Now.Subtract(this.time);
        float now = (float)oTime.TotalSeconds % T;
        float ratio = now / T;
        Vector2 position = RhythmBarBlock.transform.position;
        position.x = ix+width * (ratio-0.5f);
        transform.position = position;
    }
}
