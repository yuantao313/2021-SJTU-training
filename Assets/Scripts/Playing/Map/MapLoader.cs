using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using UnityEditor;
using MyGame;
using Animation = MyGame.Animation;
using Newtonsoft.Json;


public class MapLoader : MonoBehaviour
{
    public GameObject camera;
    public Tilemap TeleportationMap;
    public Tilemap GroundMap;
    public Tilemap SkyMap;
    public Tilemap RecoveryMap;
    public Tilemap AlertMap;
    private List<TileBase> DynamicTiles;
    private Map map;
    private const int width = 9;
    private const int height = 12;

    public Map Map => map;

    // Start is called before the first frame update
    void Start()
    {
        map = new Map();
        String m = Resources.Load<TextAsset>("maps/1").text;
        print(m);
        map = JsonConvert.DeserializeObject<Map>(m);
        camera.GetComponent<GameMainController>().Bpm = map.bpm;
        DynamicTiles = new List<TileBase> { 
            Resources.Load<TileBase>("tiles/flame"), 
            Resources.Load<TileBase>("tiles/green")
    };
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void drawDynamicMap()
    {
        drawVariableMatrix(0, 0, map.GroundBlock, GroundMap);
    }

    public void drawStaticMap(List<List<int>> matrix)
    {
        drawMatrix(0, 0, matrix, SkyMap, DynamicTiles[0]);
    }
    public void drawTeleportation()
    {
        foreach (var t in map.Teleportation)
        {
            TeleportationMap.SetTile((Vector3Int)t.startPos, DynamicTiles[1]);
            TeleportationMap.SetTile((Vector3Int)t.endPos, DynamicTiles[1]);
        }
    }

    public void clearDynamicMap()
    {
        GroundMap.ClearAllTiles();
    }
    public void clearStaticMap()
    {
        SkyMap.ClearAllTiles();
    }
    public void drawAlert(Vector2Int pos)
    {
        AlertMap.SetTile((Vector3Int)pos, DynamicTiles[1]);

    }
    public void cleanAlert()
    {
        AlertMap.ClearAllTiles();
    }
    public void drawLine(int dir, int pos)
    {
        if (dir == 0)
        {
            for (int i = 0; i < map.height; i++)
            {
                SkyMap.SetTile(new Vector3Int(pos, i, 0), DynamicTiles[1]);
            }
        }
        else if (dir == 1)
        {
            for (int i = 0; i < map.width; i++)
            {
                SkyMap.SetTile(new Vector3Int(i, pos, 0), DynamicTiles[1]);

            }
        }
    }
    public void eraseLine(int dir, int pos)
    {
        if (dir == 0)
        {
            for (int i = 0; i < map.height; i++)
            {
                SkyMap.SetTile(new Vector3Int(pos, i, 0),null);
            }
        }
        else if (dir == 1)
        {
            for (int i = 0; i < map.width; i++)
            {
                SkyMap.SetTile(new Vector3Int(i, pos, 0), null);

            }
        }
    }
    public void drawMatrix(int x, int y, List<List<int>> matrix, Tilemap target, TileBase source)
    {
        for (int i = 0; i < matrix[0].Count; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                if (matrix[j][i] != 0)//障碍物
                {
                    target.SetTile(new Vector3Int(x + i, y + j, 0), source);
                }
            }
        }
    }
    public void drawVariableMatrix(int x, int y, List<List<int>> matrix, Tilemap target)
    {
        for (int i = 0; i < matrix[0].Count; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                if (matrix[j][i] >= 1)//障碍物
                {
                    target.SetTile(new Vector3Int(x + i, y + j, 0), DynamicTiles[matrix[j][i] - 1]);
                }
            }
        }
    }
}
namespace MyGame
{
    /**
     * ^y轴
     * │
     * │
     * │
     *─┼──────────>x轴
     */
    //命名空间，存储map类及子类，构造函数为空，为了使用json序列化
    public class Map
    {
        public float bpm;
        public int width;
        public int height;
        public List<List<int>> GroundBlock;
        public List<Animation> Animation;
        public List<Teleportation> Teleportation;
        public List<Laser> Laser;

        public Map()
        {
        }

    }


    public class Animation
    {//动画类
        public int appearTime;
        public List<List<List<int>>> frame;//每一帧图象
        public List<int> startPos;
        public Animation()
        {

        }
    }
    public class Teleportation
    {
        public Vector2Int startPos;
        public Vector2Int endPos;
        Teleportation()
        {

        }

    }
    public class Laser
    {
        public int side;
        public int pos;
        public int appearTime;
        Laser()
        {

        }
    }
}