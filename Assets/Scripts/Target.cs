using UnityEngine;

// ターゲットクラス
public class Target : MonoBehaviour
{
    private GameObject player;    // プレイヤーオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("canTouchArea");
    }

    // Update is called once per frame
    void Update()
    {
        // フィールド外に指が出たら、targetオブジェクトを削除
        if(this.gameObject.transform.position.y > 3.0f || this.gameObject.transform.position.y < -5.0f) {
            player.GetComponent<Player>().DestroyTarget();
        }
    }

    // 敵との当たり判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        // enemyタグと衝突した場合、ライフを減少
        if(collision.gameObject.tag == "enemy") {
            player.GetComponent<Player>().Update_Life();
        }

        // bulletタグと衝突した場合、ライフを減少
        if(collision.gameObject.tag == "bullet") {
            Destroy(collision.gameObject);
            player.GetComponent<Player>().Update_Life();
        }
    }
}
