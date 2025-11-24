using UnityEngine;

// 弾クラス
public class Bullet : MonoBehaviour
{
    private float angle;    // 弾の発射角度
    private float speed;    // 弾の速さ
    private int bullet_pattern;    // 弾の種類

    private Vector3 velocity;    // 弾の速さベクトル
    private float time = 0f;    // 弾のタイマー

    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        switch(bullet_pattern) {
            // case 0: 弾を発射する動作
            case 0:
            // case 1: 弾が回転する動作
            case 1:
                velocity.x = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
                velocity.y = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

                float zangle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                Destroy(gameObject, 5.0f);
                break;
            // case 2: 弾がその場に残り続ける動作
            case 2:
                render = GetComponent<SpriteRenderer>();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bullet_pattern == 0) {
            // 弾を進行方向に進める
            transform.position += velocity * Time.deltaTime;
        }
        else if(bullet_pattern == 1) {
            // 弾を進行方向に進める
            transform.position += velocity * Time.deltaTime;
            // 弾を回転させる
            transform.rotation = Quaternion.Euler(0, 0, time += 300.0f * Time.deltaTime);
        }
        else if(bullet_pattern == 2) {
            // 弾を徐々に透明にする
            time += Time.deltaTime;
            if(time < 5.0f) {
                float alpha = 1.0f - time / 10.0f;
                Color color = render.color;
                color.a = alpha;
                render.color = color;
            }
            else {
                Destroy(this.gameObject);
            }
        }
    }

    // 弾を設定するメソッド
    public void SetBullet(float data_0, float data_1, int data_2)
    {
        angle = data_0;
        speed = data_1;
        bullet_pattern = data_2;
    }
}
