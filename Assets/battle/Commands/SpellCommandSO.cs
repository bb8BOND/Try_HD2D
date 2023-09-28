using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellCommandsSO: CommandSO
{

    // sppw��2�{�܂ł͈̔͂̐����������_���ɑI������֐�
    private int GetRandomHealValue(int sppw)
    {
        return Random.Range(1, sppw * 2 + 1);
    }

    public void Spell(ref int targetHP, int sppw)
    {
        int spellValue = GetRandomHealValue(sppw); // �����_���ȍU���͂��擾
        targetHP -= spellValue;
        Debug.Log($"�G��{spellValue}�̖��@�U���I �G�̎c��HP: " + targetHP);
    }
}
