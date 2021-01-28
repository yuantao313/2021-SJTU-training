using System;
using System.Collections;
using System.Collections.Generic;
using MyGame;
using UnityEngine;
using UnityEngine.Tilemaps;
using Animation = MyGame.Animation;

public class StaticMapController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    private List<List<int>> bufferArea;
    private int width=9;
    private int height = 12;
    private List<Animation> anima;
    private MapLoader _mapLoader;
    void Start()
    {
        _mapLoader = GetComponentInParent<MapLoader>();
        bufferArea = new List<List<int>>();
        List<int> a = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0};
        for (int i = 0; i < 9; i++)
        {
            bufferArea.Add(a);
        }
        this.anima = GetComponentInParent<MapLoader>().Map.Animation;
        StartCoroutine(checkAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator checkAnimation()
    {
        //new WaitForSeconds(0.1f);
        while (true)
        {
           foreach (var a in this.anima)
                   {
                       if (a.appearTime == camera.GetComponent<GameMainController>().timer)
                       {
                           
                           StartCoroutine(drawAnimation(a));
                           a.appearTime = 0;
                       }
                   }
            yield return new WaitForSeconds(1); 
        }
        
    }

    public IEnumerator drawAnimation(Animation a)
    {
        foreach (var f in a.frame)
        {
            
            _mapLoader.drawMatrix(a.startPos[0],a.startPos[1],3,3,f,_mapLoader.SkyMap,_mapLoader.ObstructionTile);
            yield return new WaitForSeconds(1);
            _mapLoader.drawMatrix(a.startPos[0],a.startPos[1],3,3,f,_mapLoader.SkyMap,new Tile());

        }
    }
    /*public IEnumerator addAnimationToBuffer(Animation a)
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
                        this.bufferArea[i+a.startPos[0]][j+a.startPos[1]] = 1;
                    }
                }
            }
            yield return new WaitForSeconds(1);
        }
    }*/
    /*public IEnumerator drawBuffer()
    {
        while (true)
        {
            GetComponentInParent<MapLoader>().clearStaticMap();
            if (camera.GetComponent<GameMainController>().Status == "running")
            {
                yield return new WaitForEndOfFrame();
                this.GetComponentInParent<MapLoader>().drawStaticMap(this.bufferArea);
            }
            yield return new WaitForSeconds(1);
            clearBufferArea();//清空缓冲区
        }
    }*/

    private void clearBufferArea()
    {
        bufferArea = new List<List<int>>();
        List<int> a = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0};
        for (int i = 0; i < 9; i++)
        {
            bufferArea.Add(a);
        }
    }
}
