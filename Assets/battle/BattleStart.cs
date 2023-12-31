using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public GameObject SlimePBR_battle; // 敵1の設定
    public GameObject TurtleShellPBR_battle; // 敵2の設定

    public GameObject yuusya; // 味方1の設定
    public GameObject mahoutukai; // 味方2の設定
    public GameObject kisi; // 味方3の設定

    public Transform enemy_place; // 敵の出現場所
    public Transform member1_place; // 味方1の出現場所
    public Transform member2_place; // 味方2の出現場所
    public Transform member3_place; // 味方3の出現場所

    void Start()
    {
        GameObject enemyPrefab = null;

        if (Woman.collidedEnemyName == "SlimePBR")
        {
            enemyPrefab = SlimePBR_battle;
        }
        else if (Woman.collidedEnemyName == "TurtleShellPBR")
        {
            enemyPrefab = TurtleShellPBR_battle;
        }

        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemy_place.position, Quaternion.identity);
            enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        GameObject member1 = Instantiate(yuusya, member1_place.position, Quaternion.Euler(0f, 90f, 0f));
        GameObject member2 = Instantiate(mahoutukai, member2_place.position, Quaternion.Euler(0f, 90f, 0f));
        GameObject member3 = Instantiate(kisi, member3_place.position, Quaternion.Euler(0f, 90f, 0f));
    }

    void Update()
    {

    }
}
