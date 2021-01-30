using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private const float moveLength = 0.8f; //每个格子80px

    private float restTime;
    private float restTimer;
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
    public bool IsProtected
    {
        get => isProtected;
        set => isProtected = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        restTimer = 0;
        restTime = 60/camera.GetComponent<GameMainController>().Bpm;
        health = maxHealth;
        lastPos = new Vector2(4, 0);
        position = new Vector2(4, 0);
       Sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.GetComponent<GameMainController>().Status!="stop")
        {
            //通过检测
            restTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.W))
            {
                position.y += 1;
                rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if(position.y>0){
                    position.y -= 1;
                    rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(2);
                }
                else
                {
                    hurt(1);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.A) )
            {
                if(position.x>0){
                    position.x -= 1;
                    rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(1);
                    
                }
                else
                {
                    this.hurt(1);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.D) ) {
                if(position.x<8){
                    position.x += 1;
                    rhythmBarArrow.GetComponent<RhythmBarArrowController>().rotate(-1);
                }
                else
                {
                    hurt(1);
                }
            }

            if (position != lastPos)
            {
                transform.Translate((position - lastPos) * moveLength);
                float offset = Math.Abs(restTime - restTimer);
                if (offset < 0.1 * restTime && offset >= 0)
                {
                   Sound.clip = moveSound;
                   Sound.Play();
                    cure(1);
                    performance.text = "Perfect";
                }
                else if (offset < 0.2 * restTime && offset >= 0.1 * restTime)
                {
                    Sound.clip = moveSound;
                    Sound.Play();
                    performance.text = "Good";
                }
                else
                {
                    Sound.clip = hurtSound;
                    Sound.Play();
                    hurt(1);
                    performance.text = "Bad";
                }
                restTimer = 0;
            }

            if (health <= 0)
            {
                camera.GetComponent<GameMainController>().Status = "stop";
                StopAllCoroutines();
                SceneManager.LoadScene(3);
            }

            if (position.y >= 17)
            {
                camera.GetComponent<GameMainController>().Status = "stop";
                StopAllCoroutines();
                SceneManager.LoadScene(4);
            }
            lastPos = position;
        }
    }

    public void cure(int recovery)
    {
        if (health + recovery > maxHealth)//不能治疗超过最大生命值
        {
            health = maxHealth;
        }
        else if(health + recovery <0)
        {
            health = 0;
        }
        else
        { 
            health += recovery;
        }
    }

    private void hurt(int damage)
    {
        if (!isProtected)
        {
            cure((-1) * damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        //当人物触碰障碍时
        if (c.CompareTag("Obstruction"))
        {//障碍
            
                hurt(1);
            
            
        }else if (c.CompareTag("Recovery"))
        {//回复
            c.gameObject.SetActive(false);
            health += 2;
        }else if (c.CompareTag("Teleportation"))
        {//传送
            position=c.GetComponent<Teleportation>().EndPoint;
        }else if (c.CompareTag("Slider"))
        {//滑块
            
        }
    }

  

}