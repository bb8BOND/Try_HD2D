using UnityEngine;
using TMPro;

public class result_name : MonoBehaviour
{
    // �G���O��TextMeshPro�I�u�W�F�N�g���Q�Ƃ���ϐ�
    public TextMeshProUGUI enemyNameText;
    //������TextMeshPro�I�u�W�F�N�g���Q�Ƃ���ϐ�
    public TextMeshProUGUI explainText;

    // �����̓G���O
    private string initialEnemyName;
    private string initialexplain;


    private void Start()
    {
        explainText.text = woman.enemy_explain;
        enemyNameText.text = woman.collidedEnemyName;
    }

    

    
}
