using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public ScrollView scrollView; // ScrollViewスクリプトを参照するための変数
    public Image textureDisplay; // 表示するテクスチャを表示するためのImageコンポーネント

    void Update()
    {
        // 選択されている項目のImageコンポーネントを取得してStateオブジェクトに表示する
        int selectedIndex = scrollView.GetSelectedIndex();
        Transform contentTransform = scrollView.scrollRect.content;
        if (selectedIndex >= 0 && selectedIndex < contentTransform.childCount)
        {
            Transform selectedItem = contentTransform.GetChild(selectedIndex);
            Image selectedImage = selectedItem.Find("icon/Image").GetComponent<Image>(); // Imageコンポーネントを取得
            textureDisplay.sprite = selectedImage.sprite; // StateオブジェクトのImageコンポーネントに設定
        }
    }
}
