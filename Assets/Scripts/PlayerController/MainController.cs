using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour
{
    private const float moveLength = 0.8f; //每个格子80px

    private const float restTime = 1f;
    private const float bpm = 60;
    private float restTimer;
    private float timer;
    private int health;
    public Slider healthBar;
    private int maxHealth=100; 
    public Text performance;
    public Text pointText;
    
    private int point;

    public GameObject UpCollider;
    public GameObject DownCollider;
    public GameObject LeftCollider;
    public GameObject RightCollider;
    private Vector2 lastPos;
    private Vector2 position;

    private AudioSource moveSound;
    private AudioSource hurtSound;

    public GameObject camera;
    
    // Start is called before the first frame update
    void Start()
    {
        restTimer = 0;
        this.health = this.maxHealth;
        this.point = 0;
        this.pointText.text = "Point:0";
        this.lastPos = new Vector2(4, 0);
        this.position = new Vector2(4, 0);
        this.moveSound = GetComponent<AudioSource>();
        this.healthBar.maxValue = (float)this.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.camera.GetComponent<GameMainController>().Running)
        {
            //通过检测
            restTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.W))
            {
                this.position.y += 1;
            }
            else if (Input.GetKeyDown(KeyCode.S)&& this.position.y>0)
            {
                this.position.y -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.A) &&this.position.x>0)
            {
                this.position.x -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.D) &&this.position.x<8)
            {
                this.position.x += 1;
            }

            if (this.position != this.lastPos)
            {
                //this.transform.position=Vector2.Lerp()
                transform.Translate((this.position - this.lastPos) * moveLength);
                float offset = Math.Abs(restTime - restTimer);
                if (offset < 0.1 * restTime && offset >= 0)
                {
                    this.cure(1);
                    this.performance.text = "Perfect";
                    this.point += 5;
                }
                else if (offset < 0.2 * restTime && offset >= 0.1 * restTime)
                {
                    this.performance.text = "Good";
                    this.point += 3;
                }
                else
                {
                    this.cure(-1);
                    this.performance.text = "Bad";
                    this.point += 0;
                }

                this.pointText.text = "Point:" + this.point.ToString();

                restTimer = 0;
                this.moveSound.Play();
            }

            timer += Time.deltaTime;
            /*
            if (timer >= 1f)
            {
                timer = 0;
                this.position.y += 1;
                transform.Translate(Vector2.up*moveLength);
            }*/
            
            if (this.health <= 0)
            {
                this.camera.GetComponent<GameMainController>().Running = false;
            }

            this.healthBar.value = this.health;
            this.lastPos = this.position;
        }
        else
        {
            //游戏结束
        }
    }

    public void cure(int recovery)
    {
        if (this.health + recovery > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
        else
        {
            this.health += recovery;
        }
    }

    private void hurt(int damage)
    {
        this.cure((-1) * damage);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        //当人物触碰障碍时
        if (c.CompareTag("Obstruction"))
        {//障碍
            this.hurt(1);
        }else if (c.CompareTag("Recovery"))
        {//回复
            c.gameObject.SetActive(false);
            this.health += 2;
        }else if (c.CompareTag("Teleportation"))
        {//传送
            this.position=c.GetComponent<Teleportation>().EndPoint;
        }else if (c.CompareTag("Slider"))
        {//滑块
            
        }
    }
}
/**
class pos
{
    public int x;
    public int y;

    public pos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static bool operator==(pos a,pos b)
    {
        return (a.x == b.x && a.y == b.y);
    }

    public static bool operator !=(pos a, pos b)
    {
        return !(a == b);
    }
}*/