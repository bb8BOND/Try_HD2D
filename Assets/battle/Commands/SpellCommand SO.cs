using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellCommandSO: CommandSO
{

    // sppwÌ2{ÜÅÌÍÍÌ®ð_ÉIð·éÖ
    private int GetRandomHealValue(int sppw)
    {
        return Random.Range(1, sppw * 2 + 1);
    }

    public void Spell(ref int targetHP, int sppw)
    {
        var spellValue = GetRandomHealValue(sppw); // _ÈUÍðæ¾
        targetHP -= spellValue;
        Debug.Log($"GÉ{spellValue}Ì@UI GÌcèHP: " + targetHP);
    }
}
