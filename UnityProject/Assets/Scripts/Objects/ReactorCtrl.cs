using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorCtrl : MonoBehaviour
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
    // 爆破effectの位置指定用
    [SerializeField]
    private Transform One;
    [SerializeField]
    private Transform Two;
    [SerializeField]
    private Transform Three;
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        health--;
        damagecheck = true;

        // reactor消滅処理
        if (health == 0)
        {
            StartCoroutine("BreakCount");
        }

        // playerの撃った弾なら
        if (collider.gameObject.name == "MyBullet")
        {
            Destroy(collider.gameObject);
        }
    }

    IEnumerator BreakCount()
    {// 破壊処理
        Instantiate(explosion, One.position, transform.rotation);
        sounds[0].Play();
        //音の出るタイミングを調整
        yield return new WaitForSeconds(0.1f);
        sounds[1].Play();
        yield return new WaitForSeconds(0.2f);
        Instantiate(explosion, Two.position, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(explosion, Three.position, transform.rotation);
        sounds[0].Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
