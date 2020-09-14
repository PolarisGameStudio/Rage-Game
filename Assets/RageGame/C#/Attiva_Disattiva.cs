using UnityEngine;
using System.Collections;

public class Attiva_Disattiva : MonoBehaviour {

    public GameObject[] OggettiDaAttivare;
    public GameObject[] OggettiDaDisattivare;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerNetwork.DEAD)
        {
            //Debug.Log(PlayerNetwork.DEAD);
            for (int i = 0; i < OggettiDaAttivare.Length; i++)
            {
                OggettiDaAttivare[i].SetActive(false);
            }
            for (int i = 0; i < OggettiDaDisattivare.Length; i++)
            {
                OggettiDaDisattivare[i].SetActive(true);
            }
        }
	}


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!PlayerNetwork.DEAD)
        {
            for (int i = 0; i < OggettiDaAttivare.Length; i++)
            {
                OggettiDaAttivare[i].SetActive(true);
            }
            for (int i = 0; i < OggettiDaDisattivare.Length; i++)
            {
                OggettiDaDisattivare[i].SetActive(false);
            }
        }
    }
}
