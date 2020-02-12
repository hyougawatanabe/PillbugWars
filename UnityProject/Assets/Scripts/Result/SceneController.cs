using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RunGame.Result
{
    /// <summary>
    /// 『リザルト画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// ステージの数を指定します。
        /// </summary>
        /// <remarks>チュートリアルも1ステージと数えます。</remarks>
        [SerializeField]
        private int stageCount = 3;

        /// <summary>
        /// 起動するシーン番号を取得または設定します。
        /// </summary>
        public static int StageNo {
            get { return stageNo; }
            set { stageNo = value; }
        }
        private static int stageNo = 0;

        /// <summary>
        /// 『STAGE #』表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private Text stageNoUI = null;

        /// <summary>
        /// 『ステージ名』表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject stageNameUI = null;

        /// <summary>
        /// クリアータイムを取得または設定します。
        /// </summary>
        public static float ClearTime {
            get { return clearTime; }
            set { clearTime = value; }
        }
        private static float clearTime = 99.99f;

        /// <summary>
        /// クリアータイム表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject clearTimeUI = null;

        /// <summary>
        /// ベストタイム表示用のUIを指定します。
        /// </summary>
        [SerializeField]
        private GameObject bestTimeUI = null;

        // イラスト表示変更用
        [SerializeField]
        private Image Exp_reactor;
        [SerializeField]
        private Image Exp_ship;

        void Start() {
            // UIを更新
            stageNoUI.text = StageNo.ToString();

            var stageNames = GameController.Instance.StageNames;
            Debug.AssertFormat(
                StageNo >= 0 && StageNo < stageNames.Length,
                "不正なStageNo : {0}が指定されました", StageNo);
            if (StageNo >= 0 && StageNo < stageNames.Length) {
                
            }
            // 範囲外のステージ番号が指定された場合
            else {
                // 最終ステージを仮表示してエラーをださない
                stageNameUI.GetComponentInChildren<Text>().text = stageNames[0];
            }

            clearTimeUI.GetComponentInChildren<Text>().text =
                ClearTime.ToString("00.00");
            bestTimeUI.GetComponentInChildren<Text>().text =
                GameController.Instance.BestTime.ToString("00.00");
        }

        void Update() {
            // 「Enter」キーが押された場合
            if (Input.GetButtonUp("Submit")) {
                // 次のステージ番号
                var nextStageNo = StageNo + 1;
                // 次のステージが存在する場合
                if (nextStageNo < stageCount) {
                    // 『ステージ画面』へシーン遷移
                    Stage.SceneController.StageNo = nextStageNo;
                    SceneManager.LoadScene("Stage");
                }
                // 最終ステージをクリアーした場合
                else {
                    // 『ゲームクリアー画面』へシーン遷移
                    SceneManager.LoadScene("GameClear");
                }
            }

            // ステージによってイラストを変更
            if(StageNo == 1)
            {
                Exp_reactor.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Exp_ship.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            else if(StageNo == 2)
            {
                Exp_reactor.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Exp_ship.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
            else if(StageNo == 3)
            {
                Exp_reactor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                Exp_ship.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                Exp_reactor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                Exp_ship.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }
    }
}