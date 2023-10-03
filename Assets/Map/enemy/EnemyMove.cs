using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float[] speedOptions = { 0f, 2f, 4f }; // �ړ����x�̃I�v�V����

    public float minDuration = 1f; // �ړ��̍ŏ�����
    public float maxDuration = 5f; // �ړ��̍ő厞��

    private float timer; // �^�C�}�[
    private float movementDuration; // �ړ�����
    private Vector3 movementDirection; // �ړ�����
    private float movementSpeed; // �ړ����x

    private Rigidbody rb;

    private Animator _anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewMovementParameters();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= movementDuration)
        {
            SetNewMovementParameters();
        }

        var movement = movementDirection * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        if (movementSpeed == 0f)
        {
            _anim.SetBool("Walk", false);
        }
        else
        {
            _anim.SetBool("Walk", true);
            // �I�u�W�F�N�g�̌������ړ������ɍ��킹��
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movement.normalized);
            }
        }
    }

    void SetNewMovementParameters()
    {
        movementDuration = Random.Range(minDuration, maxDuration); // �V�����ړ����Ԃ�ݒ�
        movementDirection = Random.insideUnitSphere; // �����_���Ȉړ�������ݒ�
        movementSpeed = speedOptions[Random.Range(0, speedOptions.Length)]; // �����_���Ȉړ����x��ݒ�
        timer = 0f; // �^�C�}�[�����Z�b�g
    }
}
