using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    // trueなら移動する
    public bool wallcount = false;
    // reactorを入れるための配列
    private GameObject[] reactor;
    // doorを入れるための配列
    private GameObject[] door;
    // 初期の色を設定
    private SpriteRenderer startup;

    void Start()
    {
        startup = gameObject.GetComponent<SpriteRenderer>();
        // 初期の色は透明
        startup.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        if (wallcount)
        {
            transform.Translate(0.06f, 0, 0);   // 値は固定
        }

        // "Reactor"tagを持つものを収納
        reactor = GameObject.FindGameObjectsWithTag("Reactor");
        if (reactor.Length == 0)
        {
            Destroy(gameObject);
        }

        // "Door"tagを持つものを収納
        door = GameObject.FindGameObjectsWithTag("Door");
        if (door.Length == 0)
        {
             StartCoroutine(Movewall());
        }
    }

    IEnumerator Movewall()
    {// doorが破壊されてからの処理
        // alpha値を戻す
        startup.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        yield return new WaitForSeconds(5);
        wallcount = true;
    }

}
