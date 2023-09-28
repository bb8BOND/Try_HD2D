using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandSO : ScriptableObject
{
    public new string Name;

    // ’Ç‰ÁF“G‚ğUŒ‚‚·‚éƒƒ\ƒbƒh
    public void Attack(ref int targetHP , int atpw)
    {
        targetHP -= atpw; // –¡•û‚ÌUŒ‚—Í‚ğ“G‚ÌHP‚©‚çŒ¸Z
        Debug.Log($"“G‚É{atpw}‚ÌUŒ‚I “G‚ÌHP: " + targetHP);
    }
}
