using UnityEngine;

[CreateAssetMenu(fileName = "Enemymgrdata", menuName = "Enemymgrdata")]

// 敵データを管理するクラス
public class Enemymgrdata : ScriptableObject
{
    [SerializeField] private Enemydata enemydata;    // 敵データの内部クラス

    private GameObject enemy_prefab;    // 敵オブジェクトのプレハブ
    private const int enemy_num = 1;    // 敵オブジェクトの数

    public void Set_Enemy(int num)
    {
        enemy_prefab = enemydata.GetEnemy_Prefab(num);    // 敵データの内部クラスから取得
    }

    // 敵オブジェクトのプレハブを返却する関数
    public GameObject GetEnemy_Prefab()
    {
        return this.enemy_prefab;
    }


    // 敵データの内部クラス
    [System.Serializable]
    private class Enemydata
    {

        [SerializeField] private GameObject[] enemy_prefab;    // 敵オブジェクトのプレハブ


        // 敵オブジェクトのプレハブを返却する関数
        public GameObject GetEnemy_Prefab(int num)
        {
            return this.enemy_prefab[num];
        }
    }
}
