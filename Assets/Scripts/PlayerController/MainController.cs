using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private const float moveLength = 0.8f; //每个格子80px

    private const float restTime = 1f;
    private const float bpm = 60;
    private float restTimer;
    
    private int health;

    public Text performance;
    public Text pointText;
    
    private int point;

    public GameObject UpCollider;
    public GameObject DownCollider;
    public GameObject LeftCollider;
    public GameObject RightCollider;
    // Start is called before the first frame update
    void Start()
    {
        restTimer = 0;
        health = 20;
        this.point = 0;
        this.pointText.text = "Point:0";
    }

    // Update is called once per frame
    void Update()
    {
        //通过检测
        restTimer += Time.deltaTime;

        Vector2 movement = new Vector2(0, 0);

        if (Input.GetKeyDown(KeyCode.W)&&this.UpCollider.GetComponent<ColliderController>().getStatus())
        {
            movement = moveLength * Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S)&&this.DownCollider.GetComponent<ColliderController>().getStatus())
        {
            movement = moveLength * Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A)&&this.LeftCollider.GetComponent<ColliderController>().getStatus())
        {
            movement = moveLength * Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D)&&this.RightCollider.GetComponent<ColliderController>().getStatus())
        {
            movement = moveLength * Vector2.right;
        }

        if (movement != new Vector2(0, 0))
        {
            transform.Translate(movement);

            float offset = Math.Abs(restTime - restTimer);
            if (offset < 0.1 * restTime && offset >= 0)
            {
                this.health += 1;
                this.performance.text = "Petfect";
                this.point += 5;
            }
            else if (offset < 0.2 * restTime && offset >= 0.1 * restTime)
            {
                this.performance.text = "Good";
                this.point += 3;
            }
            else
            {
                this.health -= 1;
                this.performance.text = "Bad";
                this.point += 0;
            }
            this.pointText.text="Point:"+this.point.ToString();

            restTimer = 0;
        }
    }

 

    private void OnTriggerEnter(Collider c)
    {
        //当人物触碰障碍时
        if (c.CompareTag("Obstruction"))
        {//障碍
            
        }else if (c.CompareTag("Recovery"))
        {//回复
            c.gameObject.SetActive(false);
            this.health += 2;
        }else if (c.CompareTag("Teleportation"))
        {//传送
            
        }else if (c.CompareTag("Slider"))
        {//滑块
            
        }
    }
}
