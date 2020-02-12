using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // enemybulletを消去するまでの時間
    private float deleteTime = 1.25f; // (値は固定)
    // 体力を指定する変数
    public int bullethealth = 1;    // (値は固定)
    // 爆破effectを入れる用
    public GameObject explosion;

    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    void Update()
    {
        transform.Translate(-0.1f, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        bullethealth--;
        // 弾(enemybullet)消滅処理
        if (bullethealth <= 0)
        {
            // 爆破effectの起動
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        // playerの撃った弾なら
        if (collider.gameObject.name == "MyBullet")
        {
            Destroy(collider.gameObject);
        }
    }
}
