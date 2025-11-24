using UnityEngine;

// キノコ管理クラス
public class MushroomMgr : MonoBehaviour
{
    [SerializeField] private GameObject[] mushroom;    // キノコのプレハブ

    private int geted_cordyceps;    // 獲得した冬虫夏草の総数
    private int index = 0;    // キノコの要素番号
    private int index_max;    // キノコの要素の最大値
    private float count_time;    // カウント
    private float time;    // キノコインスタンスを生成する時間間隔
    private bool alive = true;    // プレイヤーが生きているかどうか
    private GameObject scoremgr;    // スコア管理オブジェクト

    // Start is called before the first frame update
    void Start()
    {
        scoremgr = GameObject.Find("ScoreMgr");
        if(scoremgr != null) {
            geted_cordyceps = scoremgr.GetComponent<ScoreMgr>().GetCordyceps();
        }

        index_max = 5;

        // 獲得した冬虫夏草の数によって、キノコの発射間隔と発射種類を変化
        if(geted_cordyceps >= 1000) {
            time = 0.05f;
            index_max += 10;
        }
        else {
            time = 0.1f - geted_cordyceps / 100 * 0.005f;
            index_max += geted_cordyceps / 100;
        }

        count_time = time;
    }

    // Update is called once per frame
    void Update()
    {
        // マウスがドラッグされている場合またはタッチが行われている場合
        if (Input.GetMouseButton(0))
        {
            if(count_time < 0) {
                SpawnMushroom();
                count_time = time;
            }
        }

        count_time -= Time.deltaTime;
    }

    // キノコを生成する関数
    void SpawnMushroom()
    {
        // targetオブジェクトが生成されている場合のみ
        if(alive) {
            // マウスまたはタッチの位置にキノコを生成
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0f;

            // タッチ位置yが-5.0 < y <-3.0の場合、キノコインスタンスを生成
            if(-5.0f < spawnPosition.y && spawnPosition.y < 3.0f) { 
                GameObject instance = Instantiate(mushroom[index], spawnPosition, mushroom[index].transform.rotation);
            
                // タッチ位置yが-1.0を境目にして下方向に落下 or 上方向に上昇
                if(spawnPosition.y > -1.0f) {
                    instance.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    instance.layer = LayerMask.NameToLayer("mushroomDown");
                }
                else {
                    instance.GetComponent<Rigidbody2D>().gravityScale = -1f;
                    instance.layer = LayerMask.NameToLayer("mushroomUp");
                }
                
                index++;

                if (index >= index_max) {
                    index = 0;
                }
            }
        }
    }

    // targetオブジェクトが生成されているかどうかを設定する関数
    public void Set_Alive(bool state)
    {
        alive = state;
    }
}
