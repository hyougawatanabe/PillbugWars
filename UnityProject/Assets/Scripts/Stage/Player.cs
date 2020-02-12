using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    /// <summary>
    /// 『ダンゴムシ』を表します。
    /// </summary>
    public class Player : MonoBehaviour
    {
        // 通常の移動速度を指定します。
        [SerializeField]
        private float speed = 4;
        // ダッシュ時の移動速度を指定します。
        [SerializeField]
        private float dashSpeed = 8;
        // ジャンプの力を指定します。
        [SerializeField]
        private float jumpPower = 150;
        // 設置判定の際に判定対象となるレイヤーを指定します。
        [SerializeField]
        private LayerMask groundLayer = 0;
        // ダッシュの際のサウンドを指定します。
        [SerializeField]
        private AudioClip soundOnDash = null;
        // bulletを入れる用
        [SerializeField]
        private GameObject Bullet;
        // 発射する間隔設定用
        float interval = 0.0f;
        // playerの攻撃位置の指定用
        [SerializeField]
        private Transform Shotpoint_;
        // reactorを入れるための配列
        private GameObject[] reactor;
        // playerのdash判定(dash時の速さ変更をさせないためのもの)
        bool checkDash = false;
        // soundの配列
        private AudioSource[] sounds;
        // trueの時操作をできなくする
        private bool stopMove = false;

        /// <summary>
        /// プレイ中の場合はtrue、ステージ開始前またはゲームオーバー時にはfalse
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            set { isActive = value; }
        }
        bool isActive = false;

        // 着地している場合はtrue、ジャンプ中はfalse
        [SerializeField]
        bool isGrounded = false;
        // 止まっている場合はtrue、移動している場合はfalse
        [SerializeField]
        bool checkStop = false;
        // playerがやられたかチェックする用
        private bool endCheck = false;

        // AnimatorのパラメーターID
        static readonly int dashId = Animator.StringToHash("isDash");

        // ダッシュ状態の場合はtrue
        public bool IsDash {
            get { return isDash; }
            private set {
                isDash = value;
                // ダッシュ状態への遷移時
                if (value)
                {
                    // ダッシュアニメーションへ切り替え
                    animator.SetBool(dashId, true);
                    // サウンド再生
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                    audioSource.clip = soundOnDash;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                // 通常状態へ遷移する場合
                else
                {
                    // 通常アニメーションへ切り替え
                    animator.SetBool(dashId, false);
                    // サウンド停止
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                }
            }
        }
        bool isDash = false;

        // 設置判定用のエリア
        Vector3 groundCheckA, groundCheckB;

        // コンポーネントを事前に参照しておく変数
        Animator animator;
        new Rigidbody2D rigidbody;
        // サウンドエフェクト再生用のAudioSource
        AudioSource audioSource;

        void Start()
        {
            // 事前にコンポーネントを参照
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            sounds = gameObject.GetComponents<AudioSource>();

            // Box Collider 2Dの判定エリアを取得
            var collider = GetComponent<BoxCollider2D>();
            // コライダーの中心座標へずらす
            groundCheckA = collider.offset;
            groundCheckB = collider.offset;
            // コライダーのbottomへずらす
            groundCheckA.y += -collider.size.y * 0.5f;
            groundCheckB.y += -collider.size.y * 0.5f;
            // 範囲を決定
            Vector2 size = collider.size;
            size.x *= 0.75f;    // 横幅
            size.y *= 0.25f;    // 高さは4分の1
            // コライダーの横幅方向へ左右にずらす
            groundCheckA.x += -size.x * 0.5f;
            groundCheckB.x += size.x * 0.5f;
            // コライダーの高さ方向へ上下にずらす
            groundCheckA.y += -size.y * 0.5f;
            groundCheckB.y += size.y * 0.5f;
        }

        void Update()
        {
            if (IsActive)
            {
                // 転倒判定
                // 1. スピードが０になったとき
                // 2. かつ、着地できていない場合

                // 回転角度を取得[-360, 360]
                var rotationZ = transform.eulerAngles.z;
                // 角度が[-180, 180]で表されるように補正
                if (rotationZ > 180)
                {
                    rotationZ -= 360;
                }
                else if (rotationZ < -180)
                {
                    rotationZ += 360;
                }
                // 転倒条件フラグ(転倒状態:true)
                var isTumbled = (rotationZ > 70) || (rotationZ < -70);
                if (rigidbody.velocity.magnitude < 0.1f &&
                    !isGrounded && isTumbled)
                {
                    IsActive = false;
                    SceneController.Instance.GameOver();
                    // しびれてるアニメーションへ切り替え
                    animator.SetBool("isShock", true);
                    // ゲームオーバー時キー操作をさせなくする
                    stopMove = true;
                    // やられチェック
                    endCheck = true;
                }
            }

            if (!stopMove)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                // 接地している場合
                if (isGrounded)
                {
                    // 'shift''rightarrow'キーが押し下げられている場合はダッシュ処理
                    if (Input.GetButton("Fire3") && horizontal > 0.5f)
                    {
                        // x軸方向の移動
                        var velocity = rigidbody.velocity;
                        velocity.x = dashSpeed;
                        rigidbody.velocity = velocity;
                        // player移動中
                        checkStop = false;
                        // dash中
                        checkDash = true;
                        // 通常状態からダッシュ状態に切り替える場合
                        if (!IsDash)
                        {
                            IsDash = true;
                        }
                    }
                    // "Jump"入力でジャンプ処理
                    else if (Input.GetButtonDown("Jump"))
                    {
                        IsDash = false;
                        rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                        // ジャンプ状態に設定
                        isGrounded = false;
                        // player移動中
                        checkStop = false;
                        // jump音再生
                        sounds[2].Play();
                    }
                    else
                    {
                        IsDash = false;
                        // not dash中
                        checkDash = false;
                        // x軸方向の移動
                        var velocity = rigidbody.velocity;
                        velocity.x = speed;
                        rigidbody.velocity = velocity;
                    }
                }
                // 空中状態の場合
                else
                {
                    if (IsDash)
                    {
                        IsDash = false;
                        checkStop = true;
                    }
                }

                // 止まっている場合のみ弾を発射可能
                if (checkStop)
                {
                    // 'ctrl'キーで弾発射
                    if (Input.GetButtonDown("Fire1"))
                    {
                        // 連射を防ぐため少し時間を空けて弾を発射
                        if (interval >= 0.1f)
                        {
                            interval = 0.0f;
                            Shoot();
                            // shoot音再生
                            sounds[1].Play();
                        }
                    }
                }

                if (!checkDash)
                {
                    // 'rightarrow'で右に移動
                    if (!Input.GetButton("Fire3") && horizontal > 0.5f)
                    {
                        var velocity = rigidbody.velocity;
                        velocity.x = speed + 4;
                        rigidbody.velocity = velocity;
                        // player移動中
                        checkStop = false;
                    }
                    else
                    {
                        // player停止中
                        checkStop = true;
                    }
                }
            }
            interval += Time.deltaTime;

            // "Reactor"tagを持つものを収納
            reactor = GameObject.FindGameObjectsWithTag("Reactor");
            if (reactor.Length == 0)
            {
                // clearさせる
                SceneController.Instance.StageClear();
                // ゲームオーバー時キー操作をさせなくする
                stopMove = true;
            }

            // やられた後の処理
            if (endCheck)
            {
                StartCoroutine(Endplay());
            }
        }
        void Shoot()
        {
            // bulletを出現させる
            Instantiate(Bullet, Shotpoint_.position, Quaternion.identity);
        }

        /// <summary>
        /// 固定フレームレートで実行されるフレーム更新処理
        /// </summary>
        private void FixedUpdate()
        {
            // 着地判定
            // ワールド空間の位置へ移動
            var minPosition = groundCheckA + transform.position;
            var maxPosition = groundCheckB + transform.position;
            // minPositionとmaxPositionで指定した範囲内に
            // コライダーが存在するかどうかを判定
            isGrounded = Physics2D.OverlapArea(
                minPosition, maxPosition, groundLayer);
#if UNITY_EDITOR
            // デバッグ用にテストでラインを描画する
            Vector2 start, end;

            // TOP
            start.x = minPosition.x;
            end.x = maxPosition.x;
            start.y = maxPosition.y;
            end.y = maxPosition.y;
            Debug.DrawLine(start, end, Color.yellow);
            // BOTTOM
            start.x = minPosition.x;
            end.x = maxPosition.x;
            start.y = minPosition.y;
            end.y = minPosition.y;
            Debug.DrawLine(start, end, Color.yellow);
            // LEFT
            start.x = minPosition.x;
            end.x = minPosition.x;
            start.y = minPosition.y;
            end.y = maxPosition.y;
            Debug.DrawLine(start, end, Color.yellow);
            // RIGHT
            start.x = maxPosition.x;
            end.x = maxPosition.x;
            start.y = minPosition.y;
            end.y = maxPosition.y;
            Debug.DrawLine(start, end, Color.yellow);
#endif
        }

        /// <summary>
        /// このプレイヤーが他のオブジェクトのトリガー内に侵入した際に
        /// 呼び出されます。
        /// </summary>
        /// <param name="collider">侵入したトリガー</param>
        private void OnTriggerEnter2D(Collider2D collider)
        {
            // ゲームオーバー判定
            if (collider.tag == "GameOver")
            {
                // ゲームオーバー時キー操作をさせなくする
                stopMove = true;
                // しびれてるアニメーションへ切り替え
                animator.SetBool("isShock", true);
                // やられチェック
                endCheck = true;
                SceneController.Instance.GameOver();
            }
            else if (collider.tag == "Enemy")
            {
                // ゲームオーバー時キー操作をさせなくする
                stopMove = true;
                SceneController.Instance.GameOver();
                // しびれてるアニメーションへ切り替え
                animator.SetBool("isCrash", true);
                // やられチェック
                endCheck = true;
            }
            else if (collider.tag == "ShockWall")
            {
                // ゲームオーバー時キー操作をさせなくする
                stopMove = true;
                SceneController.Instance.GameOver();
                // しびれてるアニメーションへ切り替え
                animator.SetBool("isShock", true);
                // やられチェック
                endCheck = true;
            }
        }
        IEnumerator Endplay()
        {// playerを一定時間後消去
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}