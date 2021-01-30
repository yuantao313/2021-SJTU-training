using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HealthBarController : MonoBehaviour
{
    public GameObject player;

    private Vector3Int[] order;

    public Tilemap healthBar;

    public Tile heart;

    public Tile halfHeart;

    // Start is called before the first frame update
    void Start()
    {
        this.order = new Vector3Int[]
        {
            new Vector3Int(0, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, 2, 0),
            new Vector3Int(0, 3, 0),
            new Vector3Int(0, 4, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(1, 2, 0),
            new Vector3Int(1, 3, 0),
            new Vector3Int(1, 4, 0)
        };
    }

    // Update is called once per frame
    void Update()
    {
        this.healthBar.ClearAllTiles();
        int health = player.GetComponent<PlayerController>().Health;
        int index = 0;
        for (; index < health / 2; index++)
        {
            this.healthBar.SetTile(this.order[index],heart);
        }
        if (health % 2 == 1)
        {
            this.healthBar.SetTile(this.order[index],halfHeart);
        }
    }
}
