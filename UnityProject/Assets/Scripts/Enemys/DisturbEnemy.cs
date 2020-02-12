using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbEnemy : MonoBehaviour
{
    // 体力を指定する変数
    public int health;
    // ダメージのチェック
    private bool damagecheck = false;
    // ダメージ時間計測用
    private float damagecount = 0.0f;
    // ダメージを受けた時の色変更用
    private SpriteRenderer damage;
    // 爆破effectを入れる用
    public GameObject explosion;
    // reactorを入れるための配列
    private GameObject[] reactor;

    // 攻撃する際のカウント用
    float sicle = 0.0f;
    // 攻撃頻度を調整する用
    public float counter = 3.0f;
    // enemyの攻撃位置の指定用
    [SerializeField]
    Transform Attackpoint_;
    // 攻撃する方向を指定(trueなら上向き,falseなら下向き)
    public bool CheckDirect = true;
    // enemybulletを入れる用
    public GameObject UpBullet;     // 上向きbullet
    public GameObject DownBullet;   // 下向きbullet

    void Start()
    {
        damage = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // damagecheckがfalseなら初期状態の色, trueなら赤色に変化する
        if (!damagecheck)
        {// ダメージを受けていない間
            damage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {// ダメージを受けている間
            damage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

            damagecount += Time.deltaTime;
            if (damagecount >= 0.1f)
            {
                damagecheck = false;
                damagecount = 0.0f;
            }
        }

        // 上下どちらの方向に攻撃するか判断 trueなら上向き, falseなら下向き
        if(CheckDirect)
        {
            sicle += Time.deltaTime;
            // 敵の弾を周期的に発射
            if (sicle >= counter)
            {
                sicle = 0.0f;
                EnemyShoot();   // 上向きに弾を撃つ
            }
        }
        else
        {
            sicle += Time.deltaTime;
            // 敵の弾を周期的に発射
            if (sicle >= counter)
            {
                sicle = 0.0f;
                EnemyShootExp();    // 下向きに弾を撃つ
            }
        }

        // "Reactor"tagを持つものを収納
        reactor = GameObject.FindGameObjectsWithTag("Reactor");
        if (reactor.Length == 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyShoot()
    {
        // 上向きenemybulletを出現させる
        Instantiate(UpBullet, Attackpoint_.position, Quaternion.identity);
    }
    void EnemyShootExp()
    {
        // 下向きenemybulletを出現させる
        Instantiate(DownBullet, Attackpoint_.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        health--;
        damagecheck = true;

        // 敵(enemy)消滅処理
        if (health <= 0)
        {
            // 爆破effectを出現させる
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
