using UnityEngine;
using System.Collections;

public class AttivaAnimazione : MonoBehaviour {
    private int n_persone;
    bool flagInizio = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.inRoom)
        {
            if (!flagInizio) { n_persone = PhotonNetwork.room.playerCount; flagInizio = true; }
            if (PhotonNetwork.room.playerCount != n_persone) { GetComponent<Animator>().enabled = false; GetComponent<Animator>().applyRootMotion = false; n_persone = PhotonNetwork.room.playerCount; }
            else { GetComponent<Animator>().applyRootMotion = true; GetComponent<Animator>().enabled = true; }
            }
	}
}
