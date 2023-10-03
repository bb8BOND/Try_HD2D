using UnityEngine;
using TMPro;

public class EnemyName : MonoBehaviour
{
    public string characterName; // �L�����N�^�[�̖��O
    private TextMeshProUGUI textMeshPro; // TextMeshPro�̎Q��

    private void Start()
    {
        // TextMeshPro�����ɍ쐬����Ă��邩�`�F�b�N
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        // TextMeshPro��������Ȃ��ꍇ�́A�L�����N�^�[�I�u�W�F�N�g�̉��ɍ쐬����Ă���TextMeshPro������
        if (textMeshPro == null)
        {
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        }

        // ����TextMeshPro��������Ȃ��ꍇ�̓G���[���b�Z�[�W��\��
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro��������܂���ł����B�L�����o�X��TextMeshPro���A�^�b�`���Ă��������B");
            return;
        }

        //Debug.Log($"{woman.collidedEnemyName}");
        characterName = Woman.collidedEnemyName;

        // �L�����N�^�[�̖��O��TextMeshPro�ɐݒ�
        textMeshPro.text = characterName;

    }
}