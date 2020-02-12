using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Escape : MonoBehaviour
{
    // playerのイラストの色変更用
    [SerializeField]
    private Image mcColor;

    void Update()
    {
        if(transform.position.x >= 500)
        {
            // イラストを斜め左下に移動
            transform.position += new Vector3(-3, -2);
            if (transform.position.x >= 1000)
            {
                // イラスト拡大
                transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
            }
            else
            {
                // イラスト縮小
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.0f);
            }
        }
        else
        {
            // playerのイラストを透明にする
            mcColor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
