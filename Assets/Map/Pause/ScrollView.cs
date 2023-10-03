using System;
using System.Collections.Generic;
using System.IO;
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

    private EnemyManager enemyManager; // �|�[�Y��ʂ��Ǘ�����X�N���v�g�ւ̎Q��

    public GameObject member1;
    public GameObject member2;
    public GameObject member3;
    public GameObject[] members;
    public int memberIndex { get; private set; } = 0;

    string folderPath = "C:/Users/81802/unity/chara"; // �����������t�H���_�̃p�X
    List<string> pngFiles = new List<string>();// �w�肳�ꂽ�t�H���_����PNG�t�@�C���̃p�X���i�[���郊�X�g
    public GameObject characterObject_original;

    void Start()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>(); // EnemyManager�X�N���v�g�ւ̎Q�Ƃ��擾

        var result = Searchfile();
        var pngCount = result.Item1; // 1�Ԗڂ̕Ԃ�l(�t�@�C���̑���)���擾
        List<string> pngFiles = result.Item2; // 2�Ԗڂ̕Ԃ�l(�t�@�C���̃p�X�̃��X�g)���擾
        scrollStep = 1f / pngCount;
        for (var i = 0; i < pngCount; i++)
        {
            var characterObject = Creat_copy(characterObject_original);
            // ���I�u�W�F�N�g���擾
            var childTransform = characterObject.transform.Find("icon");
            var grandchildTransform = childTransform.transform.Find("RawImage");
            // RawImage�R���|�[�l���g���擾
            var rawImageComponent = grandchildTransform.GetComponent<RawImage>();
            // PNG�t�@�C�����o�C�g�f�[�^�Ƃ��ēǂݍ���
            var fileData = System.IO.File.ReadAllBytes(pngFiles[i]);
            // Texture2D���쐬���APNG�f�[�^��ǂݍ���
            var texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            // RawImage�R���|�[�l���g��texture�ɐݒ肵�܂��B
            rawImageComponent.texture = texture;
        }

        // ������Ԃōŏ��̍��ڂ�I����ԂƂ���
        UpdateSelection(selectedIndex);
        if(!enemyManager.isaddmember){
            member1.SetActive(true);
        }

        // �ݒ肳�ꂽ�X�e�b�v�T�C�Y�Ɋ�Â��ăR���e���c�̍������v�Z
        var contentRect = scrollRect.content.GetComponent<RectTransform>();
        var contentHeight = contentRect.sizeDelta.y;
        var stepHeight =  scrollStep * contentHeight;
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
        var contentTransform = scrollRect.content;
        var itemCount = contentTransform.childCount;

        // ���݂̑I�����ڂ��I����Ԃɂ���
        var selectedItem = contentTransform.GetChild(selectedIndex);
        var selectedImage = selectedItem.Find("icon").GetComponent<Image>();
        selectedImage.color = initialIconColor;

        // �V�����I�����ڂ��X�V
        selectedIndex += direction;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, itemCount - 1);

        // �V�����I�����ڂ�I����Ԃɂ���
        var newSelectedItem = contentTransform.GetChild(selectedIndex);
        var newSelectedImage = newSelectedItem.Find("icon").GetComponent<Image>();
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

    // �w�肳�ꂽ�t�H���_����PNG�t�@�C����T��
    public Tuple<int, List<string>> Searchfile()
    {
        var pngCount = 0;

        // �t�H���_�����݂��Ȃ��ꍇ�̓G���[��\�����ďI��
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("�w�肳�ꂽ�t�H���_�����݂��܂���: " + folderPath);
            return new Tuple<int, List<string>>(0, new List<string>());
        }

        // �t�H���_���̃t�@�C�����擾���APNG�t�@�C�����J�E���g����у��X�g�ɒǉ�
        var files = Directory.GetFiles(folderPath);
        foreach (var file in files)
        {
            if (file.ToLower().EndsWith(".png"))
            {
                pngCount++;
                pngFiles.Add(file); // ���X�g��PNG�t�@�C���̃p�X��ǉ�
            }
        }

        // ���X�g�Ɋi�[���ꂽPNG�t�@�C���𐔒l���Ƀ\�[�g
        SortPngFiles();

        // ���X�g�Ɋi�[���ꂽPNG�t�@�C���̃p�X��\���i�f�o�b�O�p�j
        foreach (var pngFilePath in pngFiles)
        {
            Debug.Log("PNG�t�@�C���̃p�X: " + pngFilePath);
        }

        return new Tuple<int, List<string>>(pngCount, pngFiles);
    }

    //�����o�[�I�u�W�F�N�g�̕����֐�
    GameObject Creat_copy(GameObject original)
    {
        if (original == null)
        {
            Debug.LogWarning("Original GameObject is null. Cannot clone.");
            return null;
        }

        // �I�u�W�F�N�g�𕡐����A�V�����C���X�^���X��Ԃ�
        var clonedObject = Instantiate(original, transform.position, transform.rotation);
        // �������ꂽ�I�u�W�F�N�g�̐e���I���W�i���Ɠ����ɐݒ肷��
        if (original.transform.parent != null)
        {
            clonedObject.transform.SetParent(original.transform.parent);
        }
        return clonedObject;
    }

    // ���X�g�Ɋi�[���ꂽPNG�t�@�C���𐔒l���Ƀ\�[�g����֐�
    public void SortPngFiles()
    {
        pngFiles.Sort((a, b) =>
        {
            string fileNameA = Path.GetFileNameWithoutExtension(a);
            string fileNameB = Path.GetFileNameWithoutExtension(b);

            int numberA, numberB;
            bool successA = int.TryParse(fileNameA, out numberA);
            bool successB = int.TryParse(fileNameB, out numberB);

            // ���l�ɕϊ��ł���ꍇ�A���l�Ŕ�r
            if (successA && successB)
            {
                return numberA.CompareTo(numberB);
            }
            // ���l�ɕϊ��ł��Ȃ��ꍇ�A������Ƃ��Ĕ�r
            else
            {
                return fileNameA.CompareTo(fileNameB);
            }
        });
    }


}