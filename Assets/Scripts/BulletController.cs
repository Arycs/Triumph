using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour {

    //子弹的种类
    public enum BulletType { enemy,player,laser }
    public float speed = 1.0f;

    public int damage = 1;
    public static int playerPlus = 0;
    public int enemyPlus = 0;
    public float animTime = 0.0f;

    public BulletType bulletType = BulletType.player;

    public bool attcked = true;

    void Update () {

        if (bulletType != BulletType.laser)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 8 * speed;
        }
        
        //print(damagePlus);
    }

    void OnTriggerEnter(Collider coll)
    {
        //玩家以及炮塔发射的子弹
        if (bulletType==BulletType.player)
        {
            if (coll.name != "Plane" && coll.tag != "Player" && coll.tag != "Tower" && coll.tag == "Obstacle") 
            {
                DestroyBullet(animTime);
                if (coll.gameObject.GetComponent<TowerController>() != null)
                {
                }
            }
            if (coll.tag == "Enemy")
            {
                if (gameObject.name != "Explosion(Clone)")
                {
                    gameObject.transform.parent = coll.transform;
                }

                coll.GetComponent<EnemyController>().ChangeHp(damage + playerPlus);

                DestroyBullet(animTime);
            }
        }

        //敌人发射的子弹
        if (bulletType == BulletType.enemy)
        {
            if (coll.name != "Plane" && coll.tag != "Enemy" && coll.tag != "Tower" && coll.tag != "EnemyBullet" && coll.tag == "Obstacle")
            {
                DestroyBullet(animTime);
                if (coll.gameObject.GetComponent<TowerController>() != null)
                {
                    print("2");
                }
            }
            if (coll.tag == "Player")
            {
                gameObject.transform.parent = coll.transform;

                if (attcked)
                {
                    coll.GetComponent<ActorController>().ChangeHP(damage + enemyPlus);
                    attcked = false;
                }

                DestroyBullet(animTime);
            }
        }

        if (bulletType == BulletType.laser)
        {
            if (coll.name != "Plane" && coll.tag != "Enemy" && coll.tag != "Tower" && coll.tag != "EnemyBullet" && coll.tag == "Obstacle")
            {
                print("2");
            }
            if (coll.tag == "Player")
            {
                coll.GetComponent<ActorController>().ChangeHP(damage + enemyPlus);
            }
        }
    }

    void DestroyBullet(float time)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponentInChildren<Animator>().SetTrigger("explosion");
        Destroy(gameObject, time);
    }
}
