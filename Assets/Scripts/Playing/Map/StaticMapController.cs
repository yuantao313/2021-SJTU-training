using System;
using System.Collections;
using System.Collections.Generic;
using MyGame;
using UnityEngine;
using UnityEngine.Tilemaps;
using Animation = MyGame.Animation;
 /// <summary>
 /// StaticMapController加载、控制“并不下落”的障碍物。
/// 也可以理解为控制所有动画型障碍物。
 /// </summary>
public class StaticMapController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    private int width = 9;
    private int height = 12;
    private List<Animation> anima;
    private MapLoader _mapLoader;
    private float bpm;
    void Start()
    {
        bpm = camera.GetComponent<GameMainController>().Bpm;
        _mapLoader = GetComponentInParent<MapLoader>();
        anima = GetComponentInParent<MapLoader>().Map.Animation;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void checkAnimation(int t)
    {
        print(anima);
        foreach (var a in anima)
        {
            if (a.appearTime == t)
            {
                StartCoroutine(drawAnimation(a));
                anima.Remove(a);
            }
        }
    }

    public IEnumerator drawAnimation(Animation a)
    {
        foreach (var f in a.frame)
        {
            _mapLoader.drawMatrix(a.startPos[0], a.startPos[1],  f, _mapLoader.SkyMap, _mapLoader.ObstructionTile);
            yield return new WaitForSeconds(60 / bpm);
            _mapLoader.drawMatrix(a.startPos[0], a.startPos[1],  f, _mapLoader.SkyMap, null);
        }
    }
}
