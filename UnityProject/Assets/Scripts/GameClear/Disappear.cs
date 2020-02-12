using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Disappear : MonoBehaviour
{
    // イラストの色変更用
    [SerializeField]
    private Image disappearColor;
    // playerのイラストの位置を確認する用
    [SerializeField]
    private Transform mcPosition;

    void Update()
    {
        if(mcPosition.position.x <= 500)
        {
            // イラストを透明にする
            disappearColor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
