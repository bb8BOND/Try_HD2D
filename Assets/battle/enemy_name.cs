using UnityEngine;
using TMPro;

public class enemy_name : MonoBehaviour
{
    public string characterName; // キャラクターの名前
    private TextMeshProUGUI textMeshPro; // TextMeshProの参照

    private void Start()
    {
        // TextMeshProが既に作成されているかチェック
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        // TextMeshProが見つからない場合は、キャラクターオブジェクトの下に作成されているTextMeshProを検索
        if (textMeshPro == null)
        {
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        }

        // もしTextMeshProが見つからない場合はエラーメッセージを表示
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProが見つかりませんでした。キャンバスにTextMeshProをアタッチしてください。");
            return;
        }

        //Debug.Log($"{woman.collidedEnemyName}");
        characterName = woman.collidedEnemyName;

        // キャラクターの名前をTextMeshProに設定
        textMeshPro.text = characterName;

    }
}