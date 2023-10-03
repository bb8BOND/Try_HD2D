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
        Debug.Log($"–¡•û‚É{atpw}‚ÌUŒ‚Ic‚èHP{member1HP}‚Æ{member2HP}‚Æ{member3HP}");
    }
}