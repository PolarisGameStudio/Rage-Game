using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            coll.gameObject.GetComponent<Controller>().Ladder = true;
        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            coll.gameObject.GetComponent<Controller>().Ladder = false;
        }

    }
}
