using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// DynamicMapController加载、控制所有下落型的障碍物。
/// </summary>
public class DynamicMapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInParent<MapLoader>().drawDynamicMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //下落式的实现方法为直接改变Tilemap的Grid坐标。
    public void Drop()
    {
        this.transform.position += Vector3.down * 0.8f;
    }

}
