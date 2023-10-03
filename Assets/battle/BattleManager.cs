using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;

public class BattleManager : MonoBehaviour
{
    // 敵の設定
    public GameObject SlimePBR_battle;
    public GameObject TurtleShellPBR_battle;
    public GameObject Dragon_1_battle;
    public GameObject Dragon_2_battle;
    public GameObject Dragon_3_battle;
    public GameObject Dragon_4_battle;
    public GameObject Dragon_5_battle;

    // 味方の設定
    public GameObject[] friends;
    

    public GameObject member1;//味方1
    public GameObject member2;//味方2
    public GameObject member3;//味方3



    public Transform enemy_place; // 敵の出現場所
    public Transform member1_place; // 味方1の出現場所
    public Transform member2_place; // 味方2の出現場所
    public Transform member3_place; // 味方3の出現場所

    //攻撃の設定
    public CommandSO commandSO;
    public AttackCommandSO attackcommandSO;
    public HealCommandSO healcommandSO;
    public SpellCommandSO spellcommandSO;
    public EnemyAttackCommandSO enemy_attackcommandSO;

    private string[] nowselected;

    public GameObject[] Hiteffect;
    public GameObject HealingCircle;

    public GameObject Result;
    public GameObject Win;
    public GameObject Lose;

    public GameObject number;
    public GameObject One;
    public GameObject Two;
    public GameObject Three;

    [SerializeField]private Fade fade;

    // EnemyManagerスクリプトへの参照
    //public GameObject EnemyManager;

    enum Phase
    {
        StartPhase,
        ChooseCommandPhase,
        ExecutePhase,
        ResultPhase,
        EndPhase,
    }
    Phase phase;

    // HP/AT/TYを管理する変数
    int enemyHP;
    int enemyAT;
    int member1HP;
    int member2HP;
    int member3HP;
    int member1AT;
    int member2AT;
    int member3AT;
    int member1TY;
    int member2TY;
    int member3TY;

    void Start()
    {
        phase = Phase.StartPhase;
        StartCoroutine(Battle());

        // 敵味方の生成
        GameObject enemyPrefab = null;

        if (Woman.collidedEnemyName == "GNCT")
        {
            enemyPrefab = SlimePBR_battle;
            enemyHP = SlimePBR_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = SlimePBR_battle.GetComponent<EnemyBattle>().at;
        }
        else if (Woman.collidedEnemyName == "NITGC")
        {
            enemyPrefab = TurtleShellPBR_battle;
            enemyHP = TurtleShellPBR_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = TurtleShellPBR_battle.GetComponent<EnemyBattle>().at;
        }else if (Woman.collidedEnemyName == "D科")
        {
            enemyPrefab = Dragon_1_battle;
            enemyHP = Dragon_1_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = Dragon_1_battle.GetComponent<EnemyBattle>().at;
        }
        else if (Woman.collidedEnemyName == "C科")
        {
            enemyPrefab = Dragon_2_battle;
            enemyHP = Dragon_2_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = Dragon_2_battle.GetComponent<EnemyBattle>().at;
        }
        else if (Woman.collidedEnemyName == "E科")
        {
            enemyPrefab = Dragon_3_battle;
            enemyHP = Dragon_3_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = Dragon_3_battle.GetComponent<EnemyBattle>().at;
        }
        else if (Woman.collidedEnemyName == "A科")
        {
            enemyPrefab = Dragon_4_battle;
            enemyHP = Dragon_4_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = Dragon_4_battle.GetComponent<EnemyBattle>().at;
        }
        else if (Woman.collidedEnemyName == "M科")
        {
            enemyPrefab = Dragon_5_battle;
            enemyHP = Dragon_5_battle.GetComponent<EnemyBattle>().hp;
            enemyAT = Dragon_5_battle.GetComponent<EnemyBattle>().at;
        }

        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemy_place.position, Quaternion.identity);
            enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        //ScrollViewスクリプトからSerchfileを参照し、結果をresultに保存する。
        ScrollView scrollView = new ScrollView();
        Tuple<int, List<string>> result = scrollView.Searchfile();
        int pngCount = result.Item1; // 1番目の返り値(ファイルの総数)を取得
        List<string> pngFiles = result.Item2; // 2番目の返り値(ファイルのパスのリスト)を取得
        for (int i = 0; i < pngCount; i++)
        {
            var gb = new GameObject("character_" + i.ToString(), typeof(SpriteRenderer), typeof(Balance));
            gb.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            var texture = new Texture2D(0, 0);
            var path = pngFiles[i];
            var bytes = File.ReadAllBytes(path);
            texture.LoadImage(bytes);
            var rect = new Rect(0, 0, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, Vector2.zero);
            var spriteRenderer = gb.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;

            //キャラクターの攻撃力やhpをPlayerPrefsから取得
            string power_id = i.ToString() + "_Attack";
            string hp_id = i.ToString() + "_Hp";
            int attack = PlayerPrefs.GetInt(power_id);
            int hp = PlayerPrefs.GetInt(hp_id);
            if(attack != 0 && hp != 0)
            {
                gb.GetComponent<Balance>().power = attack;
                gb.GetComponent<Balance>().hp = hp;
            }

            // ゲームオブジェクトをfriendsリストに追加
            Array.Resize(ref friends, friends.Length + 1);
            friends[friends.Length - 1] = gb;
            Debug.Log($"リストに{friends[i]}を追加しました。");
        }

