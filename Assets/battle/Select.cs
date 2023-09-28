using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    public GameObject nowIndicator;  // ���ݑI�����Ă���R�}���h�������I�u�W�F�N�g
    public GameObject[] commandButtons;  // �R�}���h�̃{�^���z��
    private int currentIndex = 0;    // ���݂̑I���C���f�b�N�X

    private bool isInputEnabled = true;  // ���̗͂L�����t���O

    // ���ݑI�����Ă���R�}���h��m�邽�߂̌��J�ϐ�
    public static string currentCommand;

    private float offsetFromButton = -80f; // ���������̃I�t�Z�b�g�𒲐�����l
    private float verticalOffsetFromButton = -35f; // �c�����̃I�t�Z�b�g�𒲐�����l

    private void Start()
    {
        // ������Ԃōŏ��̃R�}���h��I�����Ă��邱�Ƃ�����
        SetNowIndicatorPosition();
        currentCommand = GetCommandName(currentIndex); // �����I���R�}���h���擾

        // �R���[�`�����J�n�F�unow�v�I�u�W�F�N�g�̓_��
        StartCoroutine(BlinkNowIndicator());

        currentCommand = GetCommandName(currentIndex);// �ŏ���Attack�R�}���h��I��
    }

    private void Update()
    {
        // ���͂��L����������L�[�������ꂽ�ꍇ
        if (isInputEnabled && Input.GetAxis("Vertical") > 0)
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = commandButtons.Length - 1;

            SetNowIndicatorPosition();
            currentCommand = GetCommandName(currentIndex); // �I���R�}���h���X�V
            DisableInputForDuration();  // ���͂��ꎞ�I�ɖ�����
        }
        // ���͂��L�����������L�[�������ꂽ�ꍇ
        else if (isInputEnabled && Input.GetAxis("Vertical") < 0)
        {
            currentIndex++;
            if (currentIndex >= commandButtons.Length)
                currentIndex = 0;

            SetNowIndicatorPosition();
            currentCommand = GetCommandName(currentIndex); // �I���R�}���h���X�V
            DisableInputForDuration();  // ���͂��ꎞ�I�ɖ�����
        }

        //�R�}���h�̑I������
        //if (Input.GetKeyDown(KeyCode.P))
        //{
          //  if(currentCommand == "Attack" || currentCommand =="Escape")
            //{
              //  Debug.Log("osaretayo");
            //}
        //}
    }

    // ���ݑI�����Ă���R�}���h�̈ʒu��nowIndicator��z�u����
    private void SetNowIndicatorPosition()
    {
        nowIndicator.transform.SetParent(commandButtons[currentIndex].transform, false);
        nowIndicator.transform.localPosition = new Vector3(offsetFromButton, verticalOffsetFromButton, 0f); // �I�t�Z�b�g�l���g�p���Ĉʒu�𒲐����܂�
    }


    // �R�}���h�̃C���f�b�N�X����R�}���h�����擾����
    private string GetCommandName(int index)
    {
        if (index >= 0 && index < commandButtons.Length)
        {
            return commandButtons[index].name;
        }
        return string.Empty;
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
        yield return new WaitForSeconds(0.4f);  // �K�؂ȑ҂����Ԃ�ݒ肷��
        isInputEnabled = true;
    }

    // �unow�v�I�u�W�F�N�g�̓_�ł���R���[�`��
    private System.Collections.IEnumerator BlinkNowIndicator()
    {
        Image nowIndicatorImage = nowIndicator.GetComponent<Image>();

        // ������ԁF�����x��1�ɂ��Ĕ�\��
        nowIndicatorImage.color = new Color(1f, 1f, 1f, 0f);

        while (true)
        {
            // �����x���������Əグ��
            for (float alpha = 0f; alpha <= 1f; alpha += 0.05f)
            {
                nowIndicatorImage.color = new Color(1f, 1f, 1f, alpha);
                yield return new WaitForSeconds(0.02f); // �K�؂ȑ҂����Ԃ�ݒ肷��
            }

            // �����x���������Ɖ�����
            for (float alpha = 1f; alpha >= 0f; alpha -= 0.05f)
            {
                nowIndicatorImage.color = new Color(1f, 1f, 1f, alpha);
                yield return new WaitForSeconds(0.02f); // �K�؂ȑ҂����Ԃ�ݒ肷��
            }
        }
    }

}
