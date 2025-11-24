using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

// プレイヤークラス
public class Player : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject target;    // プレイヤーターゲットのプレハブ
    [SerializeField] private GameObject[] life_image;    // ライフのImageオブジェクト
    [SerializeField] private Sprite broken_life;    // 割れたライフのスプライト
    [SerializeField] private GameObject mushroommgr;    // キノコ管理オブジェクト
    [SerializeField] private Image return_button;    // homeボタン
    [SerializeField] private TextMeshProUGUI score_text;    // スコアテキスト
    [SerializeField] private AudioClip damage;    // ダメージ音
    [SerializeField] private AudioClip game_over;    // ゲームオーバー音

    private GameObject target_instance = null;    // ターゲットのインスタンス
    private int destroied_enemy = 0;    // 倒した敵の数
    private int score = 0;    // スコア
    private int life = 3;    // プレイヤーの残機数
    private GameObject scoremgr;    // スコア管理オブジェクト
    private AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        scoremgr = GameObject.Find("ScoreMgr");

        return_button.gameObject.SetActive(false);

        score_text.text = "SCORE: " + 0 + " ×" + 0;

        audiosource = GetComponent<AudioSource>();
    }


    // タップ・ポインタが押されている場合に呼び出される関数
    public void OnPointerDown(PointerEventData eventData)
    {
        // ライフが残っている場合
        if(life > 0) {
            SpawnTarget(eventData);
            mushroommgr.GetComponent<MushroomMgr>().Set_Alive(true);    // キノコを発生させる
        }
    }

    // タップ・ポインタを離した場合に呼び出される関数
    public void OnPointerUp(PointerEventData eventData)
    {
        // ターゲットインスタンスがnullでない場合
        if(target_instance != null) {
            DestroyTarget();
        }
    }

    // ドラッグしている場合に呼び出される関数
    public void OnDrag(PointerEventData eventData)
    {
        // ターゲットインスタンスがnullでない場合
        if (target_instance != null) {
            // ドラッグしている位置を取得
            Vector3 destination = Camera.main.ScreenToWorldPoint(eventData.position);
            destination.z = 0f;

            target_instance.GetComponent<Transform>().position = destination;
        }

        // ターゲットインスタンスがnullでなく、ライフが0になった場合、ターゲットインスタンスを削除
        if(target_instance != null && life == 0) {
            DestroyTarget();
        }
    }

    // ターゲットインスタンスを生成する関数
    void SpawnTarget(PointerEventData eventData)
    {
        // ターゲットインスタンスがnullの場合、インスタンスを生成
        if(target_instance == null) {
            // タップ・ポインタが押された位置にインスタンスを生成
            Vector3 spawnposition = Camera.main.ScreenToWorldPoint(eventData.position);
            spawnposition.z = 0f;

            target_instance = Instantiate(target, spawnposition, target.transform.rotation);
        }
    }

    // ターゲットインスタンスを破壊する関数
    public void DestroyTarget()
    {
        // ターゲットインスタンスがnullでない場合
        if(target_instance != null) {
            Destroy(target_instance.gameObject);
            target_instance = null;
            mushroommgr.GetComponent<MushroomMgr>().Set_Alive(false);    // キノコを発生させない
        }
    }

    // プレイヤーのライフを設定する関数
    public void Update_Life()
    {
        // ライフが残っている場合
        if(life > 0) {
            life--;
            audiosource.PlayOneShot(damage);
            life_image[life].GetComponent<Image>().sprite = broken_life;

            DestroyTarget();

            // ライフが0になった場合
            if(life == 0) {
                DestroyTarget();
                SaveMyScore();
                audiosource.PlayOneShot(game_over);
                return_button.gameObject.SetActive(true);
            }
        }
    }

    // スコアを加算する関数
    public void AddScore(int sum, int num)
    {
        score = sum * num * 100;
        destroied_enemy = num;
        score_text.text = "SCORE: " + sum + " ×" + num * 100;
    }

    // スコアを保存する関数
    private void SaveMyScore()
    {
        // ハイスコアを保存
        if(scoremgr != null) {
            scoremgr.GetComponent<ScoreMgr>().SetHighScore(score, destroied_enemy);
        }
    }
}
