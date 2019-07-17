using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTowerUI : MonoBehaviour {

    //存储塔和对应信息的字典
    public Dictionary<int, string> TowerDic = new Dictionary<int, string>();

    //显示塔的详细信息的文本
    public Text TowerInformation;

    //对BuildTower的调用
    public BuildTower Bt1;
    public BuildTower Bt2;
    public BuildTower Bt3;
    public BuildTower Bt4;

    //对技能的调用
    public SkillUI Su;

    //防御塔的预制体
    public GameObject TowerA;
    public GameObject TowerB;
    public GameObject TowerC;
    public GameObject TowerD;
    public GameObject TowerE;
    public int towerIndex;
    private Transform towerGround;

    //技能槽
    public GameObject[] Skills;
    public int index = 0;
    public Sprite SkillA;
    public Sprite SkillB;
    public Sprite SkillC;
    public Sprite SkillD;
    public Sprite SkillE;
    public static int EnergyShieldSkill = -1;

    public Image towerImg;
    public Text cost;
    private bool notBuild = false;

    void Start () {

        TowerDic.Add(1, "机枪碉堡 (10能量) \n\n攻击范围：圆形，较远，攻击最近的敌人 \n伤害：10点基础伤害，无法穿透 \n攻速：1发/1秒 \n\n技能 \n同调：被动，当主角处在机枪塔攻击范围内，你的移动速度上升，机枪塔攻速上升。");
        TowerDic.Add(2, "磁力场 (20能量) \n\n攻击范围：圆形，中等，减速，范围内所有敌人， \n伤害：1点基础伤害 \n攻速：1秒 \n\n技能 \n磁力引爆：引爆磁力线圈，对磁力塔攻击范围内敌人造成20点伤害。磁力塔停止工作10秒。");
        TowerDic.Add(3, "能量增幅器 (20能量) \n\n攻击范围：圆形，较远，主角 \n作用：主角伤害加成10点 \n\n技能 \n能量超频：将主角伤害加成提高到20点，持续时间10秒。");
        TowerDic.Add(4, "电磁炮台 (30能量) \n\n攻击范围：圆形，远，范围伤害 \n伤害：10点 \n攻速：2秒 \n\n技能 \n火力轰炸：呼叫电磁炮台进行以自己为中心的精准轰炸。对大范围内敌人造成20点伤害。");
        TowerDic.Add(5, "能量吸收器 (30能量) \n\n攻击范围：圆形，中等 \n作用：将范围内能量子弹吸收 \n\n技能 \n能量护盾：将吸收的能量转换成护盾。根据已吸收的能量数值判定护盾大小。");
    }

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            EnemyController.x = 0;
        }
    }

    public void BuildTowerA(Sprite img)
    {
        TowerInformation.text = TowerDic[1];
        towerImg.sprite = img;
        cost.text = "10能量";
        towerIndex = 0;
    }
    public void BuildTowerB(Sprite img)
    {
        TowerInformation.text = TowerDic[2];
        towerImg.sprite = img;
        cost.text = "20能量";
        towerIndex = 1;
    }
    public void BuildTowerC(Sprite img)
    {
        TowerInformation.text = TowerDic[3];
        towerImg.sprite = img;
        cost.text = "20能量";
        towerIndex = 2;
    }
    public void BuildTowerD(Sprite img)
    {
        TowerInformation.text = TowerDic[4];
        towerImg.sprite = img;
        cost.text = "30能量";
        towerIndex = 3;
    }
    public void BuildTowerE(Sprite img)
    {
        TowerInformation.text = TowerDic[5];
        towerImg.sprite = img;
        cost.text = "30能量";
        towerIndex = 4;
    }

    public void Build() {

        EnemyController.x = 1;

        if (towerIndex == 0)
        {
            //根据塔基的位置生成塔
            if (ActorController.ep >= 10)
            {
                Instantiate(TowerA, towerGround);
                notBuild = true;
                towerIndex = -1;
                gameObject.SetActive(false);
                GetComponentsInChildren<Button>()[0].interactable = false;
                SkillUI.SkillDic.Add("同调", "同调：被动，当你处在机枪塔攻击范围内，你的移动速度上升。机枪塔攻速上升。");
                Skills[index].GetComponent<Image>().sprite = SkillA;
                Skills[index].GetComponentInChildren<Text>().text = "同调";
                ActorController.ep -= 10;
                index++;
            }
            else {
                TowerInformation.text = "能量不足，请收集能量";
                notBuild = false;
            }
        }
        else if (towerIndex == 1)
        {
            if (ActorController.ep >= 20)
            {
                Instantiate(TowerB, towerGround);
                notBuild = true;
                towerIndex = -1;
                gameObject.SetActive(false);
                GetComponentsInChildren<Button>()[1].interactable = false;
                SkillUI.SkillDic.Add("磁力引爆", "磁力引爆：引爆磁力线圈，对磁力塔攻击范围内敌人造成20点伤害。磁力塔停止工作10s。");
                Skills[index].GetComponent<Image>().sprite = SkillB;
                Skills[index].GetComponentInChildren<Text>().text = "磁力引爆";
                ActorController.ep -= 20;
                index++;
            }
            else
            {
                TowerInformation.text = "能量不足，请收集能量";
                notBuild = false;
            }
        }
        else if (towerIndex == 2)
        {
            if (ActorController.ep >= 20)
            {
                Instantiate(TowerC, towerGround);
                notBuild = true;
                towerIndex = -1;
                gameObject.SetActive(false);
                GetComponentsInChildren<Button>()[2].interactable = false;
                SkillUI.SkillDic.Add("能量超频", "能量超频：将伤害加成提高到20点，持续时间10s。");
                Skills[index].GetComponent<Image>().sprite = SkillC;
                Skills[index].GetComponentInChildren<Text>().text = "能量超频";
                ActorController.ep -= 20;
                index++;
            }
            else
            {
                TowerInformation.text = "能量不足，请收集能量";
                notBuild = false;
            }
        }
        else if (towerIndex == 3)
        {
            if (ActorController.ep >= 30)
            {
                Instantiate(TowerD, towerGround);
                notBuild = true;
                towerIndex = -1;
                gameObject.SetActive(false);
                GetComponentsInChildren<Button>()[3].interactable = false;
                SkillUI.SkillDic.Add("火力轰炸", "火力轰炸：呼叫电磁炮台进行以自己为中心的精准轰炸。对大范围内敌人造成20点伤害。");
                Skills[index].GetComponent<Image>().sprite = SkillD;
                Skills[index].GetComponentInChildren<Text>().text = "火力轰炸";
                ActorController.ep -= 30;
                index++;
            }
            else
            {
                TowerInformation.text = "能量不足，请收集能量";
                notBuild = false;
            }           
        }
        else if (towerIndex == 4)
        {
            if (ActorController.ep >= 30)
            {
                Instantiate(TowerE, towerGround);
                notBuild = true;
                towerIndex = -1;
                gameObject.SetActive(false);
                GetComponentsInChildren<Button>()[4].interactable = false; SkillUI.SkillDic.Add("能量护盾", "能量护盾：将吸收的能量转换成护盾。根据已吸收的能量数值判定护盾大小。");
                Skills[index].GetComponent<Image>().sprite = SkillE;
                Skills[index].transform.GetChild(1).GetComponent<Image>().fillAmount = 1;
                Skills[index].GetComponentInChildren<Text>().text = "能量护盾";
                EnergyShieldSkill = index;
                ActorController.ep -= 30;
                index++;
            }
            else
            {
                TowerInformation.text = "能量不足，请收集能量";
                notBuild = false;
            }       
        }
        else if (towerIndex == -1)
        {
            return;
        }

        if (notBuild)
        {
            switch (towerGround.name)
            {
                case "GunTower1":
                    Bt1.CanBuild = false;
                    print("1");
                    break;
                case "GunTower2":
                    Bt2.CanBuild = false;
                    break;
                case "GunTower3":
                    Bt3.CanBuild = false;
                    break;
                case "GunTower4":
                    Bt4.CanBuild = false;
                    break;
                default:
                    break;
            }
        }
    }

    public void Esc()
    {
        //Bt1.CanBuild = true;
        //Bt2.CanBuild = true;
        //Bt3.CanBuild = true;
        //Bt4.CanBuild = true;
        //Bt1.Tips.SetActive(true);
        //Bt2.Tips.SetActive(true);
        //Bt3.Tips.SetActive(true);
        //Bt4.Tips.SetActive(true);
        EnemyController.x = 1;
        gameObject.SetActive(false);
    }

    public void BuildGround(Transform trans)
    {
        towerGround = trans;
    }
}
