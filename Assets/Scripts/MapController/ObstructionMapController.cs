using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionMapController : MonoBehaviour
{
    public int[,] matrix;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = player.GetComponent<MainController>().position;
        if (this.matrix[player.position.x, player.position.y] == 1)
        {//玩家碰到障碍，出现伤害
            player.cure(-1);
        }
    }
}
