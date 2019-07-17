using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorController : MonoBehaviour {

    public GameObject model;
    private IUserInput pi;
    public float walkSpeed = 2.0f;

    public Transform firePos1;
    public Transform firePos2;
    public GameObject bullet;
    public Transform bullets;
    public Transform leftArm1;
    public Transform leftArm2;
    public Transform aim;

    //生命值、护甲值、能量
    public static float hp = 10.0f;
    public static float ap = 20.0f;
    public static int ep;
    public Slider hpSlider;
    public Slider apSlider;
    public Text epText;
    //游戏是否暂停
    public GameObject buildUI;
    public GameObject endUI;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;

    private AudioSource biu;

    void Awake () {
        
        pi = GetComponent<IUserInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        hp = 10.0f;
        ap = 20.0f;
        ep = 0;

        biu = transform.gameObject.GetComponent<AudioSource>();
    }
	
	void Update () {

        hp = hp > 10 ? 10 : hp;
        ap = ap > 20 ? 20 : ap;
        hp = hp < 0 ? 0 : hp;
        ap = ap < 0 ? 0 : ap;
        hpSlider.value = (hp >= 10.0f ? 1 : hp / 10.0f);
        apSlider.value = (ap >= 20.0f ? 1 : ap / 20.0f);
        epText.text = ep.ToString();

        if (hp <= 0)
        {
            EnemyController.x = 0;
            planarVec = Vector3.zero;
            endUI.SetActive(true);
        }
        else if (!buildUI.activeInHierarchy)
        {
            //发射射线，移动准星的位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                aim.position = new Vector3(hit.point.x, 0, hit.point.z);
                if (Vector3.Distance(transform.position, hit.point) < 5.0f)
                {
                    //限制准星的移动范围（暂时不限制）
                }
            }

            //先根据准星与角色的位置转动角色，再根据键盘的输入转动角色
            if (aim.position.x - transform.position.x > 0.0f)
            {
                model.transform.rotation = Quaternion.Euler(90, 0, 0);
                leftArm2.gameObject.SetActive(false);
                leftArm1.gameObject.SetActive(true);
                leftArm1.LookAt(aim);
            }
            else if (aim.position.x - transform.position.x < 0.0f)
            {
                model.transform.rotation = Quaternion.Euler(-90, 0, -180);
                leftArm1.gameObject.SetActive(false);
                leftArm2.gameObject.SetActive(true);
                leftArm2.LookAt(aim);
            }

            if (pi.Dmag > 0.0f)
            {
                anim.SetFloat("forward", pi.Dmag);  // 1walk、0idle
            }
            //计算角色的移动值
            planarVec = pi.Dright * transform.right * walkSpeed + pi.Dup * transform.forward * walkSpeed;

            if (pi.attack)
            {
                biu.Play();
                //anim.SetTrigger("attack");
                if (firePos1.gameObject.activeInHierarchy)
                {
                    Instantiate(bullet, firePos1.position, firePos1.rotation, bullets);
                }
                else if (firePos2.gameObject.activeInHierarchy)
                {
                    Instantiate(bullet, firePos2.position, firePos2.rotation, bullets);
                }
            }
        }
        else
        {
            planarVec = Vector3.zero;
        }
    }

    void FixedUpdate() {

        //model.transform.position += planarVec * Time.fixedDeltaTime;

        rigid.velocity = new Vector3(planarVec.x, planarVec.y, planarVec.z);
    }

    public void ChangeHP(int damage)
    {
        if (ap > 0)
        {
            ap -= damage;
        }
        else
        {
            hp -= damage;
        }
    }
}
