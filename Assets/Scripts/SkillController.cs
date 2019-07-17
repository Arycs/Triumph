using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

    public IUserInput pi;
    public int index;

    public Image image;

    public GameObject[] gos;

    void Awake () {

        pi = GameObject.Find("PlayerHandler").GetComponent<IUserInput>();

        image = transform.parent.GetChild(1).GetComponent<Image>();
    }
	
	void Update () {

        if (GetComponent<Text>().text != "无技能")
        {
            if (pi.skill1 && index == 1)
            {
                ReleaseSkill();
            }

            if (pi.skill2 && index == 2)
            {
                ReleaseSkill();
            }

            if (pi.skill3 && index == 3)
            {
                ReleaseSkill();
            }

            if (pi.skill4 && index == 4)
            {
                ReleaseSkill();
            }
        }

        if (GetComponent<Text>().text != "能量护盾")
        {
            if (image.fillAmount <= 1 && image.fillAmount > 0)
            {
                image.fillAmount -= 0.1f * Time.deltaTime;
            }
        }

        gos = GameObject.FindGameObjectsWithTag("Tower");
    }

    public void ReleaseSkill()
    {
        switch (GetComponent<Text>().text)
        {
            case "同调":
                print("同调");

                break;
            case "磁力引爆":
                if (image.fillAmount == 0)
                {
                    foreach (GameObject go in gos)
                    {
                        go.GetComponent<TowerController>().MagneticExplosion();
                    }
                    image.fillAmount = 1;
                }
               
                break;
            case "能量超频":
                if (image.fillAmount == 0)
                {
                    foreach (GameObject go in gos)
                    {
                        go.GetComponent<TowerController>().EnergyOverclock();
                    }
                    image.fillAmount = 1;
                }
                break;
            case "火力轰炸":
                if (image.fillAmount == 0)
                {
                    foreach (GameObject go in gos)
                    {
                        go.GetComponent<TowerController>().FireBomb();
                    }
                    image.fillAmount = 1;
                }
                break;
            case "能量护盾":
                if (image.fillAmount == 0)
                {
                    foreach (GameObject go in gos)
                    {
                        go.GetComponent<TowerController>().EnergyShield();
                    }
                    ActorController.ap += 10;
                    image.fillAmount = 1;
                }
                break;
            case "无技能":
                print("无技能");
                break;
            default:
                break;
        }
    }
}
