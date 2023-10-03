using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public ScrollView scrollView; // ScrollViewスクリプトを参照するための変数
    public Image textureDisplay; // 表示するテクスチャを表示するためのImageコンポーネント

    void Update()
    {
        // 選択されている項目のImageコンポーネントを取得してStateオブジェクトに表示する
        var selectedIndex = scrollView.GetSelectedIndex();
        var contentTransform = scrollView.scrollRect.content;
        if (selectedIndex >= 0 && selectedIndex < contentTransform.childCount)
        {
            var selectedItem = contentTransform.GetChild(selectedIndex);
            var selectedImage = selectedItem.Find("icon/Image").GetComponent<Image>(); // Imageコンポーネントを取得
            textureDisplay.sprite = selectedImage.sprite; // StateオブジェクトのImageコンポーネントに設定
        }
    }
}
