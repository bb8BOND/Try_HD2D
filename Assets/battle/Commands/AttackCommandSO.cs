using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandSO : CommandSO
{

    public void Attack(ref int targetHP, int atpw)
    {
        targetHP -= atpw; // ĄűĚUÍđGĚHPŠç¸Z
        Debug.Log($"GÉ{atpw}ĚUI GĚcčHP: " + targetHP);
    }
}

