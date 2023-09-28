using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandSO : CommandSO
{

    public void Attack(ref int targetHP, int atpw)
    {
        targetHP -= atpw; // –¡•û‚ÌUŒ‚—Í‚ğ“G‚ÌHP‚©‚çŒ¸Z
        Debug.Log($"“G‚É{atpw}‚ÌUŒ‚I “G‚Ìc‚èHP: " + targetHP);
    }
}

