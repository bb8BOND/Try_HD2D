using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject SlimePBR;
    public GameObject TurtleShellPBR;
    public GameObject Dragon_1;
    public GameObject Dragon_2;
    public GameObject Dragon_3;
    public GameObject Dragon_4;
    public GameObject Dragon_5;

    public Transform SlimePBR_place;
    public Transform TurtleShellPBR_place;
    public Transform Dragon_1_place;
    public Transform Dragon_2_place;
    public Transform Dragon_3_place;
    public Transform Dragon_4_place;
    public Transform Dragon_5_place;

    float TimeCount;
    public bool isPaused { get; private set; } // �|�[�Y��ʂ̃t���O
    public float focusDistance = 1f;
    public GameObject postProcessVolumeObject;
    // Post Process Volume�R���|�[�l���g���i�[����ϐ�
    private PostProcessVolume postProcessVolume;
    // P�������ꂽ���ǂ�����ۑ�����ϐ�
    private bool isPKeyPressed = false;

    public GameObject nowIndicator; // "now"�������I�u�W�F�N�g
    public GameObject optionsPanel; // �I�����̐e�p�l��
    private bool isInputEnabled = true; // ���̗͂L�����t���O
    private bool onselect = false;//�I���R�}���h�̈ړ����Ǘ�
    private int selectedIndex = 0;      // �I������Ă��鍀�ڂ̃C���f�b�N�X

    public GameObject canvasObject; // �V���A���C�Y�t�B�[���h���g�p���āA�C���X�y�N�^�[���Canvas�I�u�W�F�N�g���w��
    public GameObject titlecheck;
    public GameObject ScrollView;
    public bool iseditteam = false;
    public bool isaddmember = false;
    public GameObject State;

    [SerializeField]private Fade fade;

    static public int member1 = 0;
    static public int member2 = 1;
    static public int member3 = 2;
    static public int addmem;

    public GameObject Addmem;
    

    // Phase���`
    private enum Phase
    {
        Selectoption,
        back,
        editteam,
        addmember,
        title,
        End
    }

    private Phase currentPhase = Phase.Selectoption;

    void Start()
    {
        fade.FadeOut(1f);
        TimeCount = 0f; // TimeCount�̏�����
        // Post Process Volume�R���|�[�l���g���擾���܂�
        postProcessVolume = postProcessVolumeObject.GetComponent<PostProcessVolume>();

        // ������Ԃōŏ��̍��ڂ�I����ԂƂ���
        SelectOption(0);




        Instantiate(SlimePBR, SlimePBR_place.position, Quaternion.identity);
        Instantiate(TurtleShellPBR, TurtleShellPBR_place.position, Quaternion.identity); �@�@�@//�v��������������������������������������������������������
        Instantiate(Dragon_1, Dragon_1_place.position, Quaternion.identity);
        Instantiate(Dragon_2, Dragon_2_place.position, Quaternion.identity);
        Instantiate(Dragon_3, Dragon_3_place.position, Quaternion.identity);
        Instantiate(Dragon_4, Dragon_4_place.position, Quaternion.identity);
        Instantiate(Dragon_5, Dragon_5_place.position, Quaternion.identity);

    }

    void Update()
    {
        if (!isPaused)
    {
        //�^�u���G���������̃R�[�h
        //TimeCount += Time.deltaTime;
        //if (TimeCount > 10)
        //{
        //    Instantiate(SlimePBR, SlimePBR_place.position, Quaternion.identity);
        //    Instantiate(TurtleShellPBR, TurtleShellPBR_place.position, Quaternion.identity);          ������ӂœK����10f���炢
        //    TimeCount = 0f; // TimeCount�̃��Z�b�g
        //}
    }
    else if (isPaused)
    {
            if (!onselect)
            {// ���͂�����ΑI�����ڂ�ύX
                if (Input.GetKeyDown(KeyCode.W)) // Input.GetAxis("Vertical") > 0
                {
                    if (selectedIndex > 0)
                    {
                        SelectOption(selectedIndex - 1);
                    }
                    Debug.Log("���ݑI������Ă��鍀�ځF" + optionsPanel.transform.GetChild(selectedIndex).name);
                }
                else if (Input.GetKeyDown(KeyCode.S)) // Input.GetAxis("Vertical") < 0
                {
                    if (selectedIndex < optionsPanel.transform.childCount - 1)
                    {
                        SelectOption(selectedIndex + 1);
                    }
                    Debug.Log("���ݑI������Ă��鍀�ځF" + optionsPanel.transform.GetChild(selectedIndex).name);
                }
            }
            
    }

        if (Input.GetKeyDown(KeyCode.O) && !isPKeyPressed)
        {
            isPKeyPressed = true;
            focusDistance = 1f;
            UpdateFocusDistance();//�ڂ���
            canvasObject.SetActive(true);
            nowIndicator.transform.position = new Vector3(1743, 305, 0);
            StartCoroutine(PhaseCoroutine());//�R���[�`�����s
            TogglePause();
        }
        
    }

    // �Q�[���̈ꎞ��~��؂�ւ���֐�
    void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }

    // �Q�[�����ꎞ��~����֐�
    void Pause()
    {
        Time.timeScale = 0f; // �Q�[���̎��Ԃ��~
        isPaused = true;
        Debug.Log("�Q�[�����ꎞ��~���܂���");
    }

    // �Q�[�����ĊJ����֐�
    void ResumeGame()
    {
        Time.timeScale = 1f; // �Q�[���̎��Ԃ�ʏ�ɖ߂�
        isPaused = false;
        Debug.Log("�Q�[�����ĊJ����܂���");
    }

    // �|�[�Y��ʂ��ڂ����֐�
    private void UpdateFocusDistance()
    {
        // Post Process Volume�̐ݒ���擾���܂�
        var volumeSettings = postProcessVolume.sharedProfile;

        // DepthOfField�R���|�[�l���g���擾���܂�
        DepthOfField dof;
        if (volumeSettings.TryGetSettings(out dof))
        {
            // Focus Distance�̒l��ݒ肵�܂�
            dof.focusDistance.value = focusDistance; // float�^�ɏC��
        }
    }

    // �I�����ꂽ���ڂ��X�V���郁�\�b�h
    private void SelectOption(int index)
    {
        // �I�����ꂽ���ڂ̃C���f�b�N�X�Ɋ�Â��āA�����F��ݒ肷��
        for (int i = 0; i < 4; i++)
        {
            Transform optionTransform = optionsPanel.transform.GetChild(i);
            TextMeshProUGUI optionText = optionTransform.GetComponent<TextMeshProUGUI>();
            if (optionText != null)
            {
                if (i == index)
                {
                    // �I������Ă��鍀�ڂ̕����F��ύX
                    optionText.color = new Color(1f, 1f, 1f); // ���F
                }
                else
                {
                    // �I������Ă��Ȃ����ڂ̕����F�����ɖ߂�
                    optionText.color = new Color(130f/255f , 130f/255f , 130f/255f);
                }
            }
        }

        selectedIndex = index;

        string optionName = GetOptionName(selectedIndex);

        // "back"�������ꍇ�̏���
        if (optionName == "back")
        {
            nowIndicator.transform.position = new Vector3(1743, 305, 0);
        }else if(optionName == "editteam")
        {
            nowIndicator.transform.position = new Vector3(1743, 235, 0);
        }else if(optionName == "addmember")
        {
            nowIndicator.transform.position = new Vector3(1743, 165, 0);
        }else if(optionName == "title")
        {
            nowIndicator.transform.position = new Vector3(1743, 95, 0);
        }
    }

    // ���ݑI������Ă��鍀�ڂ̖��O���擾���郁�\�b�h
    private string GetOptionName(int index)
    {
        return optionsPanel.transform.GetChild(index).name;
    }

    // ���͂��ꎞ�I�ɖ���������
    private void DisableInputForDuration()
    {
        isInputEnabled = false;
        StartCoroutine(EnableInputAfterDelay());
    }

    // ���̑҂����Ԍ�ɓ��͂�L��������
    private System.Collections.IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(0.15f);  // �K�؂ȑ҂����Ԃ�ݒ肷��
        isInputEnabled = true;
    }

    // �R���[�`���̎��s
    private IEnumerator PhaseCoroutine()
    {
        while (currentPhase != Phase.End)
        {
            switch (currentPhase)
            {
                case Phase.Selectoption:
                    Debug.Log("Selectoption Phase�����s����܂���");
                    onselect = false;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U));
                    if(GetOptionName(selectedIndex) == "back")
                    {
                        onselect = true;
                        currentPhase = Phase.back;
                    }else if(GetOptionName(selectedIndex) == "editteam")
                    {
                        onselect = true;
                        currentPhase = Phase.editteam;
                    }
                    else if(GetOptionName(selectedIndex) == "addmember")
                    {
                        onselect = true;
                        currentPhase = Phase.addmember;
                    }
                    else if(GetOptionName(selectedIndex) == "title")
                    {
                        onselect = true;
                        //fade.FadeIn(1f, () => SceneManager.LoadScene("empty"));
                        currentPhase = Phase.title;
                    }
                    
                    break;
                case Phase.back:
                    Debug.Log("back Phase�����s����܂���");
                    
                    isPKeyPressed = false;
                    TogglePause();
                    focusDistance = 10f;
                    UpdateFocusDistance();//�ڂ�������
                    canvasObject.SetActive(false);
                    currentPhase = Phase.End;
                    break;
                case Phase.editteam:
                    Debug.Log("editteam Phase�����s����܂���");
                    ScrollView .SetActive(true);
                    //State.SetActive(true);
                    iseditteam = true;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I));
                    Debug.Log("�I������܂����B");

                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        ScrollView .SetActive(false);
                        iseditteam = false;
                        currentPhase = Phase.Selectoption;
                    }
                    else if (Input.GetKeyDown(KeyCode.U))
                    {   // ScrollView�̎Q�Ƃ��擾
                        ScrollView scrollView = ScrollView.GetComponent<ScrollView>();

                        if (scrollView.GetMemberIndex() == 0)
                        {
                            member1 = scrollView.GetSelectedIndex();
                            Debug.Log($"member1��{member1}�Ɍ���I");

                        } else if (scrollView.GetMemberIndex() == 1)
                        {
                            member2 = scrollView.GetSelectedIndex();
                            Debug.Log($"member2��{member2}�Ɍ���I");

                        } else if (scrollView.GetMemberIndex() == 2)
                        {
                            member3 = scrollView.GetSelectedIndex();
                            Debug.Log($"member3��{member3}�Ɍ���I");
                        }
                    }
                    break;
                case Phase.addmember:
                    Debug.Log("addmember Phase�����s����܂���");

                    ScrollView.SetActive(true);//�ꗗ��\��
                    iseditteam = true;
                    isaddmember = true;
                    if(Input.GetKeyDown(KeyCode.I)){
                        ScrollView.SetActive(false);
                        iseditteam = false;
                        isaddmember = false;
                        currentPhase = Phase.Selectoption;
                    }else if(Input.GetKeyDown(KeyCode.U)){
                        // ScrollView�̎Q�Ƃ��擾
                        ScrollView scrollView = ScrollView.GetComponent<ScrollView>();
                        addmem = scrollView.GetSelectedIndex();
                        //addmem�ɂ͑I�����ꂽ�L������index�ԍ����ۑ�����Ă���B
                        //���̌�ɁA�I�����ꂽ�L�����N�^�[�̍U���͂Ƃ�hp�Ƃ����擾����R�[�h�������K�v���肢������������������������
                        //��ic�J�[�h�̂�Ƃ̘A�g���K�v���您����������������������������������������������������������������������


                        //battle_manager battleManager;
                        //GameObject obj = GameObject.Find("friends");
                        //battleManager = obj.GetComponent<battle_manager>();
                        //Addmem = battleManager.friends[addmem];
                    }
                    break;

                case Phase.title:
                    Debug.Log("title Phase�����s����܂���");
                    
                    titlecheck.SetActive(true);
                    // U��I�̂����ꂩ���������܂őҋ@
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I));

                    // �ǂ���̃L�[�������ꂽ������
                    if (Input.GetKeyDown(KeyCode.U))
                    {
                        isPKeyPressed = false;
                        TogglePause();
                        focusDistance = 10f;
                        UpdateFocusDistance();//�ڂ�������
                        canvasObject.SetActive(false);
                        
                        fade.FadeIn(1f, () => SceneManager.LoadScene("empty"));
                        yield return new WaitForSeconds(10f);
                        Debug.Log("�^�C�g���ɑJ�ڂ��Ⴀ������");
                        currentPhase = Phase.End;
                    }
                    else if (Input.GetKeyDown(KeyCode.I))
                    {
                        titlecheck.SetActive(false);
                        currentPhase = Phase.Selectoption;
                        break;
                    }
                    break;
                case Phase.End:
                    break;
            }

            // �I�����ڂ��ύX���ꂽ�ꍇ��nowIndicator�̈ʒu���X�V����
            SelectOption(selectedIndex);
            yield return null;
        }
        currentPhase = Phase.Selectoption;

        yield break;
    }

    IEnumerator titlescene()
    {
        //�ҋ@����
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("empty");
    }
}
