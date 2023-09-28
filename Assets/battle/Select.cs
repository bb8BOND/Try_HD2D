using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    public GameObject nowIndicator;  // 現在選択しているコマンドを示すオブジェクト
    public GameObject[] commandButtons;  // コマンドのボタン配列
    private int currentIndex = 0;    // 現在の選択インデックス

    private bool isInputEnabled = true;  // 入力の有効化フラグ

    // 現在選択しているコマンドを知るための公開変数
    public static string currentCommand;

    private float offsetFromButton = -80f; // 水平方向のオフセットを調整する値
    private float verticalOffsetFromButton = -35f; // 縦方向のオフセットを調整する値

    private void Start()
    {
        // 初期状態で最初のコマンドを選択していることを示す
        SetNowIndicatorPosition();
        currentCommand = GetCommandName(currentIndex); // 初期選択コマンドを取得

        // コルーチンを開始：「now」オブジェクトの点滅
        StartCoroutine(BlinkNowIndicator());

        currentCommand = GetCommandName(currentIndex);// 最初にAttackコマンドを選択
    }

    private void Update()
    {
        // 入力が有効かつ上方向キーが押された場合
        if (isInputEnabled && Input.GetAxis("Vertical") > 0)
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = commandButtons.Length - 1;

            SetNowIndicatorPosition();
            currentCommand = GetCommandName(currentIndex); // 選択コマンドを更新
            DisableInputForDuration();  // 入力を一時的に無効化
        }
        // 入力が有効かつ下方向キーが押された場合
        else if (isInputEnabled && Input.GetAxis("Vertical") < 0)
        {
            currentIndex++;
            if (currentIndex >= commandButtons.Length)
                currentIndex = 0;

            SetNowIndicatorPosition();
            currentCommand = GetCommandName(currentIndex); // 選択コマンドを更新
            DisableInputForDuration();  // 入力を一時的に無効化
        }

        //コマンドの選択決定
        //if (Input.GetKeyDown(KeyCode.P))
        //{
          //  if(currentCommand == "Attack" || currentCommand =="Escape")
            //{
              //  Debug.Log("osaretayo");
            //}
        //}
    }

    // 現在選択しているコマンドの位置にnowIndicatorを配置する
    private void SetNowIndicatorPosition()
    {
        nowIndicator.transform.SetParent(commandButtons[currentIndex].transform, false);
        nowIndicator.transform.localPosition = new Vector3(offsetFromButton, verticalOffsetFromButton, 0f); // オフセット値を使用して位置を調整します
    }


    // コマンドのインデックスからコマンド名を取得する
    private string GetCommandName(int index)
    {
        if (index >= 0 && index < commandButtons.Length)
        {
            return commandButtons[index].name;
        }
        return string.Empty;
    }

    // 入力を一時的に無効化する
    private void DisableInputForDuration()
    {
        isInputEnabled = false;
        StartCoroutine(EnableInputAfterDelay());
    }

    // 一定の待ち時間後に入力を有効化する
    private System.Collections.IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(0.4f);  // 適切な待ち時間を設定する
        isInputEnabled = true;
    }

    // 「now」オブジェクトの点滅するコルーチン
    private System.Collections.IEnumerator BlinkNowIndicator()
    {
        Image nowIndicatorImage = nowIndicator.GetComponent<Image>();

        // 初期状態：透明度を1にして非表示
        nowIndicatorImage.color = new Color(1f, 1f, 1f, 0f);

        while (true)
        {
            // 透明度をゆっくりと上げる
            for (float alpha = 0f; alpha <= 1f; alpha += 0.05f)
            {
                nowIndicatorImage.color = new Color(1f, 1f, 1f, alpha);
                yield return new WaitForSeconds(0.02f); // 適切な待ち時間を設定する
            }

            // 透明度をゆっくりと下げる
            for (float alpha = 1f; alpha >= 0f; alpha -= 0.05f)
            {
                nowIndicatorImage.color = new Color(1f, 1f, 1f, alpha);
                yield return new WaitForSeconds(0.02f); // 適切な待ち時間を設定する
            }
        }
    }

}
