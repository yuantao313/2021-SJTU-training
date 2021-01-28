using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private const float moveLength = 0.8f; //每个格子80px

    private const float restTime = 1f;
    private const float bpm = 60;
    private float restTimer;
    private float timer;
    private int health;
    private int maxHealth=20; 
    public Text performance;
    public Text pointText;
    
    private int point;
    
    private Vector2 lastPos;
    private Vector2 position;
    private AudioSource Sound;
    public AudioClip moveSound;
    public AudioClip hurtSound;

    public GameObject camera;
    public GameObject rhythmBarArrow;
    public int Health => health;
    private bool isProtected;
    private bool damageLock;
    public bool IsProtected
    {
        get => isProtected;
        set => isProtected = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        restTimer = 0;
        this.isProtected = false;
        this.health = this.maxHealth;
        this.point = 0;
        this.pointText.text = "Point:0";
        this.lastPos = new Vector2(4, 0);
        this.position = new Vector2(4, 0);
        this.Sound = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.camera.GetComponent<GameMainController>().Status=="running")
        {
            //通过检测
            restTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.W))
            {
                this.position.y += 1;
                this.rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if(this.position.y>0){
                    this.position.y -= 1;
                    this.rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(2);
                }
                else
                {
                    this.hurt(1);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.A) )
            {
                if(this.position.x>0){
                    this.position.x -= 1;
                    this.rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(1);
                    
                }
                else
                {
                    this.hurt(1);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.D) ) {
                if(this.position.x<8){
                    this.position.x += 1;
                    this.rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(-1);
                }
                else
                {
                    this.hurt(1);
                }
            }

            if (this.position != this.lastPos)
            {
                //this.transform.position=Vector2.Lerp()
                transform.Translate((this.position - this.lastPos) * moveLength);
                float offset = Math.Abs(restTime - restTimer);
                if (offset < 0.1 * restTime && offset >= 0)
                {
                    this.Sound.clip = moveSound;
                    this.Sound.Play();
                    this.cure(1);
                    this.performance.text = "Perfect";
                    this.point += 5;
                }
                else if (offset < 0.2 * restTime && offset >= 0.1 * restTime)
                {
                    this.Sound.clip = moveSound;
                    this.Sound.Play();
                    this.performance.text = "Good";
                    this.point += 3;
                }
                else
                {
                    this.Sound.clip = hurtSound;
                    this.Sound.Play();
                    this.hurt(1);
                    this.performance.text = "Bad";
                    this.point += 0;
                }

                this.pointText.text = "Point:" + this.point.ToString();

                restTimer = 0;
            }

            timer += Time.deltaTime;
            
            
            
            if (this.health <= 0)
            {
                this.camera.GetComponent<GameMainController>().Status = "stop";
                StopAllCoroutines();
                SceneManager.LoadScene(3);
            }

            if (this.position.y >= 30)
            {
                SceneManager.LoadScene(4);
            }
            this.lastPos = this.position;
        }
    }

    public void cure(int recovery)
    {
        if (this.health + recovery > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
        else if(this.health + recovery <0)
        {
            this.health = 0;
        }
        else
        { 
            this.health += recovery;
        }
    }

    private void hurt(int damage)
    {
        if (!this.isProtected)
        {
            this.cure((-1) * damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        //当人物触碰障碍时
        if (c.CompareTag("Obstruction"))
        {//障碍
            if(!this.damageLock){
                this.damageLock = true;
                this.hurt(1);
            }
            
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

    private void OnTriggerExit(Collider c)
    {
        this.damageLock = false;
    }
/*
    public IEnumerator hit()
    {
        Vector2 lastpos = this.position;
        while (true)
        {
            if()
        }
    }*/
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