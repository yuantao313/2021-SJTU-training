using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject player;

    private Vector2 playerLastPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 verticalOffset = new Vector2(0, playerPosition.y-playerLastPosition.y);
        this.transform.position += (Vector3)verticalOffset;
        this.playerLastPosition = playerPosition;
    }
}
