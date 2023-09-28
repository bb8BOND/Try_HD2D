using UnityEngine;
using TMPro;

public class result_name : MonoBehaviour
{
    // 敵名前のTextMeshProオブジェクトを参照する変数
    public TextMeshProUGUI enemyNameText;
    //説明のTextMeshProオブジェクトを参照する変数
    public TextMeshProUGUI explainText;

    // 初期の敵名前
    private string initialEnemyName;
    private string initialexplain;


    private void Start()
    {
        explainText.text = woman.enemy_explain;
        enemyNameText.text = woman.collidedEnemyName;
    }

    

    
}
