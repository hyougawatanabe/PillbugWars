using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // bulletを消去するまでの時間
    private float deleteTime = 0.75f; // (値は固定)

    void Start()
    {
        Destroy(gameObject, deleteTime);
        // 敵の弾との区別するための名前
        gameObject.name = "MyBullet";
    }

    void Update()
    {
         transform.Translate(0.2f, 0, 0);
     }
}
