using UnityEngine;
using UnityEngine.UI;


public class ScrollView2 : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollStep = 0.1f; // �X�N���[���̃X�e�b�v�T�C�Y��ݒ�

    // ���ݑI������Ă��鍀�ڂ̃C���f�b�N�X
    public int selectedIndex { get; private set; } = 0;
    //���ݑI������Ă��鍀�ڂ̉摜
    public Transform newSelectedItem;

    // icon�̏����F
    private Color initialIconColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);

    private enemy_manager enemyManager; // �|�[�Y��ʂ��Ǘ�����X�N���v�g�ւ̎Q��

    public GameObject[] members;
    public int memberIndex { get; private set; } = 0;

    void Start()
    {
        enemyManager = FindObjectOfType<enemy_manager>(); // enemy_manager�X�N���v�g�ւ̎Q�Ƃ��擾

        // ������Ԃōŏ��̍��ڂ�I����ԂƂ���
        UpdateSelection(selectedIndex);
        
    }

    void Update()
    {
        if (enemyManager.iseditteam) // �|�[�Y��ʂ��\������Ă��鎞�̂݃X�N���[��������
        {
            // wasd�L�[�̉��������m���ăX�N���[�������ƑI�����ڂ̍X�V�����s����
            if (Input.GetKeyDown(KeyCode.W))
            {
                ScrollUp();
                UpdateSelection(-1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ScrollDown();
                UpdateSelection(1);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                
            }
        }
    }

    // �X�N���[���A�b�v����
    void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition += scrollStep;
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
    }

    // �X�N���[���_�E������
    void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition -= scrollStep;
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
    }

    // �I�����ڂ̍X�V
    void UpdateSelection(int direction)
    {
        Transform contentTransform = scrollRect.content;
        int itemCount = contentTransform.childCount;

        // ���݂̑I�����ڂ��I����Ԃɂ���
        Transform selectedItem = contentTransform.GetChild(selectedIndex);
        Image selectedImage = selectedItem.Find("icon").GetComponent<Image>();
        selectedImage.color = initialIconColor;

        // �V�����I�����ڂ��X�V
        selectedIndex += direction;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, itemCount - 1);

        // �V�����I�����ڂ�I����Ԃɂ���
        Transform newSelectedItem = contentTransform.GetChild(selectedIndex);
        Image newSelectedImage = newSelectedItem.Find("icon").GetComponent<Image>();
        newSelectedImage.color = Color.green;

    }


    // �|�[�Y��ʂɂ����錻�ݑI������Ă��鍀�ڂ̃C���f�b�N�X���擾����֐�
    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    public int GetMemberIndex()
    {
        return memberIndex;
    }



}