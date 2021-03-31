using System.Collections.Generic;
using UnityEngine;
public class TeleportationController : MonoBehaviour
{
    private List<Teleportation> points;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector2Int teleport(Vector2Int playerPos)
    {
        foreach (var t in points)
        {
            if (t.startPos.x == playerPos.x && t.startPos.y == playerPos.y)
            {
                points.Remove(t);
                return t.endPos;
            }
        }
        return playerPos;
    }

}

