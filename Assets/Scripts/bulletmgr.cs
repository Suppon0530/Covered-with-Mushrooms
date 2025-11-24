using System.Collections;
using UnityEngine;

// 弾管理クラス
public class BulletMgr : MonoBehaviour
{
    [SerializeField] private GameObject bullet;    // 弾プレハブ
    [SerializeField] private AudioClip shot;    // bulletのshot音

    private int angle;    // 発射角度
    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // 2WayShotの関数
    public void Shot_2way(float x, float y, int bullet_pattern)
    {
        if(x > 0 && y > -1) {
            angle = 215;
        }
        else if(x < 0 && y > -1) {
            angle = 305;
        }
        else if(x < 0 && y < -1) {
            angle = 35;
        }
        else if(x > 0 && y < -1) {
            angle = 125;
        }
        for(int i = 0; i < 2; i++) {

            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet script = bullet_instance.GetComponent<Bullet>();
            script.SetBullet(angle + (i * 20), 7, bullet_pattern);
        }

        audiosource.PlayOneShot(shot);
    }

    // 3WayShotの関数
    public void Shot_3way(float x, float y, int bullet_pattern)
    {
        if(x > 0 && y > -1) {
            angle = 195;
        }
        else if(x < 0 && y > -1) {
            angle = 285;
        }
        else if(x < 0 && y < -1) {
            angle = 15;
        }
        else if(x > 0 && y < -1) {
            angle = 105;
        }
        for(int i = 0; i < 3; i++) {

            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet script = bullet_instance.GetComponent<Bullet>();
            script.SetBullet(angle + (i * 30), 4, bullet_pattern);
        }

        audiosource.PlayOneShot(shot);
    }

    // ランダムショットの関数
    public IEnumerator Shot_Random(int bullet_pattern)
    {
        for(int i = 0; i < 10; i++) {

            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet script = bullet_instance.GetComponent<Bullet>();
            script.SetBullet(Random.Range(0, 360), 4, bullet_pattern);
            audiosource.PlayOneShot(shot);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // 円形発射の関数
    public void Shot_Circle(int bullet_pattern)
    {
        for(int i = 0; i < 8; i++) {

            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet script = bullet_instance.GetComponent<Bullet>();
            script.SetBullet(45 + (i * 45), 3, bullet_pattern);
        }
        audiosource.PlayOneShot(shot);
    }

    // 毒沼を生成する関数
    public void Poison_Swamp(int bullet_pattern)
    {
        GameObject bullet_instance = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 0.625f, transform.position.z), Quaternion.identity);
        Bullet script = bullet_instance.GetComponent<Bullet>();
        script.SetBullet(0, 0, bullet_pattern);
        audiosource.PlayOneShot(shot);
    }

    // 敵のコピーを生成する関数
    public void Enemy_Spawn()
    {
        GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
        audiosource.PlayOneShot(shot);
    }

    // 5WayShotの関数
    public void Shot_5way(float x, float y, int bullet_pattern)
    {
        if(x > 0 && y > -1) {
            angle = 185;
        }
        else if(x < 0 && y > -1) {
            angle = 275;
        }
        else if(x < 0 && y < -1) {
            angle = 5;
        }
        else if(x > 0 && y < -1) {
            angle = 95;
        }
        for(int i = 0; i < 5; i++) {

            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet script = bullet_instance.GetComponent<Bullet>();
            script.SetBullet(angle + (i * 20), 4, bullet_pattern);
        }

        audiosource.PlayOneShot(shot);
    }

    // 円形のウェーブショットの関数
    public IEnumerator Shot_Wave(int bullet_pattern)
    {
        angle = 36;
        for(int i = 0; i < 10; i++) {

            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet script = bullet_instance.GetComponent<Bullet>();
            script.SetBullet(angle, 3, bullet_pattern);
            audiosource.PlayOneShot(shot);
            angle += 36;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
