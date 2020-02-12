using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBullet : MonoBehaviour
{
    // enemybulletを消去するまでの時間
    private float deleteTime = 0.75f; // (値は固定)

    void Start()
    {
        Destroy(gameObject, deleteTime);
        // 敵の弾との区別するための名前
        gameObject.name = "MyBullet";
    }

    void Update()
    {
        transform.Translate(0, 0.1f, 0);
    }
}
