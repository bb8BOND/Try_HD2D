using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandSO : ScriptableObject
{
    public new string name;

    // ÇÁFGđUˇé\bh
    public void Attack(ref int targetHP , int atpw)
    {
        targetHP -= atpw; // ĄűĚUÍđGĚHPŠç¸Z
        Debug.Log($"GÉ{atpw}ĚUI GĚHP: " + targetHP);
    }
}
