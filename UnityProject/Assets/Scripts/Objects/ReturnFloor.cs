using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFloor : MonoBehaviour
{
    // playerと接触しているかどうかを判断する用
    bool CheckTouch = false;
    // floor落下のタイム計測用
    float LimitTime = 0.0f;
    // floor消去用
    public float DeadPosition = 0.0f;

    void Update()
    {
        if(CheckTouch)
        {
            LimitTime += Time.deltaTime;
        }

        // 一定時間後落下
        if(LimitTime >= 2.5f)
        {
            transform.Translate(0.0f, -0.1f, 0.0f);
        }

        if(transform.position.y <= DeadPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {// playerと接触したら
            CheckTouch = true;
            // floorを赤くする
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
