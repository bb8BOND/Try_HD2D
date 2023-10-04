using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Woman : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer spRenderer;
    private bool wasd;

    public static string collidedEnemyName; // 衝突した敵の名前を保持する変数
    public static string enemy_explain;  //敵の説明
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
        // EnemyManager スクリプトの参照を取得
        enemyManager = FindFirstObjectByType<EnemyManager>();

        if (!enemyManager.isPaused && !stop)
        {
            // ゲームが一時停止中でなければ主人公の移動処理を行う
            HandleCharacterMovement();

            // 主人公のアニメーションを再生
            _anim.enabled = true;
        }
        else
        {
            // ゲームが一時停止中は主人公のアニメーションを停止する
            _anim.SetBool("Run", false);
            _anim.enabled = false;
        }
    }

    // 主人公の移動を制御するメソッド
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
            collidedEnemyName = "GNCT"; // 衝突した敵の名前を変数に格納
            enemy_explain = "岐阜高専の旧略称。正式名称を覚えていたら、かっこいいかも!?2014年まで使用されていた名称だよー。";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
        else if (collision.gameObject.CompareTag("TurtleShellPBR"))
        {
            collidedEnemyName = "NITGC"; // 衝突した敵の名前を変数に格納
            enemy_explain = "岐阜高専の略称。これを知っていると周りから一目置かれる!?でも実際に使われている所はあまり見たことないね。";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }else if (collision.gameObject.CompareTag("Dragon_1"))
        {
            collidedEnemyName = "D科"; // 衝突した敵の名前を変数に格納
            enemy_explain = "電気電子工学科の略称。電気情報工学科と似ているけど、どちらかというと、回路などの制御方法を学んでいるんだよ。";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
        }
        else if (collision.gameObject.CompareTag("Dragon_2"))
        {
            collidedEnemyName = "C科"; // 衝突した敵の名前を変数に格納
            enemy_explain = "環境都市工学科の略称。土木や力学など、広い学問を学んでいるよ!男女の比率は.ほぼ5:5!!";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
        }
        else if (collision.gameObject.CompareTag("Dragon_3"))
        {
            collidedEnemyName = "E科"; // 衝突した敵の名前を変数に格納
            enemy_explain = "電気情報工学科の略称。回路やパソコンを用いた実験をたくさん行っているぞ!これを制作した人もE科だよ!!";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
        else if (collision.gameObject.CompareTag("Dragon_4"))
        {
            collidedEnemyName = "A科"; // 衝突した敵の名前を変数に格納
            enemy_explain = "建築工学科の略称。他の高専にもめったにない、めずしい学科だよ!男女の比率がほぼ5:5!!";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
        else if (collision.gameObject.CompareTag("Dragon_5"))
        {
            collidedEnemyName = "M科"; // 衝突した敵の名前を変数に格納
            enemy_explain = "機械工学科の略称!!機械の設計や制御を学ぶ学科!!機械好きがたくさんいるとかいないとか・・・";
            Initiate.Fade("Scene1_battle", Color.black, 1.0f);
            stop = true;
            //SceneManager.LoadScene("Scene1_battle");
        }
    }
}
