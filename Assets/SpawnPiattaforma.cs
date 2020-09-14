using UnityEngine;
using System.Collections;

public class SpawnPiattaforma : MonoBehaviour {

	public Transform SpawnPoint;
	public string playerPrefabName;
	bool flag = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (flag && PhotonNetwork.isMasterClient && PhotonNetwork.inRoom) {
			PhotonNetwork.InstantiateSceneObject(playerPrefabName, SpawnPoint.position, SpawnPoint.rotation, 0, null);
			flag = false;
		}
	}
}
