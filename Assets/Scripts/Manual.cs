using UnityEngine;
using UnityEngine.UI;

// マニュアルクラス
public class Manual : MonoBehaviour
{
    [SerializeField] private Image explain;    // 説明画面
    [SerializeField] private Sprite[] explain_sprite;    // 説明スプライト
    [SerializeField] private Button next;
    [SerializeField] private Button prev;

    void OnEnable()
    {
        explain.GetComponent<Image>().sprite = explain_sprite[0];

        next.gameObject.SetActive(true);
        prev.gameObject.SetActive(false);
    }

    // ネクストボタン選択時の処理
    public void OnNextButtonClick()
    {
        explain.GetComponent<Image>().sprite = explain_sprite[1];

        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(true);
    }

    // プレブボタン選択時の処理
    public void OnPrevButtonClick()
    {
        explain.GetComponent<Image>().sprite = explain_sprite[0];

        next.gameObject.SetActive(true);
        prev.gameObject.SetActive(false);
    }
}
