﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ←追加

namespace RunGame.Title
{
    /// <summary>
    /// 『タイトル画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        void Update()
        {
            // 「Enter」キーが押された場合
            if (Input.GetButtonUp("Submit"))
            {
                // 『ステージ選択画面』へシーン遷移
                SceneManager.LoadScene("SelectStage");
            }
        }

        /// <summary>
        /// 「StartButton」をクリックした際に
        /// 呼び出されます。
        /// </summary>
        public void OnClickStartButton()
        {
            // 『ステージ選択画面』へシーン遷移
            SceneManager.LoadScene("SelectStage");
        }
    }
}