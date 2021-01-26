using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEditor;
using Random = UnityEngine.Random;

/**
 * testdata
 * 
 */
public class MapLoader : MonoBehaviour
{
    public Tilemap ObstructionMap;
    public Tile BaseTile;
    private int[,] map;

    private int mapWidth;

    private int mapHeight;

    // Start is called before the first frame update
    void Start()
    {
        int[] a = {1, 2, 3};
        map = new int[5, 5]
        {
            {
                0, 0, 1, 0, 0
            },
            {
                0, 1, 0, 1, 0
            },
            {
                1, 0, 1, 0, 1
            },
            {
                0, 1, 0, 1, 0
            },
            {
                0, 0, 1, 0, 0
            }
        };

        drawMatrix(0,0,5,5,map);
        drawLine(0,7,7,7);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void drawMatrix(int x, int y, int w, int h, int[,] tileMatrix)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (tileMatrix[i, j] == 1)
                {
                    this.ObstructionMap.SetTile(new Vector3Int(x + i, y + j, 0), BaseTile);
                }
            }
        }
    }

    private void drawLine(int xi, int yi, int xj, int yj)
    {
        /**
         * xi yi 起点坐标
         * xj yj 终点坐标
         */
        if (xi == xj)
        {
            //Horizontal
            for (int i = yi; i < yj; i++)
            {
                this.ObstructionMap.SetTile(new Vector3Int(xi, i, 0), BaseTile);
            }
        }
        else if (yi == yj)
        {
            //Vertical
            for (int i = xi; i < xj; i++)
            {
                this.ObstructionMap.SetTile(new Vector3Int(i, yi, 0), BaseTile);
            }
        }
    }
}