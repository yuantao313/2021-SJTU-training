using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Property : MonoBehaviour
{
    public GameObject player;
    private int protectNumber;
    private int timeStopNumber;

    private String[] translation;

    public Text qText;

    public Text eText;
    // Start is called before the first frame update
    void Start()
    {
        this.protectNumber = 5;
        this.timeStopNumber = 5;
        this.translation = new String[]
        {
            "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖"
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))//使用无敌道具
        {
         this.protectNumber -= 1;
         StartCoroutine(protect());
            
        }else if (Input.GetKeyDown(KeyCode.Q))//使用时间停止道具
        {
            this.timeStopNumber -= 1;
            StartCoroutine(timeStop());
        }
        this.eText.text = this.translation[this.protectNumber];
        this.qText.text = this.translation[this.timeStopNumber];
    }

    public IEnumerator protect()
    {
        this.player.GetComponent<PlayerController>().IsProtected = true;
        yield return new WaitForSeconds(5);
        this.player.GetComponent<PlayerController>().IsProtected=false;
    }
    public IEnumerator timeStop()
    {
        this.GetComponent<GameMainController>().Status = "timestop";
        yield return new WaitForSeconds(5);
        this.GetComponent<GameMainController>().Status="running";
    }//这样会造成死前5s内使用时停道具，玩家送死，游戏状态被设为stop后，预定时间到达，游戏状态重新被设为running
    //游戏终止时中止所有协程，已修复
}
