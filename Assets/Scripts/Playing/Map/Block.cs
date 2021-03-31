
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//命名空间，存储map类及子类，构造函数为空，为了使用json序列化

[JsonConverter(typeof(MyConverter))]
public class Map
{
    public String GameVersion;
    public float Bpm;

    public List<BlockBase> Block;

    public Map()
    {
    }

}

public class MyConverter : JsonConverter
{
    public override bool CanRead => true;
    public override bool CanWrite => false;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jobj = serializer.Deserialize<JObject>(reader);//取JObject对象
        if (objectType == typeof(BlockBase))
        {
          
                   String blockType = jobj.Value<String>("type");
                   if (blockType == "Animation")
                   {
                       return serializer.Deserialize<Animation>(reader);
                   }else if (blockType == "Teleportation")
                   {
                       return serializer.Deserialize<Teleportation>(reader);
           
                   }else if (blockType == "Laser")
                   {
                       return serializer.Deserialize<Laser>(reader);
                   }else return null; 
        }else if (objectType == typeof(TileBase))
        {
            List<String> tiles = jobj.Value<List<String>>("tiles"); //取tile名字
            List<TileBase> realTiles = new List<TileBase>();
            foreach (var t in tiles)
            {
                realTiles.Append(Resources.Load<TileBase>("tiles/" + t));
            }
            return realTiles;
        }

        return null;
    }
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        return;
    }//?

    public override bool CanConvert(Type objectType)
    {
        return objectType==typeof(BlockBase) ||objectType==typeof(TileBase);
    }
}

public abstract class BlockBase
{
    [JsonProperty(IsReference = true)]
    public int AppearTime;
    public List<TileBase> Tiles;
    public String Type;
    public abstract IEnumerator Draw();
}

public class StaticBlock : BlockBase
{
    public List<List<int>> matrix;

    public override IEnumerator Draw()
    {
        Painter myPainter = new Painter(Tiles);
        myPainter.DrawVariableMatrix(new Vector2Int(0, 0), matrix);
        yield break;
    }
}
public class AnimationBlock : BlockBase
{//动画类
    public List<List<List<int>>> frame;//每一帧图象
    public Vector2Int basePos;

    public AnimationBlock()
    {
        
    }

    public override IEnumerator Draw()
    {
        Painter myPainter = new Painter(Tiles);
        foreach (var f in frame)
        {
            myPainter.DrawMatrix(basePos,f);
            yield return new WaitForSeconds(1);//todo 时间
            myPainter.SwitchMode();
            myPainter.DrawMatrix(basePos,f);
        }
    }
}
public class Teleportation : BlockBase
{
    public Vector2Int startPos;
    public Vector2Int endPos;
    Teleportation()
    {

    }

    public override IEnumerator Draw()
    {
        Painter myPainter = new Painter(Tiles);
        myPainter.DrawPoint(startPos);
        myPainter.DrawPoint(endPos);
        yield break;
    }
}
public class Laser : BlockBase
{
    public int side;
    public int pos;
    Laser()
    {

    }

    public override IEnumerator Draw()
    {
        Painter myPainter = new Painter(Tiles);
        myPainter.SwitchLayer("Alert");
            if (side == 0)
            {
                
                myPainter.DrawPoint(new Vector2Int(pos, 0));
                myPainter.DrawPoint(new Vector2Int(pos, Painter.Height - 1));
            }
            else if (side == 1)
            {
                myPainter.DrawPoint(new Vector2Int(0, pos));
                myPainter.DrawPoint(new Vector2Int(Painter.Width - 1, pos));
            
            }

            //这里判断一下方位，画出预警点

            yield return new WaitForSeconds(60 / 1 * 3);//todo 等他三秒
            myPainter.SwitchLayer("Sky");
            myPainter.DrawLine(side, pos);

            yield return new WaitForSeconds(60 / 1 * 3);
            myPainter.SwitchMode();
            myPainter.DrawLine(side, pos);
            myPainter.SwitchLayer("Alert");
            myPainter.Clean();
    }
}