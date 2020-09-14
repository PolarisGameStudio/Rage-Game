using UnityEngine;
using System.Collections;

public class FineLivello : Photon.MonoBehaviour {

    public string nomeLivello;
    /*public static int numeropersone, cont;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (numeropersone == 1)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel("RageGame");
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            cont++;
            photonView.RPC("Contatore", PhotonTargets.All, cont);
        }

    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            cont--;
            photonView.RPC("Contatore", PhotonTargets.All, cont);
        }

    }

    [PunRPC]
    void Contatore(int numero)
    {
        numeropersone = numero;
    }*/
}
