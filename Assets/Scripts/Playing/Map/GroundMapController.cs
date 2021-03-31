using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// DynamicMapController加载、控制所有下落型的障碍物。
/// </summary>
public class GroundMapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    //下落式的实现方法为直接改变Tilemap的Grid坐标。
    public void Drop()
    {
        transform.position += Vector3.down * 0.8f;
    }
}
