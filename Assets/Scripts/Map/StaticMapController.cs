using System;
using System.Collections;
using System.Collections.Generic;
using MyGame;
using UnityEngine;
using Animation = MyGame.Animation;

public class StaticMapController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    private List<List<int>> bufferArea;
    private int width=9;
    private int height = 12;
    private List<Animation> anima;
    void Start()
    {
        bufferArea = new List<List<int>>();
        this.anima = GetComponentInParent<MapLoader>().Map.Animation;
        StartCoroutine(checkAnimation());
        StartCoroutine(drawBuffer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator checkAnimation()
    {
        foreach (var a in this.anima)
        {
            if (a.appearTime == camera.GetComponent<GameMainController>().timer)
            {
                StartCoroutine(addAnimationToBuffer(a));
                a.appearTime = 0;
            }
        }
        yield return new WaitForSeconds(1);
    }

    public IEnumerator addAnimationToBuffer(Animation a)
    {
        int frameCount = a.frame.Count;
        foreach (var f in a.frame)
        {
            for (int i = 0; i < f.Count; i++)
            {
                for (int j = 0; j < f[0].Count; j++)
                {
                    if (f[j][i] == 1)
                    {
                        this.bufferArea[i][j] = 1;
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    public IEnumerator drawBuffer()
    {
        
        while (true)
        {
            GetComponentInParent<MapLoader>().clearStaticMap();
            if (camera.GetComponent<GameMainController>().Status == "running")
            {
                yield return new WaitForEndOfFrame();
                this.GetComponentInParent<MapLoader>().drawStaticMap(this.bufferArea);
                bufferArea = new List<List<int>>();//清空缓冲区
            }
            yield return new WaitForSeconds(1);
        }
    }
}
