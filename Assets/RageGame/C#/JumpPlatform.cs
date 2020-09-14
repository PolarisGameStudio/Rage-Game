using UnityEngine;
using System.Collections;

public class JumpPlatform : MonoBehaviour {

    public Transform Inizio;
    public float bounceForce = 10;
    private RaycastHit2D SpawnPoint;
    private Vector2 fwd;

	// Use this for initialization
	void Start () {
	    fwd = transform.TransformDirection(Vector2.up);
	}
	
	// Update is called once per frame
	void Update () {
	Debug.DrawRay(Inizio.transform.position, fwd * 1, Color.green);
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //Debug.Log("ASD");
            //coll.rigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            //GetComponent<Animator>().enabled = true;
            Debug.DrawRay(Inizio.transform.position, fwd * 1, Color.green);
            SpawnPoint = Physics2D.Raycast(Inizio.transform.position, fwd, 1, 1 << LayerMask.NameToLayer("Player"));
            if (SpawnPoint)
            {
                GetComponent<Animator>().Play("JumpPlatform", -1, 0);
                coll.rigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll) { 
        //GetComponent<Animator>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SpawnPoint = Physics2D.Raycast(Inizio.transform.position, fwd, 1, 1 << LayerMask.NameToLayer("Player"));
            if (SpawnPoint)
            {

            }
        }
    }
}
