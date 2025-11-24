using UnityEngine;
using UnityEngine.SceneManagement;

// ゲームシーンクラス
public class Game : MonoBehaviour
{
    // 戻るボタン選択時の処理
    public void OnReturnButtonClick()
    {
        SceneManager.LoadScene("Title");
    }
}