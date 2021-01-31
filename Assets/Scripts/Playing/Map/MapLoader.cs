using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using UnityEditor;
using Random = UnityEngine.Random;
using MyGame;
using Animation = MyGame.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Assertions.Must;

public class MapLoader : MonoBehaviour
{
    public GameObject camera;
    public Tilemap TeleportationMap;
    public Tilemap GroundMap;
    public Tilemap SkyMap;
    public Tilemap RecoveryMap;
    public Tilemap AlertMap;
    private TileBase[] DynamicTiles;
    public TileBase ObstructionTile;
    public Tile RecoveryTile;
    private Map map;
    private const int width = 9;
    private const int height = 12;

    public Map Map => map;

    // Start is called before the first frame update
    void Start()
    {//之后改为从json加载
        map = new Map(
            );
        String m =
            "{\"bpm\":70,\"groundblock\": [[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0], [0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0],[1,0,1,1,0,0,0,1,0], [1,0,0,1,0,0,0,1,1],[1,0,0,1,0,1,0,1,1],[0,0,1,0,0,1,0,0,1],[0,0,1,0,0,1,0,0,0],[0,0,1,0,1,0,0,1,0],[1,0,0,0,1,0,0,1,0],[1,1,0,0,1,0,0,1,0],[1,1,0,0,0,0,0,0,0],[0,1,0,0,0,0,0,0,0],[0,0,0,1,0,0,1,0,0],[0,0,0,1,0,0,1,0,0],[0,0,0,1,0,0,1,0,0]], \"animation\":[],\"teleportation\":[{startPos:{x:0,y:0},endPos:{x:7,y:4}}],\"laser\":[{pos:3,side:0,appearTime:3}]}";
        map = JsonConvert.DeserializeObject<Map>(m);
        camera.GetComponent<GameMainController>().Bpm = map.bpm;
        DynamicTiles = new TileBase[2] { ObstructionTile, RecoveryTile };
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void drawDynamicMap()
    {
        drawVariableMatrix(0, 0, map.GroundBlock, GroundMap,
            DynamicTiles);
    }

    public void drawStaticMap(List<List<int>> matrix)
    {
        drawMatrix(0, 0, matrix, SkyMap, ObstructionTile);
    }
    public void drawTeleportation()
    {
        foreach (var t in map.Teleportation)
        {
            TeleportationMap.SetTile((Vector3Int)t.startPos, RecoveryTile);
            TeleportationMap.SetTile((Vector3Int)t.endPos, RecoveryTile);
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
        AlertMap.SetTile((Vector3Int)pos, RecoveryTile);

    }
    public void drawLine(int dir, int pos)
    {
        if (dir == 0)
        {
            for (int i = 0; i < width; i++)
            {
                SkyMap.SetTile(new Vector3Int(pos, i, 0), RecoveryTile);
            }
        }
        else if (dir == 1)
        {
            for (int i = 0; i < height; i++)
            {
                SkyMap.SetTile(new Vector3Int(i, pos, 0), RecoveryTile);

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
    public void drawVariableMatrix(int x, int y, List<List<int>> matrix, Tilemap target, TileBase[] source)
    {
        for (int i = 0; i < matrix[0].Count; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                if (matrix[j][i] >= 1)//障碍物
                {
                    target.SetTile(new Vector3Int(x + i, y + j, 0), source[matrix[j][i] - 1]);
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
    /*
        class Flyin:Animation
        {
            public int length;//飞行物长度
            public Vector2 direction;//飞行方向，向量

            public Flyin(Vector2 startPos,):base()
            {//构造函数，在这里生成frame

            }
        }*/
    /*
        class Point : Animation
        {
            public Vector2 direction;

            public Point()
            {
                if(this.direction==Vector2.up) {
                    this.frame[0] = new int[1][2] {new int[][]{0, 1}};
                this.frame[1] = new int[1][2] {
                    new int[]{1, 0}
                };

                ;}else if (this.direction == Vector2.down)
                {
                    this.frame[1] = new int[1, 2]
                    {
                        new int[]{0, 1}
                    };
                    this.frame[0] = new int[1, 2] {{1, 0}};
                }else if (this.direction == Vector2.left)
                {
                    this.frame[0] = new int[2, 1]
                    new int[]{
                        new int[]{0}, new int[]{1}
                    };
                    this.frame[1] = new int[2, 1] {{1}, {0}};
                }else if (this.direction == Vector2.right)
                {
                    this.frame[1] = new int[2, 1]
                    {
                        {0}, {1}
                    };
                    this.frame[0] = new int[2, 1] {{1}, {0}};
                }
            }
        }*/
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