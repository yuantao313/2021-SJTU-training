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
    private List<Animation> myAnimation;
    private MapLoader _mapLoader;
    private float bpm;
    void Start()
    {
        bpm = camera.GetComponent<GameMainController>().Bpm;
        _mapLoader = GetComponentInParent<MapLoader>();
        myAnimation = _mapLoader.Map.Animation;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void LoadAnimation(int t)
    {
        foreach (var a in myAnimation)
        {
            if (a.appearTime == t)
            {
                StartCoroutine(drawAnimation(a));
                myAnimation.Remove(a);
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
