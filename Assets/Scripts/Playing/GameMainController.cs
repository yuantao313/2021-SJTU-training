using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainController : MonoBehaviour
{
    // Start is called before the first frame update
    public string Status;
    public float Bpm;
    public GameObject myMap;
    public int Timer;
    

    void Start()
    {
        Timer = 0;
        Status = "running";
        Bpm = myMap.GetComponent<MapLoader>().MyMap.Bpm;
        StartCoroutine(Loop());
    }

    // Update is called once per frame
    void Update()
    {
        if (Status == "stop")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Loop()
    {
        yield return new WaitForSeconds(60/Bpm);
        while (true)
        {
            if (Status == "running")
            {
                Timer++;//计时器+1
                myMap.GetComponentInChildren<GroundMapController>().Drop();
                myMap.GetComponentInChildren<MapLoader>().LoadSkyBlock(Timer);
                yield return new WaitForSeconds(60 / Bpm);
            }else if (Status == "pause")
            {

            }
        }
    }
}
