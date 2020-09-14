using UnityEngine;
using System.Collections;

public class TestAnimazione : Photon.MonoBehaviour {

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!photonView.isMine && !PhotonNetwork.isMasterClient)
        {
            //transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 8);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 8f);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting && PhotonNetwork.isMasterClient)
        {
            // We own this player: send the others our data
            //stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else
        {
            // Network player, receive data
            //this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }

}
