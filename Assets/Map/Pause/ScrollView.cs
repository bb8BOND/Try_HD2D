using System;
using System.Collections.Generic;
using System.IO;
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

    private EnemyManager enemyManager; // ポーズ画面を管理するスクリプトへの参照

    public GameObject member1;
    public GameObject member2;
    public GameObject member3;
    public GameObject[] members;
    public int memberIndex { get; private set; } = 0;

    string folderPath = "C:/Users/81802/unity/chara"; // 検索したいフォルダのパス
    List<string> pngFiles = new List<string>();// 指定されたフォルダ内のPNGファイルのパスを格納するリスト
    public GameObject characterObject_original;

    void Start()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>(); // EnemyManagerスクリプトへの参照を取得

        var result = Searchfile();
        var pngCount = result.Item1; // 1番目の返り値(ファイルの総数)を取得
        List<string> pngFiles = result.Item2; // 2番目の返り値(ファイルのパスのリスト)を取得
        scrollStep = 1f / pngCount;
        for (var i = 0; i < pngCount; i++)
        {
            var characterObject = Creat_copy(characterObject_original);
            // 孫オブジェクトを取得
            var childTransform = characterObject.transform.Find("icon");
            var grandchildTransform = childTransform.transform.Find("RawImage");
            // RawImageコンポーネントを取得
            var rawImageComponent = grandchildTransform.GetComponent<RawImage>();
            // PNGファイルをバイトデータとして読み込む
            var fileData = System.IO.File.ReadAllBytes(pngFiles[i]);
            // Texture2Dを作成し、PNGデータを読み込む
            var texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            // RawImageコンポーネントのtextureに設定します。
            rawImageComponent.texture = texture;
        }

        // 初期状態で最初の項目を選択状態とする
        UpdateSelection(selectedIndex);
        if(!enemyManager.isaddmember){
            member1.SetActive(true);
        }

        // 設定されたステップサイズに基づいてコンテンツの高さを計算
        var contentRect = scrollRect.content.GetComponent<RectTransform>();
        var contentHeight = contentRect.sizeDelta.y;
        var stepHeight =  scrollStep * contentHeight;
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
        var contentTransform = scrollRect.content;
        var itemCount = contentTransform.childCount;

        // 現在の選択項目を非選択状態にする
        var selectedItem = contentTransform.GetChild(selectedIndex);
        var selectedImage = selectedItem.Find("icon").GetComponent<Image>();
        selectedImage.color = initialIconColor;

        // 新しい選択項目を更新
        selectedIndex += direction;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, itemCount - 1);

        // 新しい選択項目を選択状態にする
        var newSelectedItem = contentTransform.GetChild(selectedIndex);
        var newSelectedImage = newSelectedItem.Find("icon").GetComponent<Image>();
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

    // 指定されたフォルダ内のPNGファイルを探す
    public Tuple<int, List<string>> Searchfile()
    {
        var pngCount = 0;

        // フォルダが存在しない場合はエラーを表示して終了
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("指定されたフォルダが存在しません: " + folderPath);
            return new Tuple<int, List<string>>(0, new List<string>());
        }

        // フォルダ内のファイルを取得し、PNGファイルをカウントおよびリストに追加
        var files = Directory.GetFiles(folderPath);
        foreach (var file in files)
        {
            if (file.ToLower().EndsWith(".png"))
            {
                pngCount++;
                pngFiles.Add(file); // リストにPNGファイルのパスを追加
            }
        }

        // リストに格納されたPNGファイルを数値順にソート
        SortPngFiles();

        // リストに格納されたPNGファイルのパスを表示（デバッグ用）
        foreach (var pngFilePath in pngFiles)
        {
            Debug.Log("PNGファイルのパス: " + pngFilePath);
        }

        return new Tuple<int, List<string>>(pngCount, pngFiles);
    }

    //メンバーオブジェクトの複製関数
    GameObject Creat_copy(GameObject original)
    {
        if (original == null)
        {
            Debug.LogWarning("Original GameObject is null. Cannot clone.");
            return null;
        }

        // オブジェクトを複製し、新しいインスタンスを返す
        var clonedObject = Instantiate(original, transform.position, transform.rotation);
        // 複製されたオブジェクトの親をオリジナルと同じに設定する
        if (original.transform.parent != null)
        {
            clonedObject.transform.SetParent(original.transform.parent);
        }
        return clonedObject;
    }

    // リストに格納されたPNGファイルを数値順にソートする関数
    public void SortPngFiles()
    {
        pngFiles.Sort((a, b) =>
        {
            string fileNameA = Path.GetFileNameWithoutExtension(a);
            string fileNameB = Path.GetFileNameWithoutExtension(b);

            int numberA, numberB;
            bool successA = int.TryParse(fileNameA, out numberA);
            bool successB = int.TryParse(fileNameB, out numberB);

            // 数値に変換できる場合、数値で比較
            if (successA && successB)
            {
                return numberA.CompareTo(numberB);
            }
            // 数値に変換できない場合、文字列として比較
            else
            {
                return fileNameA.CompareTo(fileNameB);
            }
        });
    }


}