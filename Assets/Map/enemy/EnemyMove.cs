using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float[] speedOptions = { 0f, 2f, 4f }; // 移動速度のオプション

    public float minDuration = 1f; // 移動の最小時間
    public float maxDuration = 5f; // 移動の最大時間

    private float timer; // タイマー
    private float movementDuration; // 移動時間
    private Vector3 movementDirection; // 移動方向
    private float movementSpeed; // 移動速度

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
            // オブジェクトの向きを移動方向に合わせる
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movement.normalized);
            }
        }
    }

    void SetNewMovementParameters()
    {
        movementDuration = Random.Range(minDuration, maxDuration); // 新しい移動時間を設定
        movementDirection = Random.insideUnitSphere; // ランダムな移動方向を設定
        movementSpeed = speedOptions[Random.Range(0, speedOptions.Length)]; // ランダムな移動速度を設定
        timer = 0f; // タイマーをリセット
    }
}
