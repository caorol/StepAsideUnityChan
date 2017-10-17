using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	private Animator animator;
	private Rigidbody _rigidbody;
	// 前進するための力
	private float forwardForce = 800.0f;
	// 左右に移動するための力
	private float turnForce = 500.0f;
	// ジャンプするための力
	private float upForce = 500.0f;
	// 左右の移動範囲
	private float movableRange = 3.4f;
	// 動きを減速させる係数
	private float coefficient = 0.95f;
	// ゲーム終了判定
	private bool isEnd = false;
	private GameObject stateText;
	private GameObject scoreText;
	private int score = 0;
	// ボタン押下判定
	private bool isLeftButtonDown = false;
	private bool isRightButtonDown = false;

	void Start () {
		this.animator = GetComponent<Animator> ();
		this._rigidbody = GetComponent<Rigidbody> ();
		this.stateText = GameObject.Find ("GameResultText");
		this.scoreText = GameObject.Find ("ScoreText");

		// 走るアニメーション開始
		this.animator.SetFloat("Speed", 1);
	}
	
	void Update () {
		// ゲーム終了時減速
		if (this.isEnd) {
			forwardForce *= coefficient;
			turnForce *= coefficient;
			upForce *= coefficient;
			animator.speed *= coefficient;
		}

		this._rigidbody.AddForce (this.transform.forward * this.forwardForce);

		// キー && ボタン移動
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLeftButtonDown) && -this.movableRange < this.transform.position.x) {
			this._rigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRightButtonDown) && this.movableRange > this.transform.position.x) {
				this._rigidbody.AddForce (this.turnForce, 0, 0);
		}

		// Jump ステートの場合は Jump に false をセット
		if (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.animator.SetBool ("Jump", false);
		}

		// スペースが押されたらジャンプ
		if (Input.GetKey (KeyCode.Space) && this.transform.position.y < 0.5f) {
			this.animator.SetBool ("Jump", true);
			this._rigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	void OnTriggerEnter(Collider other) {
		// 障害物に衝突
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficCornTag") {
			isEnd = true;
			// game over 表示
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}

		// ゴールに到達
		if (other.gameObject.tag == "GoalTag") {
			isEnd = true;
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
		}

		// コインに衝突
		if (other.gameObject.tag == "CoinTag") {
			// score 加算
			score += 10;
			scoreText.GetComponent<Text> ().text = string.Format ("Score {0} pt", score);

			// パーティクル再生
			GetComponent<ParticleSystem>().Play();

			// 接触したコインオブジェクトを破棄
			Destroy (other.gameObject);
		}
	}

	// ジャンプボタン押下
	public void GetMyJumpButtonDown() {
		if (this.transform.position.y < 0.5f) {
			this.animator.SetBool ("Jump", true);
			this._rigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	// 左ボタン押下
	public void GetMyLeftButtonDown() {
		this.isLeftButtonDown = true;
	}
	public void GetMyLeftButtonUp() {
		this.isLeftButtonDown = false;
	}

	// 右ボタン押下
	public void GetMyRightButtonDown() {
		this.isRightButtonDown = true;
	}
	public void GetMyRightButtonUp() {
		this.isRightButtonDown = false;
	}
}
