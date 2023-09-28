using UnityEngine;
using UnityEngine.UI;


public class ScrollView : MonoBehaviour
{
    public ScrollRect scrollRect;
    //�X�N���[���̑��x�ݒ�(�`�[�������o�[�����vn�l������1/nf�Őݒ肷��)
    float scrollStep = 1/25f;

    // ���ݑI������Ă��鍀�ڂ̃C���f�b�N�X
    public int selectedIndex { get; private set; } = 0;
    //���ݑI������Ă��鍀�ڂ̉摜
    public Transform newSelectedItem;

    // icon�̏����F
    private Color initialIconColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);

    private enemy_manager enemyManager; // �|�[�Y��ʂ��Ǘ�����X�N���v�g�ւ̎Q��

    public GameObject member1;
    public GameObject member2;
    public GameObject member3;
    public GameObject[] members;
    public int memberIndex { get; private set; } = 0;

    void Start()
    {
        enemyManager = FindObjectOfType<enemy_manager>(); // enemy_manager�X�N���v�g�ւ̎Q�Ƃ��擾

        // ������Ԃōŏ��̍��ڂ�I����ԂƂ���
        UpdateSelection(selectedIndex);
        if(!enemyManager.isaddmember){
            member1.SetActive(true);
        }
        Debug.Log("a");

        // �ݒ肳�ꂽ�X�e�b�v�T�C�Y�Ɋ�Â��ăR���e���c�̍������v�Z
        RectTransform contentRect = scrollRect.content.GetComponent<RectTransform>();
        float contentHeight = contentRect.sizeDelta.y;
        float stepHeight =  scrollStep * contentHeight;
        scrollRect.content.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight + stepHeight);
    }

    void Update()
    {
        if (enemyManager.iseditteam || enemyManager.isaddmember) // �|�[�Y��ʂ��\������Ă��鎞�̂݃X�N���[��������
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
            }else if (Input.GetKeyDown(KeyCode.A) && !enemyManager.isaddmember)
            {
                Updatemember(-1);
            }else if (Input.GetKeyDown(KeyCode.D) && !enemyManager.isaddmember)
            {
                Updatemember(1);
            }
        }
    }

    
    // �X�N���[���A�b�v����
    void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition + scrollStep;
    }

    // �X�N���[���_�E������
    void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition - scrollStep;
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


    //�I�������o�[�̍X�V
    void Updatemember(int change)
    {
        memberIndex = Mathf.Clamp(memberIndex + change, 0, 2); // memberIndex��0����2�͈̔͂ɐ���
        membershow(); // �����o�[�I�u�W�F�N�g�̕\�����X�V����
    }


    //�I�������o�[�̕\��
    void membershow()
    {
        if (memberIndex == 0)
        {
            member1.SetActive(true);
            member2.SetActive(false);
            member3.SetActive(false);
        }
        else if (memberIndex == 1)
        {
            member1.SetActive(false);
            member2.SetActive(true);
            member3.SetActive(false);
        }
        else if (memberIndex == 2)
        {
            member1.SetActive(false);
            member2.SetActive(false);
            member3.SetActive(true);
        }
    }

    // �|�[�Y��ʂɂ����錻�ݑI������Ă��鍀�ڂ̃C���f�b�N�X���擾����֐�
    public int  GetSelectedIndex()
    {
        return selectedIndex;
    }

    public int GetMemberIndex()
    {
        return memberIndex;
    }

    

}