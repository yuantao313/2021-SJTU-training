using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {//使用回复道具
            player.GetComponent<MainController>().cure(5);
        }else if (Input.GetKeyDown(KeyCode.X))
        {//使用时间停止道具
            
        }
    }
}
