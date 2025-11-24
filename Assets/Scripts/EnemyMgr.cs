using UnityEngine;
using TMPro;

// 敵管理クラス
public class EnemyMgr : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy_prefab;    // 敵プレハブの配列
    [SerializeField] private GameObject player;    // プレイヤーオブジェクト
    [SerializeField] private Vector3[] spawn_positions;    // 敵の発生位置ベクトル
    [SerializeField] private TextMeshProUGUI level_text;    // レベルテキスト

    private int[] enemy_elem;    // 生成している敵インスタンスの配列の要素番号
    private int[] enemy_kind;    // 敵の種類(倒していない場合0 or 倒した場合1)で表現
    private GameObject[] instance;    // 生成している敵のインスタンス数
    private int destroied_enemy = 0;    // 倒した敵の数
    private Player player_script;    // playerスクリプト

    // Start is called before the first frame update
    void Start()
    {
        // 敵配列の要素番号を初期化
        enemy_elem = new int[5];
        for(int i = 0; i < 5; i++) {
            enemy_elem[i] = 0;
        }

        // 敵のインスタンス数を初期化
        instance = new GameObject[5];
        for(int i = 0; i < 5; i++) {
            instance[i] = null;
        }

        // 敵の種類の初期化
        enemy_kind = new int[enemy_prefab.Length];
        for(int i = 0; i < enemy_prefab.Length; i++) {
            enemy_kind[i] = 0;
        }

        level_text.text = "LEVEL: " + 1;

        Spawn_Enemy(0);    // 敵を生成

        player_script = player.GetComponent<Player>();
    }

    // 敵プレハブを破壊する関数
    public void Destroy_Enemy(GameObject obj)
    {
        if(instance[0] == obj) {
            enemy_kind[enemy_elem[0]] = 1;
            instance[0] = null;
            Spawn_Enemy(0);
        }
        if(instance[1] == obj) {
            enemy_kind[enemy_elem[1]] = 1;
            instance[1] = null;
            Spawn_Enemy(1);
        }
        if(instance[2] == obj) {
            enemy_kind[enemy_elem[2]] = 1;
            instance[2] = null;
            Spawn_Enemy(2);
        }
        if(instance[3] == obj) {
            enemy_kind[enemy_elem[3]] = 1;
            instance[3] = null;
            Spawn_Enemy(3);
        }
        if(instance[4] == obj) {
            enemy_kind[enemy_elem[4]] = 1;
            instance[4] = null;
            Spawn_Enemy(4);
        }

        destroied_enemy++;

        // 敵を倒した数に応じて、敵の数を変化
        if(destroied_enemy == 5) {
            Spawn_Enemy(1);
            level_text.text = "LEVEL: " + 2;
        }
        else if(destroied_enemy == 15) {
            Spawn_Enemy(2);
            level_text.text = "LEVEL: " + 3;
        }
        else if(destroied_enemy == 30) {
            Spawn_Enemy(3);
            level_text.text = "LEVEL: " + 4;
        }
        else if(destroied_enemy == 50) {
            Spawn_Enemy(4);
            level_text.text = "LEVEL: " + 5;
        }

        player_script.AddScore(Array_Sum(), destroied_enemy);    // スコアを更新
    }

    // 敵インスタンスを生成する関数
    private void Spawn_Enemy(int num)
    {
        // 敵インスタンス[num]がnullの場合、生成
        if(instance[num] == null) {
            enemy_elem[num] = Random.Range(0, enemy_prefab.Length);
            instance[num] = Instantiate(enemy_prefab[enemy_elem[num]], spawn_positions[Random.Range(0, spawn_positions.Length)], enemy_prefab[enemy_elem[num]].transform.rotation);
        }
    }

    // enemy_kind配列の要素の合計を計算する関数
    private int Array_Sum()
    {
        int sum = 0;
        for(int i = 0; i < enemy_prefab.Length; i++) {
            sum += enemy_kind[i];
        }

        return sum;
    }
}
