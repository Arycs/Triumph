using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour {

    public bool CanBuild = true;
    //E键 提示
    public GameObject Tips;
    //建造塔的界面信息
    public GameObject TowerInformation;

    public BuildTowerUI buildUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && CanBuild == true)
        {
            buildUI.BuildGround(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Tips.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && CanBuild == true)
        {
            Tips.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TowerInformation.SetActive(true);
            }
        }

        if (other.gameObject.tag == "Player" && CanBuild == false)
        {
            Tips.SetActive(false);
        }
    }
}
