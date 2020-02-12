using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame.GameClear
{
    /// <summary>
    /// 『ゲームクリアー画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        void Update()
        {
            // 「Enter」キーが押された場合
            if (Input.GetButtonUp("Submit"))
            {
                // 『タイトル画面』へシーン遷移
                SceneManager.LoadScene("Title");
            }
        }
    }
}