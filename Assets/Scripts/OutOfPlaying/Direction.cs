using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Direction : MonoBehaviour
{
    public Image myImage;
    private int num;
    private Sprite[] dirs;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        dirs = Resources.LoadAll<Sprite>("images/direction");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Next()
    {
        num++;
        if (num < 3)
        {
            myImage.overrideSprite = dirs[num];
        }
        else
        {
            Skip();
        }
    }
    public void Skip()
    {
        SceneManager.LoadScene(2);
    }
}
