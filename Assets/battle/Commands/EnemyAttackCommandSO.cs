using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyAttackCommandSO : CommandSO
{

    public void Attack2(ref int member1HP, ref int member2HP , ref int member3HP , int atpw)
    {
        member1HP -= atpw;
        member2HP -= atpw;
        member3HP -= atpw;
        Debug.Log($"味方に{atpw}の攻撃！残りHP{member1HP}と{member2HP}と{member3HP}");
    }
}