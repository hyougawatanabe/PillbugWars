using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
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

    // 最初の移動をカウントする用
    float interval_ = 0.0f;
    // 最初の移動を調整する用
    public float count = 2.0f;
    // speed,start,endの値の初期設定
    public float speed_ = 0.1f;
    public float StartPosition = 0.0f;
    public float EndPosition = 3.0f;
    // 方向切り替え用
    bool CheckPosition = false;

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

        interval_ += Time.deltaTime;
        if (interval_ >= count)
        {
            mvEnemy();
        }

        // "Reactor"tagを持つものを収納
        reactor = GameObject.FindGameObjectsWithTag("Reactor");
        if (reactor.Length == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        health--;
        damagecheck = true;

        // 敵(enemy)消滅処理
        if (health <= 0)
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

    void mvEnemy()
    {
        // 敵(enemy)を移動させる
        // CheckPositionがtrueならプラス方向, falseならマイナス方向
        if (CheckPosition)
        {
            transform.Translate(0, speed_ * 0.02f, 0);
        }
        else
        {
            transform.Translate(0, -speed_ * 0.02f, 0);
        }

        // enemyのpositionによってCheckPositionを切り替え
        if (transform.position.y <= StartPosition)
        {
            CheckPosition = true;
        }
        else if (transform.position.y >= EndPosition)
        {
            CheckPosition = false;
        }
    }
}
