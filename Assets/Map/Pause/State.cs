using UnityEngine;
using UnityEngine.UI;
public class State : MonoBehaviour
{
    public RawImage nowcharacter;//���ݑI������Ă���L������\������RawImage�I�u�W�F�N�g
    public ScrollView scrollview;//scrollView�X�N���v�g�ւ̎Q�Ɨp

    //���ݑI������Ă��郁���o�[��\��gameobject
    public GameObject nowmember1;
    public GameObject nowmember2;
    public GameObject nowmember3;
    //�eicon�̃R���|�[�l���g
    private Image icon_1_component;
    private Image icon_2_component;
    private Image icon_3_component;
    //�eRawImage�̃R���|�[�l���g
    private RawImage rawimage_1_component;
    private RawImage rawimage_2_component;
    private RawImage rawimage_3_component;
    // icon�̏����F
    private Color initialIconColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);
    // ���肳�ꂽ�L�����̉摜�̃p�X��ۑ�����
    public string decidedchara_path;
    void Start()
    {
        // ScrollView�X�N���v�g�ւ̎Q�Ƃ��擾
        scrollview = FindObjectOfType<ScrollView>();

        //nowmember�̎q�I�u�W�F�N�gicon(�w�i�F)�Ƃ���Image�R���|�[�l���g�擾
        var icon_1 = nowmember1.transform.Find("icon");
        var icon_2 = nowmember2.transform.Find("icon");
        var icon_3 = nowmember3.transform.Find("icon");
        icon_1_component = icon_1.GetComponent<Image>();
        icon_2_component = icon_2.GetComponent<Image>();
        icon_3_component = icon_3.GetComponent<Image>();
        //nowmember�I�u�W�F�N�g�ŁAmember1��I����Ԃɂ���B
        icon_1_component.color = Color.green;

        //nowmember�̑��I�u�W�F�N�gRawImage(�摜)�Ƃ���RawImage�R���|�[�l���g�擾
        var rawimage_1 = icon_1.transform.Find("RawImage");
        var rawimage_2 = icon_2.transform.Find("RawImage");
        var rawimage_3 = icon_3.transform.Find("RawImage");
        rawimage_1_component = rawimage_1.GetComponent<RawImage>();
        rawimage_2_component = rawimage_2.GetComponent<RawImage>();
        rawimage_3_component = rawimage_3.GetComponent<RawImage>();
    }

    void Update()
    {
        Nowchara_state();
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            Nowmember_changecolor(scrollview.memberIndex);
        }
        Change_nowcharaImage(scrollview.memberIndex);
    }

    //���ݑI������Ă���L�����N�^�[�̉摜���X�V
    void Nowchara_state()
    {
        var nowcharaIndex = scrollview.GetSelectedIndex();//scrollview��selectedIndex��nowcharaIndex�֑��
        Debug.Log("nowcharaIndex:"+nowcharaIndex);
        //���ݑI������Ă���摜��path���쐬
        var nowchara_path = scrollview.pngFiles[nowcharaIndex - 1];
        // PNG�t�@�C�����o�C�g�f�[�^�Ƃ��ēǂݍ���
        var newfileData = System.IO.File.ReadAllBytes(nowchara_path);
        // Texture2D���쐬���APNG�f�[�^��ǂݍ���
        var texture = new Texture2D(2, 2);
        texture.LoadImage(newfileData);
        // RawImage�R���|�[�l���g��texture�ɐݒ�
        nowcharacter.texture = texture;
    }

    //���ݑI������Ă���nowmember�I�u�W�F�N�g(�̔w�i�H)���X�V
    void Nowmember_changecolor(int nowmemberIndex)
    {
        if (nowmemberIndex == 0)
        {
            icon_1_component.color = Color.green;
            icon_2_component.color = initialIconColor;
            icon_3_component.color = initialIconColor;
        }
        else if (nowmemberIndex == 1)
        {
            icon_1_component.color = initialIconColor;
            icon_2_component.color = Color.green;
            icon_3_component.color = initialIconColor;
        }
        else if (nowmemberIndex == 2)
        {
            icon_1_component.color = initialIconColor;
            icon_2_component.color = initialIconColor;
            icon_3_component.color = Color.green;
        }
        
    }

    void Change_nowcharaImage(int nowmemberIndex)
    {
        if (nowmemberIndex == 0)
        {
            decidedchara_path = scrollview.pngFiles[EnemyManager.member1 - 1];//member1�Ɍ��肳�ꂽ�̉摜�p�X���擾 
        }
        else if (nowmemberIndex == 1)
        {
            decidedchara_path = scrollview.pngFiles[EnemyManager.member2 - 1];//member2�Ɍ��肳�ꂽ�摜�̃p�X���擾
        }
        else if (nowmemberIndex == 2)
        {
            decidedchara_path = scrollview.pngFiles[EnemyManager.member3 - 1];//member3�Ɍ��肳�ꂽ�摜�̃p�X���擾
        }
        var newfiledata = System.IO.File.ReadAllBytes(decidedchara_path);
        var texture = new Texture2D(2, 2);
        texture.LoadImage(newfiledata);
        if (nowmemberIndex == 0)
        {
            rawimage_1_component.texture = texture;
        }
        else if (nowmemberIndex == 1)
        {
            rawimage_2_component.texture = texture;
        }
        else if (nowmemberIndex == 2)
        {
            rawimage_3_component.texture = texture;
        }
    }
}
