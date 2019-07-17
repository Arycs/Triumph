using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillUI : MonoBehaviour {

    public static Dictionary<string, string> SkillDic = new Dictionary<string, string>();

    public GameObject SkillInformation;

    public GameObject SkillA;
    public GameObject SkillB;
    public GameObject SkillC;
    public GameObject SkillD;

    void Awake() {
        SkillDic.Clear();
        SkillDic.Add("无技能", "当前无技能");
    }

    public void SkillAInformation()
    {
        SkillInformation.SetActive(true);
        SkillInformation.GetComponentInChildren<Text>().text = SkillDic[SkillA.GetComponentInChildren<Text>().text];
    }
    public void SkillBInformation()
    {
        SkillInformation.SetActive(true);
        SkillInformation.GetComponentInChildren<Text>().text = SkillDic[SkillB.GetComponentInChildren<Text>().text];
    }
    public void SkillCInformation()
    {
        SkillInformation.SetActive(true);
        SkillInformation.GetComponentInChildren<Text>().text = SkillDic[SkillC.GetComponentInChildren<Text>().text];
    }
    public void SkillDInformation()
    {
        SkillInformation.SetActive(true);
        SkillInformation.GetComponentInChildren<Text>().text = SkillDic[SkillD.GetComponentInChildren<Text>().text];
    }

    public void Esc()
    {
        SkillInformation.SetActive(false);
    }
}
