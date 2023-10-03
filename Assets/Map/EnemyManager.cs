using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject SlimePBR;
    public GameObject TurtleShellPBR;
    public GameObject Dragon_1;
    public GameObject Dragon_2;
    public GameObject Dragon_3;
    public GameObject Dragon_4;
    public GameObject Dragon_5;

    public Transform SlimePBR_place;
    public Transform TurtleShellPBR_place;
    public Transform Dragon_1_place;
    public Transform Dragon_2_place;
    public Transform Dragon_3_place;
    public Transform Dragon_4_place;
    public Transform Dragon_5_place;

    float TimeCount;
    public bool isPaused { get; private set; } // ポーズ画面のフラグ
    public float focusDistance = 1f;
    public GameObject postProcessVolumeObject;
    // Post Process Volumeコンポーネントを格納する変数
    private PostProcessVolume postProcessVolume;
    // Pが押されたかどうかを保存する変数
    private bool isPKeyPressed = false;

    public GameObject nowIndicator; // "now"を示すオブジェクト
    public GameObject optionsPanel; // 選択肢の親パネル
    private bool isInputEnabled = true; // 入力の有効化フラグ
    private bool onselect = false;//選択コマンドの移動を管理
    private int selectedIndex = 0;      // 選択されている項目のインデックス

    public GameObject canvasObject; // シリアライズフィールドを使用して、インスペクター上でCanvasオブジェクトを指定
    public GameObject titlecheck;
    public GameObject ScrollView;
    public bool iseditteam = false;
    public bool isaddmember = false;
    public GameObject State;

    [SerializeField]private Fade fade;

    static public int member1 = 0;
    static public int member2 = 1;
    static public int member3 = 2;
    static public int addmem;

    public GameObject Addmem;
    

    // Phaseを定義
    private enum Phase
    {
        Selectoption,
        back,
        editteam,
        addmember,
        title,
        End
    }

    private Phase currentPhase = Phase.Selectoption;

    void Start()
    {
        fade.FadeOut(1f);
        TimeCount = 0f; // TimeCountの初期化
        // Post Process Volumeコンポーネントを取得します
        postProcessVolume = postProcessVolumeObject.GetComponent<PostProcessVolume>();

        // 初期状態で最初の項目を選択状態とする
        SelectOption(0);




        Instantiate(SlimePBR, SlimePBR_place.position, Quaternion.identity);
        Instantiate(TurtleShellPBR, TurtleShellPBR_place.position, Quaternion.identity); 　　　//要調整だあああああああああああああああああああああああああ
        Instantiate(Dragon_1, Dragon_1_place.position, Quaternion.identity);
        Instantiate(Dragon_2, Dragon_2_place.position, Quaternion.identity);
        Instantiate(Dragon_3, Dragon_3_place.position, Quaternion.identity);
        Instantiate(Dragon_4, Dragon_4_place.position, Quaternion.identity);
        Instantiate(Dragon_5, Dragon_5_place.position, Quaternion.identity);

    }

    void Update()
    {
        if (!isPaused)
    {
        //タブン敵自動生成のコード
        //TimeCount += Time.deltaTime;
        //if (TimeCount > 10)
        //{
        //    Instantiate(SlimePBR, SlimePBR_place.position, Quaternion.identity);
        //    Instantiate(TurtleShellPBR, TurtleShellPBR_place.position, Quaternion.identity);          ここら辺で適正性10fぐらい
        //    TimeCount = 0f; // TimeCountのリセット
        //}
    }
    else if (isPaused)
    {
            if (!onselect)
            {// 入力があれば選択項目を変更
                if (Input.GetKeyDown(KeyCode.W)) // Input.GetAxis("Vertical") > 0
                {
                    if (selectedIndex > 0)
                    {
                        SelectOption(selectedIndex - 1);
                    }
                    Debug.Log("現在選択されている項目：" + optionsPanel.transform.GetChild(selectedIndex).name);
                }
                else if (Input.GetKeyDown(KeyCode.S)) // Input.GetAxis("Vertical") < 0
                {
                    if (selectedIndex < optionsPanel.transform.childCount - 1)
                    {
                        SelectOption(selectedIndex + 1);
                    }
                    Debug.Log("現在選択されている項目：" + optionsPanel.transform.GetChild(selectedIndex).name);
                }
            }
            
    }

        if (Input.GetKeyDown(KeyCode.O) && !isPKeyPressed)
        {
            isPKeyPressed = true;
            focusDistance = 1f;
            UpdateFocusDistance();//ぼかし
            canvasObject.SetActive(true);
            nowIndicator.transform.position = new Vector3(1743, 305, 0);
            StartCoroutine(PhaseCoroutine());//コルーチン実行
            TogglePause();
        }
        
    }

    // ゲームの一時停止を切り替える関数
    void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }

    // ゲームを一時停止する関数
    void Pause()
    {
        Time.timeScale = 0f; // ゲームの時間を停止
        isPaused = true;
        Debug.Log("ゲームが一時停止しました");
    }

    // ゲームを再開する関数
    void ResumeGame()
    {
        Time.timeScale = 1f; // ゲームの時間を通常に戻す
        isPaused = false;
        Debug.Log("ゲームが再開されました");
    }

    // ポーズ画面をぼかす関数
    private void UpdateFocusDistance()
    {
        // Post Process Volumeの設定を取得します
        var volumeSettings = postProcessVolume.sharedProfile;

        // DepthOfFieldコンポーネントを取得します
        DepthOfField dof;
        if (volumeSettings.TryGetSettings(out dof))
        {
            // Focus Distanceの値を設定します
            dof.focusDistance.value = focusDistance; // float型に修正
        }
    }

    // 選択された項目を更新するメソッド
    private void SelectOption(int index)
    {
        // 選択された項目のインデックスに基づいて、文字色を設定する
        for (int i = 0; i < 4; i++)
        {
            Transform optionTransform = optionsPanel.transform.GetChild(i);
            TextMeshProUGUI optionText = optionTransform.GetComponent<TextMeshProUGUI>();
            if (optionText != null)
            {
                if (i == index)
                {
                    // 選択されている項目の文字色を変更
                    optionText.color = new Color(1f, 1f, 1f); // 白色
                }
                else
                {
                    // 選択されていない項目の文字色を元に戻す
                    optionText.color = new Color(130f/255f , 130f/255f , 130f/255f);
                }
            }
        }

        selectedIndex = index;

        string optionName = GetOptionName(selectedIndex);

        // "back"だった場合の処理
        if (optionName == "back")
        {
            nowIndicator.transform.position = new Vector3(1743, 305, 0);
        }else if(optionName == "editteam")
        {
            nowIndicator.transform.position = new Vector3(1743, 235, 0);
        }else if(optionName == "addmember")
        {
            nowIndicator.transform.position = new Vector3(1743, 165, 0);
        }else if(optionName == "title")
        {
            nowIndicator.transform.position = new Vector3(1743, 95, 0);
        }
    }

    // 現在選択されている項目の名前を取得するメソッド
    private string GetOptionName(int index)
    {
        return optionsPanel.transform.GetChild(index).name;
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
        yield return new WaitForSeconds(0.15f);  // 適切な待ち時間を設定する
        isInputEnabled = true;
    }

    // コルーチンの実行
    private IEnumerator PhaseCoroutine()
    {
        while (currentPhase != Phase.End)
        {
            switch (currentPhase)
            {
                case Phase.Selectoption:
                    Debug.Log("Selectoption Phaseが実行されました");
                    onselect = false;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U));
                    if(GetOptionName(selectedIndex) == "back")
                    {
                        onselect = true;
                        currentPhase = Phase.back;
                    }else if(GetOptionName(selectedIndex) == "editteam")
                    {
                        onselect = true;
                        currentPhase = Phase.editteam;
                    }
                    else if(GetOptionName(selectedIndex) == "addmember")
                    {
                        onselect = true;
                        currentPhase = Phase.addmember;
                    }
                    else if(GetOptionName(selectedIndex) == "title")
                    {
                        onselect = true;
                        //fade.FadeIn(1f, () => SceneManager.LoadScene("empty"));
                        currentPhase = Phase.title;
                    }
                    
                    break;
                case Phase.back:
                    Debug.Log("back Phaseが実行されました");
                    
                    isPKeyPressed = false;
                    TogglePause();
                    focusDistance = 10f;
                    UpdateFocusDistance();//ぼかし無効
                    canvasObject.SetActive(false);
                    currentPhase = Phase.End;
                    break;
                case Phase.editteam:
                    Debug.Log("editteam Phaseが実行されました");
                    ScrollView .SetActive(true);
                    //State.SetActive(true);
                    iseditteam = true;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I));
                    Debug.Log("選択されました。");

                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        ScrollView .SetActive(false);
                        iseditteam = false;
                        currentPhase = Phase.Selectoption;
                    }
                    else if (Input.GetKeyDown(KeyCode.U))
                    {   // ScrollViewの参照を取得
                        ScrollView scrollView = ScrollView.GetComponent<ScrollView>();

                        if (scrollView.GetMemberIndex() == 0)
                        {
                            member1 = scrollView.GetSelectedIndex();
                            Debug.Log($"member1は{member1}に決定！");

                        } else if (scrollView.GetMemberIndex() == 1)
                        {
                            member2 = scrollView.GetSelectedIndex();
                            Debug.Log($"member2は{member2}に決定！");

                        } else if (scrollView.GetMemberIndex() == 2)
                        {
                            member3 = scrollView.GetSelectedIndex();
                            Debug.Log($"member3は{member3}に決定！");
                        }
                    }
                    break;
                case Phase.addmember:
                    Debug.Log("addmember Phaseが実行されました");

                    ScrollView.SetActive(true);//一覧を表示
                    iseditteam = true;
                    isaddmember = true;
                    if(Input.GetKeyDown(KeyCode.I)){
                        ScrollView.SetActive(false);
                        iseditteam = false;
                        isaddmember = false;
                        currentPhase = Phase.Selectoption;
                    }else if(Input.GetKeyDown(KeyCode.U)){
                        // ScrollViewの参照を取得
                        ScrollView scrollView = ScrollView.GetComponent<ScrollView>();
                        addmem = scrollView.GetSelectedIndex();
                        //addmemには選択されたキャラのindex番号が保存されている。
                        //この後に、選択されたキャラクターの攻撃力とかhpとかを取得するコードを書く必要ありいいいいいいいいいいいいい
                        //後icカードのやつとの連携も必要だよおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおおお


                        //battle_manager battleManager;
                        //GameObject obj = GameObject.Find("friends");
                        //battleManager = obj.GetComponent<battle_manager>();
                        //Addmem = battleManager.friends[addmem];
                    }
                    break;

                case Phase.title:
                    Debug.Log("title Phaseが実行されました");
                    
                    titlecheck.SetActive(true);
                    // UかIのいずれかが押されるまで待機
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I));

                    // どちらのキーが押されたか判別
                    if (Input.GetKeyDown(KeyCode.U))
                    {
                        isPKeyPressed = false;
                        TogglePause();
                        focusDistance = 10f;
                        UpdateFocusDistance();//ぼかし無効
                        canvasObject.SetActive(false);
                        
                        fade.FadeIn(1f, () => SceneManager.LoadScene("empty"));
                        yield return new WaitForSeconds(10f);
                        Debug.Log("タイトルに遷移じゃああああ");
                        currentPhase = Phase.End;
                    }
                    else if (Input.GetKeyDown(KeyCode.I))
                    {
                        titlecheck.SetActive(false);
                        currentPhase = Phase.Selectoption;
                        break;
                    }
                    break;
                case Phase.End:
                    break;
            }

            // 選択項目が変更された場合にnowIndicatorの位置を更新する
            SelectOption(selectedIndex);
            yield return null;
        }
        currentPhase = Phase.Selectoption;

        yield break;
    }

    IEnumerator titlescene()
    {
        //待機時間
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("empty");
    }
}
