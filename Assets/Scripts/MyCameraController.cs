using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {
	private GameObject unitychan;
	private float difference;
	public GameObject carPrefab;
	public GameObject coinPrefab;
	public GameObject conePrefab;
	private int startPosition = -255;
	private int goalPosition = 120;
	// アイテムを出す x 方向の範囲
	private float positionRange = 3.4f;
	// 
	private float paddingZ = 50;
	private float setItemPositionZ = 0;

	void Start () {
		this.unitychan = GameObject.Find ("unitychan");
		// unitychan とカメラの位置の差（z座標）
		this.difference = unitychan.transform.position.z - this.transform.position.z;
	}
	
	void Update () {
		this.transform.position = new Vector3 (0, this.transform.position.y, this.unitychan.transform.position.z - this.difference);
//		Debug.Log ("<color=yellow>position: " + this.transform.position + "</color>");

		// 一定距離毎にアイテムを生成
		int positionZ = Mathf.FloorToInt(this.transform.position.z);
		if (startPosition <= positionZ && (goalPosition - paddingZ) >= positionZ && setItemPositionZ != positionZ && (positionZ % 15 == 0)) {
			setItemPositionZ = positionZ;
			// どのアイテムを出すか
			int num = Random.Range (0, 10);
			if (num <= 1) {
				createCornObject ();
			} else {
				// レーン毎にアイテムを生成
				for (int j = -1; j < 2; j++) {
					int item = Random.Range (1, 11);
					if (1 <= item && item <= 6) {
						// コイン生成
						createCoinObject (positionRange * j);
					} else if (7 <= item && item <= 9) {
						// 車を生成
						createCarObject (positionRange * j);
					}
				}
			}
		}
	}

	void createCornObject() {
		for (float j = -1; j <= 1; j += 0.4f) {
			// コーンを x 軸方向い一直線に生成
			createObject (conePrefab, 4 * j, true);
		}
	}

	void createCoinObject(float positionX) {
		createObject (coinPrefab, positionX);
	}

	void createCarObject(float positionX) {
		createObject (carPrefab, positionX);
	}

	void createObject(GameObject baseObject, float positionX, bool isLine = false) {
		int offsetZ = isLine?0:Random.Range (-5, 6);
		GameObject newObject = Instantiate (baseObject) as GameObject;
		newObject.transform.position = new Vector3 (positionX, newObject.transform.position.y, this.transform.position.z + paddingZ + offsetZ);
	}
}
