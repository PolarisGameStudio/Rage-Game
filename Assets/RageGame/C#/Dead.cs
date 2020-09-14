using UnityEngine;
using System.Collections;

public class Dead : Photon.MonoBehaviour {
	public float secondi = 2f;

	// Use this for initialization
	void Start () {
        photonView.RPC("Elimina", PhotonTargets.All);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Muori(){
		yield return new WaitForSeconds(secondi);
	}

    [PunRPC]
    void Elimina()
    {
        Destroy(gameObject, secondi);
    }
}
