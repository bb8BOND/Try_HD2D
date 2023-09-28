using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandSO : CommandSO
{
    // hlpの2倍までの範囲の整数をランダムに選択する関数
    private int GetRandomHealValue(int hlp)
    {
        return Random.Range(1, hlp * 2 + 1);
    }

    public void Heal(ref int targetHP1, ref int targetHP2, ref int targetHP3, int hlp)
    {
        int healValue = GetRandomHealValue(hlp); // ランダムな回復量を取得

        targetHP1 += healValue;
        targetHP2 += healValue;
        targetHP3 += healValue;

        Debug.Log($"味方に{healValue}の回復！味方1:{targetHP1} , 味方2:{targetHP2} , 味方3:{targetHP3}");
    }
}
