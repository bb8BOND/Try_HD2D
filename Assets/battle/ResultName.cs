using UnityEngine;
using TMPro;

public class ResultName : MonoBehaviour
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
        explainText.text = Woman.enemy_explain;
        enemyNameText.text = Woman.collidedEnemyName;
    }

    

    
}
