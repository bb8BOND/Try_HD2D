using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandSO : ScriptableObject
{
    public new string Name;

    // �ǉ��F�G���U�����郁�\�b�h
    public void Attack(ref int targetHP , int atpw)
    {
        targetHP -= atpw; // �����̍U���͂�G��HP���猸�Z
        Debug.Log($"�G��{atpw}�̍U���I �G��HP: " + targetHP);
    }
}
