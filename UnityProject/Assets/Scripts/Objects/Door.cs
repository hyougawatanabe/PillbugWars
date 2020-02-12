using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // 体力を指定する変数
    public int health;
    // ダメージのチェック
    private bool damagecheck = false;
    // ダメージ時間計測用
    private float damagecount = 0.0f;
    // ダメージを受けた時の色変更用
    private SpriteRenderer damage;
    // 開始時door落下の時間調整用
    private float startTime = 0.0f;
    // 爆破effectを入れる用
    public GameObject explosion;
    // 爆破effectの位置指定用
    [SerializeField]
    private Transform expPosition;
    // soundの配列
    private AudioSource[] sounds;

    void Start()
    {
        damage = gameObject.GetComponent<SpriteRenderer>();
        sounds = gameObject.GetComponents<AudioSource>();
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

        startTime += Time.deltaTime;
        // ゲームスタート時少し時間がたってからdoorが落下
        if(startTime >= 0.7f)
        {
            if (transform.position.y >= 3.2f)
            {
                transform.Translate(0.0f, -0.2f, 0.0f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // door消滅処理
        if (health == 0)
        {
            StartCoroutine("BreakCount");
        }

        if (collider.gameObject.name == "MyBullet")
        {
            health--;
            damagecheck = true;
            Destroy(collider.gameObject);
        }
    }

    IEnumerator BreakCount()
    {// 破壊処理
        sounds[0].Play();
        yield return new WaitForSeconds(0.3f);
        Instantiate(explosion, expPosition.position, transform.rotation);
        Destroy(gameObject);
    }
}
