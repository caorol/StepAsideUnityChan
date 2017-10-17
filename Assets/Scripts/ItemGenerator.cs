using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
	public GameObject carPrefab;
	public GameObject coinPrefab;
	public GameObject conePrefab;
	private int startPosition = -160;
	private int goalPosition = 120;
	// アイテムを出す x 方向お範囲
	private float positionRange = 3.4f;

	void Start () {
		// 一定距離毎にアイテムを生成
		for (int i = startPosition; i < goalPosition; i += 15) {
			// どのアイテムを出すか
			int num = Random.Range (0, 10);
			if (num <= 1) {
				for (float j = -1; j <= 1; j += 0.4f) {
					// コーンを x 軸方向い一直線に生成
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
				}
			} else {
				// レーン毎にアイテムを生成
				for (int j = -1; j < 2; j++) {
					int item = Random.Range (1, 11);
					int offsetZ = Random.Range (-5, 6);
					if (1 <= item && item <= 6) {
						// コイン生成
						GameObject coin = Instantiate (coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (positionRange * j, coin.transform.position.y, i + offsetZ);
					} else if (7 <= item && item <= 9) {
						// 車を生成
						GameObject car = Instantiate (carPrefab) as GameObject;
						car.transform.position = new Vector3 (positionRange * j, car.transform.position.y, i + offsetZ);
					}
				}
			}
		}
	}
	
}
