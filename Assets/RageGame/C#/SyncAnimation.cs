using UnityEngine;
using System.Collections;

public class SyncAnimation : MonoBehaviour {

    public string clipName = "Swing01";
    public string clipNameReverse = "Swing01 1";
    public float speed = 1;
    bool sinistra = false;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().speed = speed;

    }
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(clipName) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                GetComponent<Animator>().SetBool("Sinistra", true);
            else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(clipNameReverse) && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                GetComponent<Animator>().SetBool("Sinistra", false);
        }

	}
}
