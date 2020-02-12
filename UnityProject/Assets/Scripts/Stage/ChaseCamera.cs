using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    /// <summary>
    /// 追尾カメラを表します。
    /// </summary>
    public class ChaseCamera : MonoBehaviour
    {
        // 追尾対象(プレイヤー)
        Transform target;

        void Start() {
            // 他のゲームオブジェクトを参照
            target = GameObject.FindGameObjectWithTag("Player").transform;

            // 追尾対象が未指定の場合は"Player"タグのオブジェクトで設定
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        void Update() {
            if (target != null) {
                var position = Camera.main.transform.position;
                position.x = target.position.x;
                position.y = target.position.y+2;
                //position.z = target.position.z;
                Camera.main.transform.position = position;
            }
        }
    }
}