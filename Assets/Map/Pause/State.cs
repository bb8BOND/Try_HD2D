using UnityEngine;
using UnityEngine.UI;
public class State : MonoBehaviour
{
    public RawImage nowcharacter;//現在選択されているキャラを表示するRawImageオブジェクト
    public ScrollView scrollview;//scrollViewスクリプトへの参照用

    //現在選択されているメンバーを表すgameobject
    public GameObject nowmember1;
    public GameObject nowmember2;
    public GameObject nowmember3;
    //各iconのコンポーネント
    private Image icon_1_component;
    private Image icon_2_component;
    private Image icon_3_component;
    //各RawImageのコンポーネント
    private RawImage rawimage_1_component;
    private RawImage rawimage_2_component;
    private RawImage rawimage_3_component;
    // iconの初期色
    private Color initialIconColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);
    // 決定されたキャラの画像のパスを保存する
    public string decidedchara_path;
    void Start()
    {
        // ScrollViewスクリプトへの参照を取得
        scrollview = FindObjectOfType<ScrollView>();

        //nowmemberの子オブジェクトicon(背景色)とそのImageコンポーネント取得
        var icon_1 = nowmember1.transform.Find("icon");
        var icon_2 = nowmember2.transform.Find("icon");
        var icon_3 = nowmember3.transform.Find("icon");
        icon_1_component = icon_1.GetComponent<Image>();
        icon_2_component = icon_2.GetComponent<Image>();
        icon_3_component = icon_3.GetComponent<Image>();
        //nowmemberオブジェクトで、member1を選択状態にする。
        icon_1_component.color = Color.green;

        //nowmemberの孫オブジェクトRawImage(画像)とそのRawImageコンポーネント取得
        var rawimage_1 = icon_1.transform.Find("RawImage");
        var rawimage_2 = icon_2.transform.Find("RawImage");
        var rawimage_3 = icon_3.transform.Find("RawImage");
        rawimage_1_component = rawimage_1.GetComponent<RawImage>();
        rawimage_2_component = rawimage_2.GetComponent<RawImage>();
        rawimage_3_component = rawimage_3.GetComponent<RawImage>();
    }

    void Update()
    {
        Nowchara_state();
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            Nowmember_changecolor(scrollview.memberIndex);
        }
        Change_nowcharaImage(scrollview.memberIndex);
    }

    //現在選択されているキャラクターの画像を更新
    void Nowchara_state()
    {
        var nowcharaIndex = scrollview.GetSelectedIndex();//scrollviewのselectedIndexをnowcharaIndexへ代入
        Debug.Log("nowcharaIndex:"+nowcharaIndex);
        //現在選択されている画像のpathを作成
        var nowchara_path = scrollview.pngFiles[nowcharaIndex - 1];
        // PNGファイルをバイトデータとして読み込む
        var newfileData = System.IO.File.ReadAllBytes(nowchara_path);
        // Texture2Dを作成し、PNGデータを読み込む
        var texture = new Texture2D(2, 2);
        texture.LoadImage(newfileData);
        // RawImageコンポーネントのtextureに設定
        nowcharacter.texture = texture;
    }

    //現在選択されているnowmemberオブジェクト(の背景食)を更新
    void Nowmember_changecolor(int nowmemberIndex)
    {
        if (nowmemberIndex == 0)
        {
            icon_1_component.color = Color.green;
            icon_2_component.color = initialIconColor;
            icon_3_component.color = initialIconColor;
        }
        else if (nowmemberIndex == 1)
        {
            icon_1_component.color = initialIconColor;
            icon_2_component.color = Color.green;
            icon_3_component.color = initialIconColor;
        }
        else if (nowmemberIndex == 2)
        {
            icon_1_component.color = initialIconColor;
            icon_2_component.color = initialIconColor;
            icon_3_component.color = Color.green;
        }
        
    }

    void Change_nowcharaImage(int nowmemberIndex)
    {
        if (nowmemberIndex == 0)
        {
            decidedchara_path = scrollview.pngFiles[EnemyManager.member1 - 1];//member1に決定されたの画像パスを取得 
        }
        else if (nowmemberIndex == 1)
        {
            decidedchara_path = scrollview.pngFiles[EnemyManager.member2 - 1];//member2に決定された画像のパスを取得
        }
        else if (nowmemberIndex == 2)
        {
            decidedchara_path = scrollview.pngFiles[EnemyManager.member3 - 1];//member3に決定された画像のパスを取得
        }
        var newfiledata = System.IO.File.ReadAllBytes(decidedchara_path);
        var texture = new Texture2D(2, 2);
        texture.LoadImage(newfiledata);
        if (nowmemberIndex == 0)
        {
            rawimage_1_component.texture = texture;
        }
        else if (nowmemberIndex == 1)
        {
            rawimage_2_component.texture = texture;
        }
        else if (nowmemberIndex == 2)
        {
            rawimage_3_component.texture = texture;
        }
    }
}