        Debug.Log($"member1は{EnemyManager.member1}");
        Debug.Log($"member2は{EnemyManager.member2}");
        Debug.Log($"member3は{EnemyManager.member3}");
        member1 = friends[EnemyManager.member1];
        member2 = friends[EnemyManager.member2];
        member3 = friends[EnemyManager.member3];
        GameObject instantiatedMember1 = Instantiate(member1, member1_place.position, Quaternion.Euler(0f, 90f, 0f));
        GameObject instantiatedMember2 = Instantiate(member2, member2_place.position, Quaternion.Euler(0f, 90f, 0f));
        GameObject instantiatedMember3 = Instantiate(member3, member3_place.position, Quaternion.Euler(0f, 90f, 0f));
        member1HP = member1.GetComponent<Balance>().hp;
        member2HP = member2.GetComponent<Balance>().hp;
        member3HP = member3.GetComponent<Balance>().hp;
        member1AT = member1.GetComponent<Balance>().power;
        member2AT = member2.GetComponent<Balance>().power;
        member3AT = member3.GetComponent<Balance>().power;
        member1TY = member1.GetComponent<Balance>().type;
        member2TY = member2.GetComponent<Balance>().type;
        member3TY = member3.GetComponent<Balance>().type;


        nowselected = new string[4];

