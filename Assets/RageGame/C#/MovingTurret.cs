using UnityEngine;
using System.Collections;

public class MovingTurret : MonoBehaviour {

    public Transform cannaTurret;
    public bool canShoot = false;

    private SpriteRenderer hole;

    private Quaternion rotation;

    void Awake()
    {
        hole = transform.Find("Cannon").transform.FindChild("Hole").GetComponent<SpriteRenderer>();
        rotation = cannaTurret.rotation;
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            canShoot = true;
            FollowPlayer(coll);
            hole.color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            canShoot = false;
            hole.color = Color.black;
            cannaTurret.rotation = rotation;
        }
    }

    void FollowPlayer(Collider2D coll)
    {
        Vector3 toTarget = coll.transform.position - cannaTurret.position;
        float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg - 180;
        cannaTurret.rotation = Quaternion.Euler(0, 0, angle);
    }
}
