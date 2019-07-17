using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour {

    //塔的种类
    public enum TowerType { ordinary, powerful, gravity, range, suck };

    //默认种类
    public TowerType towerType = TowerType.ordinary;

    //敌人集合
    public List<GameObject> enemys = new List<GameObject>();

    //武器
    public GameObject bullet;
    public GameObject explosion;
    public LineRenderer laser;
    public Transform firePos;

    //攻击冷却
    public float attackColdTimer = 0.5f;
    private float timer = 0.0f;

    //主角是否在塔内
    private bool playerInTower = false;
    //是否发动了磁力引爆技能
    private bool magnetic = false;
    //是否发动了能量超频技能
    private bool overclock = false;

    //生命值最低的敌人的生命值
    private int hpMin;

    //技能
    public GameObject Skill;

    public GameObject player;
    private AudioSource biu;

    void Awake() {
        Skill = GameObject.Find("Skill");
        player = GameObject.Find("PlayerHandler");
        biu = GetComponent<AudioSource>();
        //firePos = transform.GetChild(0);
    }
	
	void Update () {

        if (enemys.Count != 0)
        {
            switch (towerType)
            {
                //机枪碉堡
                case TowerType.ordinary:
                    if (timer == 0.0f)
                    {
                        //炮塔发射子弹，攻击敌人
                        for (int x = 0; x < enemys.Count; x++)
                        {
                            //炮塔攻击生命值最低的敌人
                            hpMin = enemys[x].GetComponent<EnemyController>().hp;
                            for (int y = 0; y < enemys.Count; y++)
                            {
                                if (enemys[y].GetComponent<EnemyController>().hp < hpMin)
                                {
                                    hpMin = enemys[y].GetComponent<EnemyController>().hp;
                                    firePos.LookAt(enemys[y].transform);
                                }
                                else
                                {
                                    firePos.LookAt(enemys[x].transform);
                                }
                            }
                        }
                        biu.Play();
                        Instantiate(bullet, firePos.position, firePos.rotation, transform);
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer >= attackColdTimer)
                    {
                        timer = 0.0f;
                    }
                    break;
                //磁力场
                case TowerType.gravity:
                    //减速范围里的敌人
                    for (int x = 0; x < enemys.Count; x++)
                    {
                        enemys[x].GetComponent<EnemyController>().isSlow = true;
                    }

                    if (!magnetic)
                    {
                        if (timer == 0.0f)
                        {
                            for (int x = 0; x < enemys.Count; x++)
                            {
                                enemys[x].GetComponent<EnemyController>().ChangeHp(1);
                            }
                        }
                        timer += Time.fixedDeltaTime;
                        if (timer >= attackColdTimer)
                        {
                            timer = 0.0f;
                        }
                    }
                    break;
                //电磁炮台
                case TowerType.range:
                    if (timer == 0.0f)
                    {
                        biu.Play();
                        Instantiate(explosion, enemys[0].transform.position, explosion.transform.rotation, transform);
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer >= attackColdTimer)
                    {
                        timer = 0.0f;
                    }
                    break;
                //能量吸收器
                case TowerType.suck:
                    break;
                default:
                    break;
            }
        }

        //主角在塔的范围内
        if (playerInTower)
        {
            switch (towerType)
            {
                //同调技能开启
                case TowerType.ordinary:
                    //主角移动速度上升
                    GameObject.Find("PlayerHandler").GetComponent<ActorController>().walkSpeed = 15;
                    //机枪塔攻速上升
                    attackColdTimer = 0.5f;
                    break;
                //能量增幅器，伤害加成10点基础伤害
                case TowerType.powerful:
                    laser.enabled = true;
                    laser.SetPositions(new Vector3[] { firePos.position, GameObject.Find("PlayerHandler").transform.position });
                    if (!overclock)
                    {
                        BulletController.playerPlus = 10;
                    }
                    break;
                default:
                    break;
            }
        }
        else  //主角不在塔的范围内
        {
            switch (towerType)
            {
                //同调技能关闭
                case TowerType.ordinary:
                    //主角移动速度正常
                    GameObject.Find("PlayerHandler").GetComponent<ActorController>().walkSpeed = 10;
                    //机枪塔攻速正常
                    attackColdTimer = 1;
                    break;
                //伤害加成0点基础伤害
                case TowerType.powerful:
                    laser.enabled = false;
                    BulletController.playerPlus = 0;
                    break;
                default:
                    break;
            }
        }

        //将集合里生命值为零的敌人移出集合
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].GetComponent<EnemyController>().hp <= 0)
            {
                enemys.Remove(enemys[i]);
            }
        }

        //磁力引爆
        if (magnetic)
        {
            timer += Time.deltaTime;
            if (timer >= 9.9f)
            {
                magnetic = false;
            }
        }

        //能量超频
        if (overclock)
        {
            BulletController.playerPlus = 20;
            timer += Time.deltaTime;
            if (timer >= 9.9f)
            {
                BulletController.playerPlus = 0;
                overclock = false;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        //敌人进入攻击范围，将敌人添加进集合
        if (coll.tag == "Enemy")
        {
            enemys.Add(coll.gameObject);
        }

        if (coll.tag == "Player")
        {
            playerInTower = true;
        }

        if (towerType == TowerType.suck)
        {
            int x = BuildTowerUI.EnergyShieldSkill;
            if (coll.tag == "EnemyBullet")
            {
                coll.enabled = false;
                Destroy(coll.gameObject);
                Skill.transform.GetChild(x).GetChild(1).GetComponent<Image>().fillAmount -= 0.05f;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        //敌人离开攻击范围，将敌人移出集合
        if (coll.tag == "Enemy")
        {
            enemys.Remove(coll.gameObject);

            if (towerType == TowerType.gravity)
            {
                coll.GetComponent<EnemyController>().isSlow = false;
            }
        }

        if (coll.tag == "Player")
        {
            playerInTower = false;
        }
    }

    //磁力引爆 技能
    public void MagneticExplosion()
    {
        if (towerType == TowerType.gravity)
        {
            print("发动磁力引爆");
            for (int x = 0; x < enemys.Count; x++)
            {
                biu.Play();
                enemys[x].GetComponent<EnemyController>().ChangeHp(20);
            }
            magnetic = true;
            timer = 0;
        }
    }

    //能量超频 技能
    public void EnergyOverclock()
    {
        if (towerType == TowerType.powerful)
        {
            print("发动能量超频");
            overclock = true;
            timer = 0;
        }
    }

    //火力轰炸 技能
    public void FireBomb()
    {
        print("发动火力轰炸");
        biu.Play();
        Instantiate(explosion, player.transform.position, explosion.transform.rotation, player.transform.GetChild(3).transform);
    }

    //能量护盾 技能
    public void EnergyShield()
    {
        print("发动能量护盾");
    }
}
