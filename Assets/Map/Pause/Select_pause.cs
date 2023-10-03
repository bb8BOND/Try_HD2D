using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_pause : MonoBehaviour
{
    public GameObject nowIndicator; // "now"を示すオブジェクト
    public GameObject[] options;    // 選択肢のオブジェクト（back、editteam、titleの順番）

    private bool isInputEnabled = true; // 入力の有効化フラグ
    private int selectedIndex = 0;      // 選択されている項目のインデックス

    void Start()
    {
        // 初期状態で最初の項目を選択状態とする
        SelectOption(0);
    }

    // Update is called once per frame
    void Update()
    {
        // 入力が有効かつ上方向キーが押された場合
        if (isInputEnabled && Input.GetAxis("Vertical") > 0)
        {
            if (selectedIndex > 0)
            {
                SelectOption(selectedIndex - 1);
            }
        }
        // 入力が有効かつ下方向キーが押された場合
        else if (isInputEnabled && Input.GetAxis("Vertical") < 0)
        {
            if (selectedIndex < options.Length - 1)
            {
                SelectOption(selectedIndex + 1);
            }
        }
    }

    // 選択された項目を更新するメソッド
    private void SelectOption(int index)
    {
        selectedIndex = index;

        // 選択されている項目の位置に"now"オブジェクトを移動させる
        var targetPosition = options[selectedIndex].transform.position;
        nowIndicator.transform.position = new Vector3(targetPosition.x, targetPosition.y, nowIndicator.transform.position.z);
    }
}
