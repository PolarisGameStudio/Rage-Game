using UnityEngine;
using System.Collections;

public class PiattaformaCadente : MonoBehaviour {

    private Vector2 position;
    private Quaternion rotation;
    private bool flag = true, eroMorto;

	// Use this for initialization
	void Start () {
        position = transform.position;
        rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	    if(PlayerNetwork.DEAD){
            GetComponent<Rigidbody2D>().isKinematic = true;
            transform.position = position;
            transform.rotation = rotation;
            eroMorto = true;
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player" && !PlayerNetwork.DEAD && flag)
        {
            StartCoroutine((Timer()));
        }

    }

    IEnumerator Timer()
    {
        if (!PlayerNetwork.DEAD)
        {
            flag = false;
            yield return new WaitForSeconds(2f);
            GetComponent<Rigidbody2D>().isKinematic = false;
            StartCoroutine(Timer2());
        }
    }

    IEnumerator Timer2()
    {
       yield return new WaitForSeconds(2f);
       flag = true;
       if (eroMorto)
       {
           GetComponent<Rigidbody2D>().isKinematic = true;
           transform.position = position;
           transform.rotation = rotation;
           eroMorto = false;
       }
    }
}