        commandSO = ScriptableObject.CreateInstance<CommandSO>(); // CommandSOインスタンスを作成
        attackcommandSO = ScriptableObject.CreateInstance<AttackCommandSO>();
        healcommandSO = ScriptableObject.CreateInstance<HealCommandSO>();
        spellcommandSO = ScriptableObject.CreateInstance<SpellCommandSO>();
        enemy_attackcommandSO = ScriptableObject.CreateInstance<EnemyAttackCommandSO>();
    }


    IEnumerator Battle()
    {
        while (phase != Phase.EndPhase)
        {
            yield return null;
            switch (phase)
            {
                case Phase.StartPhase:
                    phase = Phase.ChooseCommandPhase;
                    break;
                case Phase.ChooseCommandPhase:
                    number.SetActive(true);
                    for (int i = 0; i < 3; i++)
                    {
                        if(i == 0)
                        {
                            One.SetActive(true);
                        }else if(i == 1)
                        {
                            One.SetActive(false);
                            Two.SetActive(true);
                        }else if(i == 2)
                        {
                            Two.SetActive(false);
                            Three.SetActive(true);
                        }
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U));
                        nowselected [i] = Select.currentCommand; // 現在選択中のコマンドを配列に格納
                        Debug.Log($"選ばれたのは{nowselected [i]}でした。");
                        yield return new WaitForSeconds(0.15f); // 適切な待機時間を設定する
                    }
                    Three.SetActive(false);
                    number.SetActive(false);
                    
                    phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    
                    //味方1の行動
                    if(nowselected [0] == "Attack")
                    {
                        Hiteffect[member1TY].SetActive(true);
                        attackcommandSO.Attack(ref enemyHP, member1AT );
                        yield return new WaitForSeconds(0.9f);//Attackモーションの待機時間
                        Hiteffect[member1TY].SetActive(false);

                    }else if(nowselected [0] == "Spell")
                    {
                        Hiteffect[member1TY + 3].SetActive(true);
                        spellcommandSO.Spell(ref enemyHP, member1AT );
                        yield return new WaitForSeconds(0.9f);//Spellモーションの待機時間
                        Hiteffect[member1TY + 3].SetActive(false);
                    }
                    else if (nowselected [0] == "Item")
                    {
                        HealingCircle.SetActive(true);
                        healcommandSO.Heal(ref member1HP, ref member2HP , ref member3HP , member1AT);
                        yield return new WaitForSeconds(3.85f);//Itemモーションの待機時間
                        HealingCircle.SetActive(false);
                    }
                    else if (nowselected[0] == "Escape")
                    {
                        Initiate.Fade("Scene0_map", Color.black, 1.0f);
                        //if (Randomescape())
                        //{
                        //    Debug.Log("どうやらぁ、うまく巻いたようだぁ");
                        //}else if (!Randomescape())
                        //{
                        //    Debug.Log("うまく逃げきれなかった");
                        //}
                    }

                    if (enemyHP <= 0 || member1HP <= 0 || member2HP <= 0 || member3HP <= 0)
                    {
                        phase = Phase.ResultPhase;
                        break;
                    }
                    //yield return new WaitForSeconds(1f);
                    
                    //味方2の行動
                    if (nowselected[1] == "Attack")
                    {
                        Hiteffect[member2TY].SetActive(true);
                        attackcommandSO.Attack(ref enemyHP, member2AT);
                        yield return new WaitForSeconds(0.9f);//Attackモーションの待機時間
                        Hiteffect[member2TY].SetActive(false);
                    }
                    else if (nowselected[1] == "Spell")
                    {
                        Hiteffect[member2TY + 3].SetActive(true);
                        spellcommandSO.Spell(ref enemyHP, member2AT);
                        yield return new WaitForSeconds(0.9f);//Spellモーションの待機時間
                        Hiteffect[member2TY + 3].SetActive(false);
                    }
                    else if (nowselected[1] == "Item")
                    {
                        HealingCircle.SetActive(true);
                        healcommandSO.Heal(ref member1HP, ref member2HP, ref member3HP, member2AT);
                        yield return new WaitForSeconds(3.85f);//Itemモーションの待機時間
                        HealingCircle.SetActive(false);
                    }
                    else if (nowselected[1] == "Escape")
                    {
                        Initiate.Fade("Scene0_map", Color.black, 1.0f);
                        //if (Randomescape())
                        //{
                        //    Debug.Log("どうやらぁ、うまく巻いたようだぁ");
                        //}
                        //else if (!Randomescape())
                        //{
                        //    Debug.Log("うまく逃げきれなかった");
                        //}
                    }
                    if (enemyHP <= 0 || member1HP <= 0 || member2HP <= 0 || member3HP <= 0)
                    {
                        phase = Phase.ResultPhase;
                        break;
                    }
                    //yield return new WaitForSeconds(1f);
                    
                    //味方3の行動
                    if (nowselected[2] == "Attack")
                    {
                        Hiteffect[member3TY].SetActive(true);
                        attackcommandSO.Attack(ref enemyHP, member3AT);
                        yield return new WaitForSeconds(0.9f);//Attackモーションの待機時間
                        Hiteffect[member3TY].SetActive(false);
                    }
                    else if (nowselected[2] == "Spell")
                    {
                        Hiteffect[member3TY + 3].SetActive(true);
                        spellcommandSO.Spell(ref enemyHP, member3AT);
                        yield return new WaitForSeconds(0.9f);//Spellモーションの待機時間
                        Hiteffect[member3TY + 3].SetActive(false);
                    }
                    else if (nowselected[2] == "Item")
                    {
                        HealingCircle.SetActive(true);
                        healcommandSO.Heal(ref member1HP, ref member2HP, ref member3HP, member3AT);
                        yield return new WaitForSeconds(3.85f);//Itemモーションの待機時間
                        HealingCircle.SetActive(false);
                    }
                    else if (nowselected[2] == "Escape")
                    {
                        Initiate.Fade("Scene0_map", Color.black, 1.0f);
                        //if (Randomescape())
                        //{
                        //    Debug.Log("どうやらぁ、うまく巻いたようだぁ");
                        //}
                        //else if (!Randomescape())
                        //{
                        //    Debug.Log("うまく逃げきれなかった");
                        //}
                    }
                    if (enemyHP <= 0 || member1HP <= 0 || member2HP <= 0 || member3HP <= 0)
                    {
                        phase = Phase.ResultPhase;
                        break;
                    }
                    yield return new WaitForSeconds(1f);

                    enemy_attackcommandSO.Attack2(ref member1HP, ref member2HP, ref member3HP, enemyAT);

                    if (enemyHP >= 0 || member1HP >= 0 || member2HP >= 0 || member3HP >= 0)
                    {
                        phase = Phase.ChooseCommandPhase;
                    }
                    
                    break;
                case Phase.ResultPhase:
                    Debug.Log("終了！！");
                    Time.timeScale = 0f;
                    Result.SetActive(true);
                    if(enemyHP <= 0)
                    {
                        Win.SetActive(true);
                    }
                    else
                    {
                        Lose.SetActive(true);
                    }
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.P));
                    Time.timeScale = 1f;
                    phase = Phase.EndPhase;
                    if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.P))
                    {
                        Time.timeScale = 1f;
                        fade.FadeIn(1f, () => SceneManager.LoadScene("Scene0_map"));
                        Win.SetActive(false);
                        Lose.SetActive(false);

                    }
                    
                    break;
                case Phase.EndPhase:
                    break;
            }
            
        }
    }

    

    void Update()
    {
        
    }

    bool Randomescape()
    {
        bool result = UnityEngine.Random.Range(0, 2) == 0; // 0または1のランダムな値を取得

        return result;
    }
}
