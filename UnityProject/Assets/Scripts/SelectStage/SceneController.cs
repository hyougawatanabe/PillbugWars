using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RunGame.SelectStage
{
    /// <summary>
    /// 『ステージ選択画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// 「StageButton」の親オブジェクトを指定します。
        /// </summary>
        [SerializeField]
        private Transform buttons = null;
        /// <summary>
        /// ボタン選択時の表示スケールを指定します。
        /// </summary>
        [SerializeField]
        private Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1);

        // soundを入れる用
        [SerializeField]
        private AudioSource selectAudio;

        // selectのイラストの色変更用
        [SerializeField]
        private Image s0Color;
        [SerializeField]
        private Image s1Color;
        [SerializeField]
        private Image s2Color;
        [SerializeField]
        private Image s3Color;

        // 現在選択されているボタンを示すインデックス
        int selectedIndex = 0;

        void Start()
        {
            // GameControllerからステージ名一覧を取得
            var stageNames = GameController.Instance.StageNames;
            // buttons配列から各ボタンをループ処理
            for (int index = 0; index < buttons.childCount; index++)
            {
                var button = buttons.GetChild(index);
            }
        }

        // 前回の"Vertical"入力を保存
        float lastVertical = 0.0f;

        void Update()
        {
            // y軸入力[-1.0f, 0.0f, +1.0f]
            var vertical = Input.GetAxisRaw("Vertical");

            // 「Enter」キーが押された場合
            if (Input.GetButtonUp("Submit"))
            {
                // 『ステージ画面』へシーン遷移
                Stage.SceneController.StageNo = selectedIndex;
                SceneManager.LoadScene("Stage");
                return;
            }
            // 'uparrow'キーが押された場合
            else if (lastVertical < 0.5f && vertical > 0.5f)
            {
                selectAudio.Play();
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = 0;
                }
            }
            // 'downarrow'キーが押された場合
            else if (lastVertical > -0.5f && vertical < -0.5f)
            {
                selectAudio.Play();
                selectedIndex++;
                if (selectedIndex > buttons.childCount - 1)
                {
                    selectedIndex = buttons.childCount - 1;
                }
            }

            // buttons配列から各ボタンをループ処理
            for (int index = 0; index < buttons.childCount; index++)
            {
                var button = buttons.GetChild(index);
                // 選択中のボタンは拡大表示
                if (index == selectedIndex)
                {
                    button.localScale = selectedScale;
                }
                // 非選択中のボタンは通常表示
                else
                {
                    button.localScale = Vector3.one;
                }
            }

            // select buttonの色変更(選択時に明るく表示)
            if(selectedIndex == 0)
            {
                s0Color.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                s1Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s2Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s3Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            else if(selectedIndex == 1)
            {
                s0Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s1Color.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                s2Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s3Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            else if (selectedIndex == 2)
            {
                s0Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s1Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s2Color.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                s3Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            else if (selectedIndex == 3)
            {
                s0Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s1Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s2Color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                s3Color.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            // "Vertical"入力を更新
            lastVertical = vertical;
        }
    }
}