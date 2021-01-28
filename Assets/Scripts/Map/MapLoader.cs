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
    public Tilemap TeleportationMap;
    public Tilemap GroundMap;
    public Tilemap SkyMap;
    public Tilemap RecoveryMap;
    private Tile[] DynamicTiles;
    public Tile ObstructionTile;
    public Tile RecoveryTile;
    private Map map;
    

    public Map Map => map;

    // Start is called before the first frame update
    void Start()
    {//之后改为从json加载
        this.map = new Map(
            );
        String m =
            "{\"width\":9,\"height\":20,\"groundblock\": [[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0], [0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0],[0,0,1,0,0,0,0,0,0],[1,0,1,1,0,0,0,1,0], [1,0,0,1,0,0,0,1,1],[1,0,0,1,0,1,0,1,1],[0,0,1,0,0,1,0,0,1],[0,0,1,0,0,1,0,0,0],[0,0,1,0,1,0,0,1,0],[1,0,0,0,1,0,0,1,0],[1,1,0,0,1,0,0,1,0],[1,1,0,0,0,0,0,0,0],[0,1,0,0,0,0,0,0,0],[0,0,0,1,0,0,1,0,0],[0,0,0,1,0,0,1,0,0],[0,0,0,1,0,0,1,0,0]], \"animation\":[{\"startPos\": [0,0],\"appearTime\": 5,\"frame\": [[[0,0,0],[0,1,0],[0,0,0]],[[0,1,0],[1,0,1],[0,1,0]], [[1,1,1],[1,0,1],[1,1,1]]]},{\"startPos\": [4,7],\"appearTime\": 4,\"frame\": [[[0,0,0],[0,1,0],[0,0,0]],[[0,1,0],[1,0,1],[0,1,0]], [[1,1,1],[1,0,1],[1,1,1]]]},{\"startPos\": [7,0],\"appearTime\": 8,\"frame\": [[[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,0,1,0,0,0,0]],[[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,0,1,0,1,0,0,0],[0,0,0,0,1,0,0,0,0]],[[0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0],[0,0,1,0,0,0,1,0,0],[0,0,0,1,0,1,0,0,0],[0,0,0,0,1,0,0,0,0]],[[0,0,0,0,0,0,0,0,0],[0,1,0,0,0,0,0,1,0],[0,0,1,0,0,0,1,0,0],[0,0,0,1,0,1,0,0,0],[0,0,0,0,1,0,0,0,0]],[[1,0,0,0,0,0,0,0,1],[0,1,0,0,0,0,0,1,0],[0,0,1,0,0,0,1,0,0],[0,0,0,1,0,1,0,0,0],[0,0,0,0,1,0,0,0,0]]]}]}";
        map = JsonConvert.DeserializeObject<Map>(m);
        this.DynamicTiles = new Tile[2] {this.ObstructionTile, this.RecoveryTile};
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void drawDynamicMap()
    {
        drawVariableMatrix(0, 0, this.map.width, this.map.height, this.map.GroundBlock, this.GroundMap,
            this.DynamicTiles);
    }

    public void drawStaticMap(List<List<int>> matrix)
    {
        drawMatrix(0,0,9,5,matrix,SkyMap,ObstructionTile);
    }

    public void clearDynamicMap()
    {
        GroundMap.ClearAllTiles();
    }
    public void clearStaticMap()
    {
        SkyMap.ClearAllTiles();
    }
    public void drawMatrix(int x, int y, int w, int h, List<List<int>> matrix,Tilemap target,Tile source)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (matrix[j][i] != 0)//障碍物
                {
                    target.SetTile(new Vector3Int(x + i, y + j, 0), source);
                }
            }
        }
    }
    public void drawVariableMatrix(int x, int y, int w, int h, List<List<int>> matrix,Tilemap target,Tile[] source)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
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
        public int getLength()
        {
            return this.frame.Count;
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