using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandSO : CommandSO
{
    // hlp��2�{�܂ł͈̔͂̐����������_���ɑI������֐�
    private int GetRandomHealValue(int hlp)
    {
        return Random.Range(1, hlp * 2 + 1);
    }

    public void Heal(ref int targetHP1, ref int targetHP2, ref int targetHP3, int hlp)
    {
        int healValue = GetRandomHealValue(hlp); // �����_���ȉ񕜗ʂ��擾

        targetHP1 += healValue;
        targetHP2 += healValue;
        targetHP3 += healValue;

        Debug.Log($"������{healValue}�̉񕜁I����1:{targetHP1} , ����2:{targetHP2} , ����3:{targetHP3}");
    }
}
