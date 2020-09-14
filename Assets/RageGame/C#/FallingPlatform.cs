using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float fallDelay;
	private Vector2 platformPosition;
	private bool hoColliso = false;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		platformPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update (){
		if (!PlayerNetwork.possoMorire) {
			gameObject.transform.position = platformPosition;
			rb2d.isKinematic = true;
			GetComponent<BoxCollider2D>().isTrigger = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.CompareTag ("Player")) {
			hoColliso = true;
			StartCoroutine(Fall());
		}
	}

	IEnumerator Fall(){
		yield return new WaitForSeconds(fallDelay);
		rb2d.isKinematic = false;
		GetComponent<BoxCollider2D>().isTrigger = true;
		hoColliso = false;
		yield return 0;
	}
}
