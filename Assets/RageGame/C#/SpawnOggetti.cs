using UnityEngine;
using System.Collections;

public class SpawnOggetti : Photon.MonoBehaviour {

	public string[] oggetti;
	public GameObject spawn;
	private bool possoSpawnare = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerNetwork.DEAD)
			possoSpawnare = true;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && possoSpawnare)
        {
            if (coll.transform.GetComponent<PhotonView>().isMine)
            {
                possoSpawnare = false;
                for (int i = 0; i < oggetti.Length; i++)
                {
                    PhotonNetwork.Instantiate(oggetti[i], spawn.transform.position, spawn.transform.rotation, 0);
                }
            }
        }
    }
}
