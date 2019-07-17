using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    //生成敌人类型
    public GameObject[] enemyPrefab;
    //生成数量
    public int count;
    //生成间隔
    public float rate;
}
