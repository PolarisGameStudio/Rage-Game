using UnityEngine;
using System.Collections;

public class ShootingTurret : MonoBehaviour {

    public bool staticTurret = true;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;


    private float nextFire;
    private Animator anim;

    void Awake()
    {
        if (staticTurret)
            GetComponent<MovingTurret>().enabled = false;
        else
            GetComponent<MovingTurret>().enabled = true;

        anim = transform.Find("Cannon").GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("canShoot", false);
        if (staticTurret)
        {
            if (PhotonNetwork.isMasterClient && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                PhotonNetwork.Instantiate("Bullet", shotSpawn.position, shotSpawn.rotation, 0);
                anim.SetBool("canShoot", true);
            }
        }
        else
        {
            if (PhotonNetwork.isMasterClient && Time.time > nextFire && GetComponent<MovingTurret>().canShoot)
            {
                nextFire = Time.time + fireRate;
                PhotonNetwork.Instantiate("Bullet", shotSpawn.position, shotSpawn.rotation, 0);
                anim.SetBool("canShoot", true);
            }
        }
    }
}
