using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 敵クラス
public class Enemy : MonoBehaviour
{
    [SerializeField] private Slider slider;    // 敵のHPバー
    [SerializeField] private int hp;     // 敵のHP
    [SerializeField] private float speed;    // 移動速度
    [SerializeField] Vector3[] positions;    // 敵の移動位置
    [SerializeField] private int bullet_pattern;    // 敵の弾パターン
    [SerializeField] private GameObject cordyceps;    // 敵の冬虫夏草オブジェクト

    private GameObject bulletmgr;    // 弾管理オブジェクト
    private BulletMgr bulletmgr_script;    // 弾管理オブジェクトのスクリプト
    private GameObject enemymgr;    // 敵管理オブジェクト
    private Animator animator;    // 敵のアニメータ

    private int max_hp;    // 敵HPの最大値
    private int count = 1;    // 敵が止まらずに移動できる回数
    private Vector3 move_position;    // オブジェクトの目的地を保存
    private Vector3 direction;    // 敵の移動方向
    private int moved_count = 0;    // 移動した回数
    private float waitTime = 1.75f; // 目的地に到達した後の待機時間

    private bool isWaiting = false;    // 敵が止まっているかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        bulletmgr = transform.Find("BulletMgr").gameObject;
        if(bulletmgr != null) {
            bulletmgr_script = bulletmgr.GetComponent<BulletMgr>();
        }

        enemymgr = GameObject.Find("EnemyMgr");

        animator = this.gameObject.GetComponent<Animator>();

        max_hp = hp;

        GetRandomPosition();    // オブジェクトの目的地を設定
        count = Random.Range(2, 4);    // 敵の移動回数を設定
        slider.value = 1;    // 敵のHPバーの設定
    }

    // Update is called once per frame
    void Update()
    {
        // 敵のHPが0でない場合
        if(hp > 0) {
            // 敵が止まっていない場合
            if (!isWaiting) {
                // 敵が指定位置に到着している場合
                if(move_position == transform.position) {

                    moved_count++;
                    // 規定の回数動いた場合
                    if(moved_count == count) {
                        StartCoroutine(WaitAndMove());    // 待機

                        // bullet_patternによって、場合分け
                        switch(bullet_pattern) {
                            // ニイニイゼミの弾パターン
                            case 0:
                                bulletmgr_script.Shot_3way(this.transform.position.x, this.transform.position.y, 0);    // 弾発射
                                break;
                            // アブラゼミの弾パターン
                            case 1:
                                StartCoroutine(bulletmgr_script.Shot_Random(0));
                                break;
                            // ブナアオシャチホコの弾パターン
                            case 2:
                                bulletmgr_script.Shot_Circle(1);
                                break;
                            // ミルンヤンマの弾パターン
                            case 3:
                                bulletmgr_script.Shot_2way(this.transform.position.x, this.transform.position.y, 0);
                                break;
                            // スズメバチの弾パターン
                            case 4:
                                bulletmgr_script.Poison_Swamp(2);
                                break;
                            // トゲアリの弾パターン
                            case 5:
                                bulletmgr_script.Enemy_Spawn();
                                break;
                            // ミンミンゼミの弾パターン
                            case 6:
                                bulletmgr_script.Shot_5way(this.transform.position.x, this.transform.position.y, 0);
                                break;
                            // クマゼミの弾パターン
                            case 7:
                                StartCoroutine(bulletmgr_script.Shot_Random(0));
                                break;
                            // イワサキカレハの弾パターン
                            case 8:
                                StartCoroutine(bulletmgr_script.Shot_Wave(1));
                                break;
                            // アオオサムシのパターン(移動が速くなるのみ)
                            case 9:
                                if(speed == 2.0f) {
                                    speed = 4.0f;
                                }
                                else {
                                    speed = 2.0f;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else {
                        GetRandomPosition();    // ランダムな位置を設定
                    }
                }
                else {
                    transform.position = Vector3.MoveTowards(transform.position, move_position, speed * Time.deltaTime);
                }
            }
        }
    }

    // 敵の目的地、移動回数を特定の5つの位置からランダムに生成する関数
    private void GetRandomPosition() 
    {
        // 敵の移動位置と現在位置が異なるように設定
        while(move_position == transform.position) {
            move_position = positions[Random.Range(0, positions.Length)];    // 敵の移動位置をランダムに設定
        }

        direction = (move_position - transform.position).normalized;    // 敵の移動方向を正規化
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    // 目的地に到達した後、一定時間待機してから新しい目的地に移動する
    IEnumerator WaitAndMove()
    {
        // トゲアリとアオオサムシのみ、アニメーションを停止
        if(bullet_pattern == 5 || bullet_pattern == 9) {
            animator.speed = 0f;
        }
        isWaiting = true;

        // 現在位置から、対角線上の反対に敵の方向を変更
        if(transform.position.x > 0 && transform.position.y > -1) {
            animator.SetFloat("x", -1);
            animator.SetFloat("y", -1);
        }
        else if(transform.position.x < 0 && transform.position.y > -1) {
            animator.SetFloat("x", 1);
            animator.SetFloat("y", -1);
        }
        else if(transform.position.x < 0 && transform.position.y < -1) {
            animator.SetFloat("x", 1);
            animator.SetFloat("y", 1);
        }
        else if(transform.position.x > 0 && transform.position.y < -1) {
            animator.SetFloat("x", -1);
            animator.SetFloat("y", 1);
        }
        yield return new WaitForSeconds(waitTime);
        
        // トゲアリとアオオサムシのみ、アニメーションを再開
        if(bullet_pattern == 5 || bullet_pattern == 9) {
            animator.speed = 1f;
        }

        // 敵の移動位置の設定
        GetRandomPosition();

        // 敵のHPが半分のタイミングで、移動回数を変更
        if(hp > max_hp / 2) {
            count = Random.Range(2, 5);
        }
        else {
            count = Random.Range(1, 3);
        }
        moved_count = 0;
        isWaiting = false;
    }

    // プレイヤーが発生させるキノコとの当たり判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        // キノコと接触した場合
        if(collision.gameObject.tag == "mushroom") {
            hp--;
            slider.value = (float)hp / (float)max_hp;
            Destroy(collision.gameObject);
        }

        // 敵のHPが0になった場合
        if(hp == 0) {
            // アリのbullet以外は、冬虫夏草インスタンスを生成
            if(bullet_pattern != -1) {
                Instantiate(cordyceps, transform.position, Quaternion.Euler(0, 0, Random.Range(-90f, 90f)));
                enemymgr.GetComponent<EnemyMgr>().Destroy_Enemy(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
