using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellCommandsSO: CommandSO
{

    // sppw‚Ì2”{‚Ü‚Å‚Ì”ÍˆÍ‚Ì®”‚ğƒ‰ƒ“ƒ_ƒ€‚É‘I‘ğ‚·‚éŠÖ”
    private int GetRandomHealValue(int sppw)
    {
        return Random.Range(1, sppw * 2 + 1);
    }

    public void Spell(ref int targetHP, int sppw)
    {
        int spellValue = GetRandomHealValue(sppw); // ƒ‰ƒ“ƒ_ƒ€‚ÈUŒ‚—Í‚ğæ“¾
        targetHP -= spellValue;
        Debug.Log($"“G‚É{spellValue}‚Ì–‚–@UŒ‚I “G‚Ìc‚èHP: " + targetHP);
    }
}
