using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandSO : CommandSO
{

    public void Attack(ref int targetHP, int atpw)
    {
        targetHP -= atpw; // �����̍U���͂�G��HP���猸�Z
        Debug.Log($"�G��{atpw}�̍U���I �G�̎c��HP: " + targetHP);
    }
}

