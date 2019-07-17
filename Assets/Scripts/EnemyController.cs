using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public enum EnemyType { BotX, BotY, BotZ, Boss1, Boss2 }
    public EnemyType enemyType = EnemyType.BotX;

    public int hp = 3;

    private Animator anim;

    //不同敌人攻击间隔
    public float botXAttackTimer = 2.0f;
    public float botZAttackTimer = 0.5f;
    private float timer = 0.0f;

    //发射位置
    public Transform firePos;
    //子弹
    public GameObject bullet;
    public int enemyDamage;
    //激光
    private LineRenderer laser;
    public Transform bullets;

    //是否被减速
    public bool isSlow = false;
    //游戏是否暂停
    public static int x = 1;

    //自动寻路
    private NavMeshAgent agent;
    private GameObject player;

    //打败不同敌人获得能量
    public Text energyText;
    public int energyPoint = 0;
    private bool die = false;

    //发射弹幕的旗标
    private bool shotgunFire = true;
    private bool roundFire = true;

    public float dieTime;

    void Awake () {

        anim = GetComponentInChildren<Animator>();

        laser = firePos.GetComponent<LineRenderer>();

        bullets = GameObject.Find("EnemyBullets").transform;

        //自动寻路
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerHandler");

        energyText = GameObject.Find("EnergyPoint").GetComponentInChildren<Text>();

        x = 1;
	}
	
	void Update () {

        //根据和主角的位置旋转自身
        if (player.transform.position.x - transform.position.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //自动寻路时，与角色的位置小于某个值时停止寻路
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 2.5f)
        {
            agent.SetDestination(transform.position);
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }

        agent.speed = isSlow ? 2.5f * x : 5.0f * x;

        //死亡
        if (hp <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (x == 1)
        {
            switch (enemyType)
            {
                case EnemyType.BotX:
                    Fire(botXAttackTimer);
                    break;
                case EnemyType.BotY:
                    if (Vector3.Distance(transform.position, player.transform.position) <= 2.6f)
                    {
                        if (timer == 0.0f)
                        {
                            //对主角进行近战攻击
                            player.GetComponent<ActorController>().ChangeHP(1);
                        }
                        timer += Time.fixedDeltaTime;
                        if (timer >= 2.0f)
                        {
                            timer = 0.0f;
                        }
                    }
                    break;
                case EnemyType.BotZ:
                    Fire(botZAttackTimer);
                    break;
                case EnemyType.Boss1:
                    if (hp <= 750)
                    {
                        //生命值小于一半时，进行更强力的攻击--圆形弹幕
                        if (roundFire)
                        {
                            StartCoroutine(FireRound());
                        }
                    }
                    else
                    {
                        //散射弹幕
                        if (shotgunFire)
                        {
                            StartCoroutine(FireShotgun());
                        }
                    }
                    break;
                case EnemyType.Boss2:
                    if (hp <= 1000)
                    {
                        //生命值小于一半时，进行更强力的攻击--射线扫射
                        LaserRound();
                    }
                    else
                    {
                        //普通攻击
                        Fire(0.25f);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //普通攻击
    public void Fire(float coldTime)
    {
        if (timer == 0.0f)
        {
            //发射子弹，攻击主角
            firePos.LookAt(player.transform);
            Instantiate(bullet, firePos.position, firePos.rotation, bullets).GetComponent<BulletController>().enemyPlus = enemyDamage;
        }
        timer += Time.fixedDeltaTime;
        if (timer >= coldTime)
        {
            timer = 0.0f;
        }
    }

    //散弹弹幕
    IEnumerator FireShotgun()
    {
        shotgunFire = false;
        //三个发射的位置
        Quaternion leftRota = Quaternion.AngleAxis(-60, Vector3.up);
        Quaternion midRota = Quaternion.AngleAxis(-90, Vector3.up);
        Quaternion rightRota = Quaternion.AngleAxis(-120, Vector3.up);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)//一次发射3颗子弹
            {
                switch (j)
                {
                    case 0:
                        //根据敌人朝向改变子弹的朝向
                        midRota = Quaternion.AngleAxis(transform.rotation.z == 1 ? 90 : -90, Vector3.up);
                        firePos.rotation = midRota;
                        Instantiate(bullet, firePos.position, firePos.rotation, bullets);
                        break;
                    case 1:
                        leftRota = Quaternion.AngleAxis(transform.rotation.z == 1 ? 60 : -60, Vector3.up);
                        firePos.rotation = leftRota;
                        Instantiate(bullet, firePos.position, firePos.rotation, bullets);
                        break;
                    case 2:
                        rightRota = Quaternion.AngleAxis(transform.rotation.z == 1 ? 120 : -120, Vector3.up);
                        firePos.rotation = rightRota;
                        Instantiate(bullet, firePos.position, firePos.rotation, bullets);
                        break;
                    default:
                        break;
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
        shotgunFire = true;
    }

    //圆形弹幕
    IEnumerator FireRound()
    {
        roundFire = false;
        //Vector3 bulletDir = firePos.transform.forward;
        Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.up);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 18; j++)//发射18颗子弹
            {
                Instantiate(bullet, firePos.position, firePos.rotation, bullets);
                firePos.rotation *= rotateQuate;
            }
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
        roundFire = true;
    }

    //Boss2射线扫射
    public void LaserRound()
    {
        laser.GetComponent<BoxCollider>().enabled = true;
        laser.enabled = true;
        firePos.Rotate(0, 5f, 0);
    }

    public void ChangeHp(int x)
    {
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            hp -= x;
        }
    }

    void Die()
    {
        if (!die)
        {
            die = true;
            ActorController.ep += energyPoint;
        }
        agent.speed = 0;
        anim.SetBool("die", true);
        Destroy(gameObject, dieTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            //对主角造成激光伤害
        }
    }
}
