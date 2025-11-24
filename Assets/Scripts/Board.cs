using UnityEngine;
using TMPro;

// ボードクラス
public class Board : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score_text;    // 冬虫夏草の総GET数のテキスト

    private ScoreMgr scoredata;    // スコアデータ
    private int num;    // 冬虫夏草をGETした総数

    // Start is called before the first frame update
    void Start()
    {
        // スコアデータを取得
        GameObject dontDestroyObject = GameObject.Find("ScoreMgr");
        if (dontDestroyObject != null)
        {
            scoredata = dontDestroyObject.GetComponent<ScoreMgr>();
            num = scoredata.GetCordyceps();
            if(num > 99999) {
                num = 99999;
            }

            score_text.text = "冬虫夏草の\n総GET数\n" + num;
        }
    }
}
