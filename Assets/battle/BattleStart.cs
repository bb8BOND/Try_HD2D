using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public GameObject SlimePBR_battle; // �G1�̐ݒ�
    public GameObject TurtleShellPBR_battle; // �G2�̐ݒ�

    public GameObject yuusya; // ����1�̐ݒ�
    public GameObject mahoutukai; // ����2�̐ݒ�
    public GameObject kisi; // ����3�̐ݒ�

    public Transform enemy_place; // �G�̏o���ꏊ
    public Transform member1_place; // ����1�̏o���ꏊ
    public Transform member2_place; // ����2�̏o���ꏊ
    public Transform member3_place; // ����3�̏o���ꏊ

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
