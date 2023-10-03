using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Woman : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer spRenderer;
    private bool wasd;

    public static string collidedEnemyName; // �Փ˂����G�̖��O��ێ�����ϐ�
    public static string enemy_explain;  //�G�̐���
    private EnemyManager enemyManager;
    private bool stop;

    void Start()
    {
        this._anim = GetComponent<Animator>();
        this.spRenderer = GetComponent<SpriteRenderer>();
        this.wasd = false;
        this.stop = false;
    }

    void Update()
    {
        // EnemyManager �X�N���v�g�̎Q�Ƃ��擾
        enemyManager = FindFirstObjectByType<EnemyManager>();

        if (!enemyManager.isPaused && !stop)
        {
            // �Q�[�����ꎞ��~���łȂ���Ύ�l���̈ړ��������s��
            HandleCharacterMovement();

            // ��l���̃A�j���[�V�������Đ�
            _anim.enabled = true;
        }
        else
        {
            // �Q�[�����ꎞ��~���͎�l���̃A�j���[�V�������~����
            _anim.SetBool("Run", false);
            _anim.enabled = false;
        }
    }

    // ��l���̈ړ��𐧌䂷�郁�\�b�h
    private void HandleCharacterMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, 0.3f);
            _anim.SetBool("Run", true);
            spRenderer.flipX = true;
            wasd = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, -0.3f);
            _anim.SetBool("Run", true);
            spRenderer.flipX = false;
            wasd = false;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.3f, 0, 0);
            _anim.SetBool("Run", true);
            spRenderer.flipX = wasd;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-0.3f, 0, 0);
            _anim.SetBool("Run", true);
            spRenderer.flipX = wasd;
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            _anim.SetBool("Run", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SlimePBR"))
        {
            collidedEnemyName = "GNCT"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "�򕌍���̋����́B�������̂��o���Ă�����A��������������!?2014�N�܂Ŏg�p����Ă������̂���[�B";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
        else if (collision.gameObject.CompareTag("TurtleShellPBR"))
        {
            collidedEnemyName = "NITGC"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "�򕌍���̗��́B�����m���Ă���Ǝ��肩���ڒu�����!?�ł����ۂɎg���Ă��鏊�͂��܂茩�����ƂȂ��ˁB";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }else if (collision.gameObject.CompareTag("Dragon_1"))
        {
            collidedEnemyName = "D��"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "�d�C�d�q�H�w�Ȃ̗��́B�d�C���H�w�ȂƎ��Ă��邯�ǁA�ǂ��炩�Ƃ����ƁA��H�Ȃǂ̐�����@���w��ł���񂾂�B";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
        }
        else if (collision.gameObject.CompareTag("Dragon_2"))
        {
            collidedEnemyName = "C��"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "���s�s�H�w�Ȃ̗��́B�y�؂�͊w�ȂǁA�L���w����w��ł����!�j���̔䗦��.�ق�5:5!!";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
        }
        else if (collision.gameObject.CompareTag("Dragon_3"))
        {
            collidedEnemyName = "E��"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "�d�C���H�w�Ȃ̗��́B��H��p�\�R����p������������������s���Ă��邼!����𐧍삵���l��E�Ȃ���!!";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
        else if (collision.gameObject.CompareTag("Dragon_4"))
        {
            collidedEnemyName = "A��"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "���z�H�w�Ȃ̗��́B���̍���ɂ��߂����ɂȂ��A�߂������w�Ȃ���!�j���̔䗦���ق�5:5!!";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
        else if (collision.gameObject.CompareTag("Dragon_5"))
        {
            collidedEnemyName = "M��"; // �Փ˂����G�̖��O��ϐ��Ɋi�[
            enemy_explain = "�@�B�H�w�Ȃ̗���!!�@�B�̐݌v�␧����w�Ԋw��!!�@�B�D�����������񂢂�Ƃ����Ȃ��Ƃ��E�E�E";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
    }
}
