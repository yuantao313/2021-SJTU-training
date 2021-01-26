using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private Vector2 startPoint;

    private Vector2 endPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public Vector2 StartPoint
    {
        get => startPoint;
        set => startPoint = value;
    }

    public Vector2 EndPoint
    {
        get => endPoint;
        set => endPoint = value;
    }
}
