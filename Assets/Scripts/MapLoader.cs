using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
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
    
    // Start is called before the first frame update
    void Start()
    {
        int[] a ={1,2,3};
        map = new int[5, 5]{
            { 0,0,1,0,0
            },{ 0,1,0,1,0
            },{ 1,0,1,0,1
            },{ 0,1,0,1,0 
            },{ 0,0,1,0,0
            }}; 
        
        for (int i = 0; i < 5; i++)
        {//这里就是设置每个Tile的信息了
            for (int j = 0; j < 5; j++)
            {
                if(map[i,j]==1){
                ObstructionMap.SetTile(new Vector3Int(i, j, 0), BaseTile);
            }}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class a
{
    private int x;
    private int y;
    private int[,,] frames;
    

}
