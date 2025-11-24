using System.Collections;
using UnityEngine;

// キノコクラス
public class Mushroom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ScaleUp");
    }

    // Update is called once per frame
    void Update()
    {
        // 一定の値まで落下した場合、オブジェクトを削除
        if(this.gameObject.transform.position.y < -7f) {
            Destroy(this.gameObject);
        }
        if(this.gameObject.transform.position.y > 7f) {
            Destroy(this.gameObject);
        }
    }

    // 時間経過でキノコの大きさを変化させるクラス
    IEnumerator ScaleUp()
    {
        for(float i = 1.0f; i < 1.5f; i+= 0.05f) {
            this.transform.localScale = new Vector3(i, i, i);

            yield return new WaitForSeconds(0.09f);
        }
    }
}
