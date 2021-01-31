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
    private TileBase[] DynamicTiles;
    public TileBase ObstructionTile;
    public Tile RecoveryTile;
    private Map map;
    

    public Map Map => map;

    // Start is called before the first frame update
    void Start()
    {//之后改为从json加载
        map = new Map(
            );
        String m =
            "{\"bpm\":70,\"groundblock\": [[0,0,2,0,0,0,2,0,0],[0,0,0,2,0,2,0,0,0],[0,0,0,0,2,0,0,0,0], [0,0,0,2,0,2,0,0,0],[0,2,0,0,0,0,0,2,0],[0,0,1,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0],[1,0,1,1,0,0,0,1,0], [1,0,0,1,0,0,0,1,1],[1,0,0,1,0,1,0,1,1],[0,0,1,0,0,1,0,0,1],[0,0,1,0,0,1,0,0,0],[0,0,1,0,1,0,0,1,0],[1,0,0,0,1,0,0,1,0],[1,1,0,0,1,0,0,1,0],[1,1,0,0,0,0,0,0,0],[0,1,0,0,0,0,0,0,0],[0,0,0,1,0,0,1,0,0],[0,0,0,1,0,0,1,0,0],[0,0,0,1,0,0,1,0,0]], \"animation\":[]}";
        map = JsonConvert.DeserializeObject<Map>(m);
        camera.GetComponent<GameMainController>().Bpm = map.bpm;
        DynamicTiles = new TileBase[2] {ObstructionTile,RecoveryTile};
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
        drawMatrix(0,0,matrix,SkyMap,ObstructionTile);
    }

    public void clearDynamicMap()
    {
        GroundMap.ClearAllTiles();
    }
    public void clearStaticMap()
    {
        SkyMap.ClearAllTiles();
    }
    public void drawMatrix(int x, int y, List<List<int>> matrix,Tilemap target,TileBase source)
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
    public void drawVariableMatrix(int x, int y, List<List<int>> matrix,Tilemap target,TileBase[] source)
    {
        for (int i = 0; i < matrix[0].Count; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                if (matrix[j][i] >=1)//障碍物
                {
                    target.SetTile(new Vector3Int(x + i, y + j, 0), source[matrix[j][i]-1]);
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
    class Teleportation
    {
        public Vector2 startPos;
        public Vector2 endPos;
    }
    
}