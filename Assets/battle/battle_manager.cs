using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battle_manager : MonoBehaviour
{
    // �G�̐ݒ�
    public GameObject SlimePBR_battle;
    public GameObject TurtleShellPBR_battle;
    public GameObject Dragon_1_battle;
    public GameObject Dragon_2_battle;
    public GameObject Dragon_3_battle;
    public GameObject Dragon_4_battle;
    public GameObject Dragon_5_battle;

    // �����̐ݒ�
    public GameObject[] friends;
    

    public GameObject member1;//����1
    public GameObject member2;//����2
    public GameObject member3;//����3



    public Transform enemy_place; // �G�̏o���ꏊ
    public Transform member1_place; // ����1�̏o���ꏊ
    public Transform member2_place; // ����2�̏o���ꏊ
    public Transform member3_place; // ����3�̏o���ꏊ

    //�U���̐ݒ�
    public CommandSO commandSO;
    public AttackCommandSO attackcommandSO;
    public HealCommandSO healcommandSO;
    public SpellCommandSO spellcommandSO;
    public enemy_AttackCommandSO enemy_attackcommandSO;

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

    // enemy_manager�X�N���v�g�ւ̎Q��
    //public GameObject enemy_manager;

    enum Phase
    {
        StartPhase,
        ChooseCommandPhase,
        ExecutePhase,
        ResultPhase,
        EndPhase,
    }
    Phase phase;

    // HP/AT/TY���Ǘ�����ϐ�
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

        // �G�����̐���
        GameObject enemyPrefab = null;

        if (woman.collidedEnemyName == "GNCT")
        {
            enemyPrefab = SlimePBR_battle;
            enemyHP = SlimePBR_battle.GetComponent<enemy_battle>().hp;
            enemyAT = SlimePBR_battle.GetComponent<enemy_battle>().at;
        }
        else if (woman.collidedEnemyName == "NITGC")
        {
            enemyPrefab = TurtleShellPBR_battle;
            enemyHP = TurtleShellPBR_battle.GetComponent<enemy_battle>().hp;
            enemyAT = TurtleShellPBR_battle.GetComponent<enemy_battle>().at;
        }else if (woman.collidedEnemyName == "D��")
        {
            enemyPrefab = Dragon_1_battle;
            enemyHP = Dragon_1_battle.GetComponent<enemy_battle>().hp;
            enemyAT = Dragon_1_battle.GetComponent<enemy_battle>().at;
        }
        else if (woman.collidedEnemyName == "C��")
        {
            enemyPrefab = Dragon_2_battle;
            enemyHP = Dragon_2_battle.GetComponent<enemy_battle>().hp;
            enemyAT = Dragon_2_battle.GetComponent<enemy_battle>().at;
        }
        else if (woman.collidedEnemyName == "E��")
        {
            enemyPrefab = Dragon_3_battle;
            enemyHP = Dragon_3_battle.GetComponent<enemy_battle>().hp;
            enemyAT = Dragon_3_battle.GetComponent<enemy_battle>().at;
        }
        else if (woman.collidedEnemyName == "A��")
        {
            enemyPrefab = Dragon_4_battle;
            enemyHP = Dragon_4_battle.GetComponent<enemy_battle>().hp;
            enemyAT = Dragon_4_battle.GetComponent<enemy_battle>().at;
        }
        else if (woman.collidedEnemyName == "M��")
        {
            enemyPrefab = Dragon_5_battle;
            enemyHP = Dragon_5_battle.GetComponent<enemy_battle>().hp;
            enemyAT = Dragon_5_battle.GetComponent<enemy_battle>().at;
        }

        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemy_place.position, Quaternion.identity);
            enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        Debug.Log($"member1��{enemy_manager.member1}");
        Debug.Log($"member2��{enemy_manager.member2}");
        Debug.Log($"member3��{enemy_manager.member3}");
        member1 = friends[enemy_manager.member1];
        member2 = friends[enemy_manager.member2];
        member3 = friends[enemy_manager.member3];
        GameObject instantiatedMember1 = Instantiate(member1, member1_place.position, Quaternion.Euler(0f, 90f, 0f));
        GameObject instantiatedMember2 = Instantiate(member2, member2_place.position, Quaternion.Euler(0f, 90f, 0f));
        GameObject instantiatedMember3 = Instantiate(member3, member3_place.position, Quaternion.Euler(0f, 90f, 0f));
        member1HP = member1.GetComponent<balance>().hp;
        member2HP = member2.GetComponent<balance>().hp;
        member3HP = member3.GetComponent<balance>().hp;
        member1AT = member1.GetComponent<balance>().power;
        member2AT = member2.GetComponent<balance>().power;
        member3AT = member3.GetComponent<balance>().power;
        member1TY = member1.GetComponent<balance>().type;
        member2TY = member2.GetComponent<balance>().type;
        member3TY = member3.GetComponent<balance>().type;


        nowselected = new string[4];

        commandSO = ScriptableObject.CreateInstance<CommandSO>(); // CommandSO�C���X�^���X���쐬
        attackcommandSO = ScriptableObject.CreateInstance<AttackCommandSO>();
        healcommandSO = ScriptableObject.CreateInstance<HealCommandSO>();
        spellcommandSO = ScriptableObject.CreateInstance<SpellCommandSO>();
        enemy_attackcommandSO = ScriptableObject.CreateInstance<enemy_AttackCommandSO>();
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
                        nowselected [i] = Select.currentCommand; // ���ݑI�𒆂̃R�}���h��z��Ɋi�[
                        Debug.Log($"�I�΂ꂽ�̂�{nowselected [i]}�ł����B");
                        yield return new WaitForSeconds(0.15f); // �K�؂ȑҋ@���Ԃ�ݒ肷��
                    }
                    Three.SetActive(false);
                    number.SetActive(false);
                    
                    phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    
                    //����1�̍s��
                    if(nowselected [0] == "Attack")
                    {
                        Hiteffect[member1TY].SetActive(true);
                        attackcommandSO.Attack(ref enemyHP, member1AT );
                        yield return new WaitForSeconds(0.9f);//Attack���[�V�����̑ҋ@����
                        Hiteffect[member1TY].SetActive(false);

                    }else if(nowselected [0] == "Spell")
                    {
                        Hiteffect[member1TY + 3].SetActive(true);
                        spellcommandSO.Spell(ref enemyHP, member1AT );
                        yield return new WaitForSeconds(0.9f);//Spell���[�V�����̑ҋ@����
                        Hiteffect[member1TY + 3].SetActive(false);
                    }
                    else if (nowselected [0] == "Item")
                    {
                        HealingCircle.SetActive(true);
                        healcommandSO.Heal(ref member1HP, ref member2HP , ref member3HP , member1AT);
                        yield return new WaitForSeconds(3.85f);//Item���[�V�����̑ҋ@����
                        HealingCircle.SetActive(false);
                    }
                    else if (nowselected[0] == "Escape")
                    {
                        Initiate.Fade("Scene0_map", Color.black, 1.0f);
                        //if (Randomescape())
                        //{
                        //    Debug.Log("�ǂ���炟�A���܂��������悤����");
                        //}else if (!Randomescape())
                        //{
                        //    Debug.Log("���܂���������Ȃ�����");
                        //}
                    }

                    if (enemyHP <= 0 || member1HP <= 0 || member2HP <= 0 || member3HP <= 0)
                    {
                        phase = Phase.ResultPhase;
                        break;
                    }
                    //yield return new WaitForSeconds(1f);
                    
                    //����2�̍s��
                    if (nowselected[1] == "Attack")
                    {
                        Hiteffect[member2TY].SetActive(true);
                        attackcommandSO.Attack(ref enemyHP, member2AT);
                        yield return new WaitForSeconds(0.9f);//Attack���[�V�����̑ҋ@����
                        Hiteffect[member2TY].SetActive(false);
                    }
                    else if (nowselected[1] == "Spell")
                    {
                        Hiteffect[member2TY + 3].SetActive(true);
                        spellcommandSO.Spell(ref enemyHP, member2AT);
                        yield return new WaitForSeconds(0.9f);//Spell���[�V�����̑ҋ@����
                        Hiteffect[member2TY + 3].SetActive(false);
                    }
                    else if (nowselected[1] == "Item")
                    {
                        HealingCircle.SetActive(true);
                        healcommandSO.Heal(ref member1HP, ref member2HP, ref member3HP, member2AT);
                        yield return new WaitForSeconds(3.85f);//Item���[�V�����̑ҋ@����
                        HealingCircle.SetActive(false);
                    }
                    else if (nowselected[1] == "Escape")
                    {
                        Initiate.Fade("Scene0_map", Color.black, 1.0f);
                        //if (Randomescape())
                        //{
                        //    Debug.Log("�ǂ���炟�A���܂��������悤����");
                        //}
                        //else if (!Randomescape())
                        //{
                        //    Debug.Log("���܂���������Ȃ�����");
                        //}
                    }
                    if (enemyHP <= 0 || member1HP <= 0 || member2HP <= 0 || member3HP <= 0)
                    {
                        phase = Phase.ResultPhase;
                        break;
                    }
                    //yield return new WaitForSeconds(1f);
                    
                    //����3�̍s��
                    if (nowselected[2] == "Attack")
                    {
                        Hiteffect[member3TY].SetActive(true);
                        attackcommandSO.Attack(ref enemyHP, member3AT);
                        yield return new WaitForSeconds(0.9f);//Attack���[�V�����̑ҋ@����
                        Hiteffect[member3TY].SetActive(false);
                    }
                    else if (nowselected[2] == "Spell")
                    {
                        Hiteffect[member3TY + 3].SetActive(true);
                        spellcommandSO.Spell(ref enemyHP, member3AT);
                        yield return new WaitForSeconds(0.9f);//Spell���[�V�����̑ҋ@����
                        Hiteffect[member3TY + 3].SetActive(false);
                    }
                    else if (nowselected[2] == "Item")
                    {
                        HealingCircle.SetActive(true);
                        healcommandSO.Heal(ref member1HP, ref member2HP, ref member3HP, member3AT);
                        yield return new WaitForSeconds(3.85f);//Item���[�V�����̑ҋ@����
                        HealingCircle.SetActive(false);
                    }
                    else if (nowselected[2] == "Escape")
                    {
                        Initiate.Fade("Scene0_map", Color.black, 1.0f);
                        //if (Randomescape())
                        //{
                        //    Debug.Log("�ǂ���炟�A���܂��������悤����");
                        //}
                        //else if (!Randomescape())
                        //{
                        //    Debug.Log("���܂���������Ȃ�����");
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
                    Debug.Log("�I���I�I");
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
        bool result = Random.Range(0, 2) == 0; // 0�܂���1�̃����_���Ȓl���擾

        return result;
    }
}
