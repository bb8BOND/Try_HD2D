using UnityEngine;
using UnityEngine.UI;


public class ScrollView2 : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollStep = 0.1f; // スクロールのステップサイズを設定

    // 現在選択されている項目のインデックス
    public int selectedIndex { get; private set; } = 0;
    //現在選択されている項目の画像
    public Transform newSelectedItem;

    // iconの初期色
    private Color initialIconColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);

    private enemy_manager enemyManager; // ポーズ画面を管理するスクリプトへの参照

    public GameObject[] members;
    public int memberIndex { get; private set; } = 0;

    void Start()
    {
        enemyManager = FindObjectOfType<enemy_manager>(); // enemy_managerスクリプトへの参照を取得

        // 初期状態で最初の項目を選択状態とする
        UpdateSelection(selectedIndex);
        
    }

    void Update()
    {
        if (enemyManager.iseditteam) // ポーズ画面が表示されている時のみスクロールを処理
        {
            // wasdキーの押下を検知してスクロール処理と選択項目の更新を実行する
            if (Input.GetKeyDown(KeyCode.W))
            {
                ScrollUp();
                UpdateSelection(-1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ScrollDown();
                UpdateSelection(1);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                
            }
        }
    }

    // スクロールアップ処理
    void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition += scrollStep;
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
    }

    // スクロールダウン処理
    void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition -= scrollStep;
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
    }

    // 選択項目の更新
    void UpdateSelection(int direction)
    {
        Transform contentTransform = scrollRect.content;
        int itemCount = contentTransform.childCount;

        // 現在の選択項目を非選択状態にする
        Transform selectedItem = contentTransform.GetChild(selectedIndex);
        Image selectedImage = selectedItem.Find("icon").GetComponent<Image>();
        selectedImage.color = initialIconColor;

        // 新しい選択項目を更新
        selectedIndex += direction;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, itemCount - 1);

        // 新しい選択項目を選択状態にする
        Transform newSelectedItem = contentTransform.GetChild(selectedIndex);
        Image newSelectedImage = newSelectedItem.Find("icon").GetComponent<Image>();
        newSelectedImage.color = Color.green;

    }


    // ポーズ画面における現在選択されている項目のインデックスを取得する関数
    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    public int GetMemberIndex()
    {
        return memberIndex;
    }



}