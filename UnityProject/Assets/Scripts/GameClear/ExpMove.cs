using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ExpMove : MonoBehaviour
{
    // 爆破動作(イラストの表示,非表示を繰り返す)の調整用
    private float SeekCount = 0.0f;
    public float appearTime = 0.0f;

    // 爆破イラストの色変更用
    [SerializeField]
    private Image exColor;
    // playerのイラストの位置を確認する用
    [SerializeField]
    private Transform mcPosition;
    // trueでイラストの表示,falseで非表示
    private bool expTime = true;

    void Start()
    {
        // 初期の色は透明
        exColor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        // 爆破動作をさせる
        if(expTime)
        {
            if (SeekCount >= appearTime)
            {
                // イラストの表示
                exColor.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            if (SeekCount >= appearTime + 0.3f)
            {
                // イラストを透明にする(非表示)
                exColor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                SeekCount = 0.0f;
            }
        }

        if(mcPosition.position.x <= 500)
        {
            // 爆破動作をやめさせる
            expTime = false;
            // 爆破イラストを透明にする
            exColor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else
        {
            SeekCount += Time.deltaTime;
        }
    }
}
