using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInParent<MapLoader>().drawDynamicMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Drop()
    {
        this.transform.position += Vector3.down * 0.8f;
    }

}
