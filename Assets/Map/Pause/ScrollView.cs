using UnityEngine;
using UnityEngine.UI;


public class ScrollView : MonoBehaviour
{
    public ScrollRect scrollRect;
    //スクロールの速度設定(チームメンバーが合計n人いたら1/nfで設定する)
    float scrollStep = 1/25f;

    // 現在選択されている項目のインデックス
    public int selectedIndex { get; private set; } = 0;
    //現在選択されている項目の画像
    public Transform newSelectedItem;

    // iconの初期色
    private Color initialIconColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);

    private enemy_manager enemyManager; // ポーズ画面を管理するスクリプトへの参照

    public GameObject member1;
    public GameObject member2;
    public GameObject member3;
    public GameObject[] members;
    public int memberIndex { get; private set; } = 0;

    void Start()
    {
        enemyManager = FindObjectOfType<enemy_manager>(); // enemy_managerスクリプトへの参照を取得

        // 初期状態で最初の項目を選択状態とする
        UpdateSelection(selectedIndex);
        if(!enemyManager.isaddmember){
            member1.SetActive(true);
        }
        Debug.Log("a");

        // 設定されたステップサイズに基づいてコンテンツの高さを計算
        RectTransform contentRect = scrollRect.content.GetComponent<RectTransform>();
        float contentHeight = contentRect.sizeDelta.y;
        float stepHeight =  scrollStep * contentHeight;
        scrollRect.content.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight + stepHeight);
    }

    void Update()
    {
        if (enemyManager.iseditteam || enemyManager.isaddmember) // ポーズ画面が表示されている時のみスクロールを処理
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
            }else if (Input.GetKeyDown(KeyCode.A) && !enemyManager.isaddmember)
            {
                Updatemember(-1);
            }else if (Input.GetKeyDown(KeyCode.D) && !enemyManager.isaddmember)
            {
                Updatemember(1);
            }
        }
    }

    
    // スクロールアップ処理
    void ScrollUp()
    {
        scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition + scrollStep;
    }

    // スクロールダウン処理
    void ScrollDown()
    {
        scrollRect.verticalNormalizedPosition = scrollRect.verticalNormalizedPosition - scrollStep;
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


    //選択メンバーの更新
    void Updatemember(int change)
    {
        memberIndex = Mathf.Clamp(memberIndex + change, 0, 2); // memberIndexを0から2の範囲に制限
        membershow(); // メンバーオブジェクトの表示を更新する
    }


    //選択メンバーの表示
    void membershow()
    {
        if (memberIndex == 0)
        {
            member1.SetActive(true);
            member2.SetActive(false);
            member3.SetActive(false);
        }
        else if (memberIndex == 1)
        {
            member1.SetActive(false);
            member2.SetActive(true);
            member3.SetActive(false);
        }
        else if (memberIndex == 2)
        {
            member1.SetActive(false);
            member2.SetActive(false);
            member3.SetActive(true);
        }
    }

    // ポーズ画面における現在選択されている項目のインデックスを取得する関数
    public int  GetSelectedIndex()
    {
        return selectedIndex;
    }

    public int GetMemberIndex()
    {
        return memberIndex;
    }

    

}