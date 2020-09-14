using UnityEngine;
using System.Collections;

public class AttivazioneOggetti : MonoBehaviour {

    public GameObject[] oggetti;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerNetwork.DEAD)
        {
            //Debug.Log(PlayerNetwork.DEAD);
            for (int i = 0; i < oggetti.Length; i++)
            {
                oggetti[i].SetActive(false);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (!PlayerNetwork.DEAD)
            {
                for (int i = 0; i < oggetti.Length; i++)
                {
                    oggetti[i].SetActive(true);
                }
            }
        }
    }
}
