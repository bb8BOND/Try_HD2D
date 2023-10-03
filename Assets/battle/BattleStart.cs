using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public GameObject SlimePBR_battle; // “G1‚Ìİ’è
    public GameObject TurtleShellPBR_battle; // “G2‚Ìİ’è

    public GameObject yuusya; // –¡•û1‚Ìİ’è
    public GameObject mahoutukai; // –¡•û2‚Ìİ’è
    public GameObject kisi; // –¡•û3‚Ìİ’è

    public Transform enemy_place; // “G‚ÌoŒ»êŠ
    public Transform member1_place; // –¡•û1‚ÌoŒ»êŠ
    public Transform member2_place; // –¡•û2‚ÌoŒ»êŠ
    public Transform member3_place; // –¡•û3‚ÌoŒ»êŠ

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
