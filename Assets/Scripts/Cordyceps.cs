using UnityEngine;

// 冬虫夏草クラス
public class Cordyceps : MonoBehaviour
{
    [SerializeField] private AudioClip spawn;    // 冬虫夏草のスポーン音

    private AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();

        audiosource.PlayOneShot(spawn);
    }
}
