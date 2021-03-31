using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;
using Newtonsoft.Json;


public class MapLoader : MonoBehaviour
{
    public List<Tilemap> Layer;

    private List<BlockBase> SkyBlock;
    public Map MyMap;

    // Start is called before the first frame update
    void Start()
    {
        MyMap = new Map();
        String m = Resources.Load<TextAsset>("maps/1").text;
        MyMap = JsonConvert.DeserializeObject<Map>(m);
    }

    // Update is called once per frame
    void Update()
    {
    }



/// <summary>
/// 绘制天空方块协程
/// </summary>
    public void LoadSkyBlock(int t)
    {
        foreach (var b in SkyBlock)
        {
            if (t == b.AppearTime)
            {
                StartCoroutine(b.Draw());
            }
        }
    }
}

///地图加载的生命周期
/// 1. mapLoader被UnityEngine实例化，从指定路径加载json文件
/// todo 之后改为利用Newtonsoft.json加载json文件到MapReader类，然后运用MapLoader中的方法将数据加载到MapLoader，之后再绘制，可以避免json加载器加载不了Tile等对象的问题
/// 2. 首先加载GroundBlock到地图中
/// 3. 所有继承自BlockBase的对象组成List存储在SkyMapController中
/// 4. GameMainController进入运转周期，每一行为帧遍历一次Block列表检查时间是否正确；若正确，便将Draw加入协程，并将其从列表移除（加入协程后引用计数是否归零尚未研究）
/// 5. （备注）由于C#不支持不同返回值的重载，所以有些物件加载的Draw方法本来应该是void类型，但受到限制只能使用协程