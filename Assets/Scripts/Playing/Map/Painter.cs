using System;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
/**
 * ^y轴
 * │
 * │
 * │
 *─┼──────────>x轴
 */
/// <summary>
/// 地图绘制工具类
/// </summary>
public class Painter
{
    private List<TileBase> Tiles;
    private bool cleanMode;
    public static List<Tilemap> Layer;
    public static int Width;
    public static int Height;
    private int LayerNumber;
    
    /// <summary>
    /// 切换绘制模式。
    /// </summary>
    public void SwitchMode()
    {
            
        //cleanMode = cleanMode ? false : true; Rider的智能提示
        cleanMode = !cleanMode;
    }

    /// <summary>
    /// 根据状态，取得默认绘图方块。
    /// </summary>
    /// <returns></returns>
    private TileBase getDefaultTile()
    {
        return cleanMode ? Tiles[0] : Tiles[1];
    }

    /// <summary>
    /// 切换Layer。
    /// </summary>
    /// <param name="LayerName">Layer名称</param>
    public void SwitchLayer(String LayerName) 
    { 
        if (LayerName == "Sky")
        {
            LayerNumber = 1;
        }else if (LayerName == "Ground")
        {
                LayerNumber = 0;
            }else if (LayerName == "Alert")
            {
                LayerNumber = 2;
            }
        }

        private Tilemap getLayer()
        {
            return Layer[LayerNumber];
        }

        //constructor without params
        public Painter()
        {
            Tiles=new List<TileBase>();
            Tiles.Append(null);        
        }

        public Painter(List<TileBase> Tiles)
        {
            this.Tiles = Tiles;
        }
        static Painter()
        {
            MapLoader myMapLoader = GameObject.FindWithTag("Map").GetComponent<MapLoader>();
            Layer = myMapLoader.Layer;
        }
        /// <summary>
        /// 在指定坐标处画点。
        /// </summary>
        public void DrawPoint(Vector2Int pos)
        {
            getLayer().SetTile((Vector3Int)pos,getDefaultTile());
        }
        /// <summary>
        /// 在指定横纵坐标出绘制贯穿地图的线形。
        /// 这个方法可以用于绘制激光。
        /// </summary>
        public void DrawLine(int dir, int pos)
        {
            
            if (dir == 0)
            {
                for (int i = 0; i < Height; i++)
                {
                    getLayer().SetTile(new Vector3Int(pos, i, 0), getDefaultTile());
                }
            }
            else if (dir == 1)
            {
                for (int i = 0; i < Width; i++)
                {
                    getLayer().SetTile(new Vector3Int(i, pos, 0), getDefaultTile());
                }
            }
        }
        /// <summary>
        /// 绘制矩形。
        /// </summary>
        public  void DrawSquare(Vector2Int basePos, int width, int height)
        {
         
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    getLayer().SetTile(new Vector3Int(basePos.x + i, basePos.y + j, 0),getDefaultTile());
                }
            }
        }
        /// <summary>
        /// 绘制矩阵。
        /// </summary>
        public  void DrawMatrix(Vector2Int basePos, List<List<int>> matrix)
        {
 
            for (int i = 0; i < matrix[0].Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    if (matrix[j][i] != 0)//障碍物
                    {
                        getLayer().SetTile(new Vector3Int(basePos.x + i, basePos.y + j, 0), getDefaultTile());
                    }
                }
            }
        }
        /// <summary>
        /// 按照指定tile列表绘制矩阵。
        /// </summary>
        public  void DrawVariableMatrix(Vector2Int basePos, List<List<int>> matrix)
        {
            for (int i = 0; i < matrix[0].Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    if (matrix[j][i] >= 1)//障碍物
                    {
                        getLayer().SetTile(new Vector3Int(basePos.x + i, basePos.y + j, 0), Tiles[matrix[j][i]]);
                    }
                }
            }
        }
        public void Clean()
        {
            getLayer().ClearAllTiles();
        }
    }