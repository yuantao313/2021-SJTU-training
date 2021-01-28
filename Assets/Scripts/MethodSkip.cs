using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MethodSkip : MonoBehaviour
{
    //public int time = 0;
    Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        myImage.sprite = Resources.Load("direction2.png", typeof(Sprite)) as Sprite;
    }
    public void OnLoginButtonClick()
    {
        
        //if (time==1)
        //{
            myImage.sprite = Resources.Load("direction2.png", typeof(Sprite)) as Sprite;
        //}
        //else if(time==2)
        //{
        //    myImage.sprite = Resources.Load("direction3.png", typeof(Sprite)) as Sprite;
        //}
        //else if(time==3)
        //{
        //    SceneManager.LoadScene(2);
        //}
        //time++;
    }
    // Update is called once per frame
}
