using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMapController : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInParent<MapLoader>().drawDynamicMap();
        StartCoroutine(Drop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Drop()
    {
        while (true)
        {
            if (this.camera.GetComponent<GameMainController>().Status=="running")
            {
                this.transform.position+=Vector3.down*0.8f;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
