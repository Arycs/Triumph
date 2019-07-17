using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemysCreator : MonoBehaviour {

    public static int enemyAliveCount = 0;
    public Wave[] waves;
    public float waveRate = 3;
    private int x = 1;
    public Text waveText;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        enemyAliveCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    //迭代生成敌人
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            //上一波敌人没消灭前，下一波敌人不会生成
            while (enemyAliveCount > 0)
            {
                yield return false;
            }

            waveText.text = x + " / 10";
            x++;

            //每一波生成的间隔
            yield return new WaitForSeconds(waveRate);
            for (int i = 0; i < wave.count; i++)
            {

                Instantiate(wave.enemyPrefab[Random.Range(0, wave.enemyPrefab.Length)], transform.position, Quaternion.identity);
                //一波中每个敌人的生成间隔
                //if (i != wave.count - 1)
                //{
                    yield return new WaitForSeconds(wave.rate);
                //}
            }
        }
    }
}
