using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_pause : MonoBehaviour
{
    public GameObject nowIndicator; // "now"�������I�u�W�F�N�g
    public GameObject[] options;    // �I�����̃I�u�W�F�N�g�iback�Aeditteam�Atitle�̏��ԁj

    private bool isInputEnabled = true; // ���̗͂L�����t���O
    private int selectedIndex = 0;      // �I������Ă��鍀�ڂ̃C���f�b�N�X

    void Start()
    {
        // ������Ԃōŏ��̍��ڂ�I����ԂƂ���
        SelectOption(0);
    }

    // Update is called once per frame
    void Update()
    {
        // ���͂��L����������L�[�������ꂽ�ꍇ
        if (isInputEnabled && Input.GetAxis("Vertical") > 0)
        {
            if (selectedIndex > 0)
            {
                SelectOption(selectedIndex - 1);
            }
        }
        // ���͂��L�����������L�[�������ꂽ�ꍇ
        else if (isInputEnabled && Input.GetAxis("Vertical") < 0)
        {
            if (selectedIndex < options.Length - 1)
            {
                SelectOption(selectedIndex + 1);
            }
        }
    }

    // �I�����ꂽ���ڂ��X�V���郁�\�b�h
    private void SelectOption(int index)
    {
        selectedIndex = index;

        // �I������Ă��鍀�ڂ̈ʒu��"now"�I�u�W�F�N�g���ړ�������
        var targetPosition = options[selectedIndex].transform.position;
        nowIndicator.transform.position = new Vector3(targetPosition.x, targetPosition.y, nowIndicator.transform.position.z);
    }
}
