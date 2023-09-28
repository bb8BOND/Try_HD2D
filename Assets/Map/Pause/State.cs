using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public ScrollView scrollView; // ScrollView�X�N���v�g���Q�Ƃ��邽�߂̕ϐ�
    public Image textureDisplay; // �\������e�N�X�`����\�����邽�߂�Image�R���|�[�l���g

    void Update()
    {
        // �I������Ă��鍀�ڂ�Image�R���|�[�l���g���擾����State�I�u�W�F�N�g�ɕ\������
        int selectedIndex = scrollView.GetSelectedIndex();
        Transform contentTransform = scrollView.scrollRect.content;
        if (selectedIndex >= 0 && selectedIndex < contentTransform.childCount)
        {
            Transform selectedItem = contentTransform.GetChild(selectedIndex);
            Image selectedImage = selectedItem.Find("icon/Image").GetComponent<Image>(); // Image�R���|�[�l���g���擾
            textureDisplay.sprite = selectedImage.sprite; // State�I�u�W�F�N�g��Image�R���|�[�l���g�ɐݒ�
        }
    }
}
