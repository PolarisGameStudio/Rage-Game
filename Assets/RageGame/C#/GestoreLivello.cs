using UnityEngine;
using System.Collections;

public class GestoreLivello : MonoBehaviour {

    public GameObject Player, SpawnPlayer;
    public bool DEAD = false;
    private float respawnTimer,delayTime = 3f;
    GameObject player;

	// Use this for initialization
    void Awake()
    {
        //Spawn();    //Instantiate(sangue,);
    }
    
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void Spawn()
    {
        Instantiate(Player, SpawnPlayer.transform.position, SpawnPlayer.transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        if (DEAD)
        {
	        player.GetComponent<Controller>().enabled = false;
            player.GetComponent<SpriteRenderer>().enabled = false;
	        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            //player.GetComponent<Controller>().enabled = false;
            //player.SetActive(false);
            respawnTimer += Time.deltaTime;
            if (respawnTimer > delayTime)
            {
                player.transform.position = new Vector2(SpawnPlayer.transform.position.x, SpawnPlayer.transform.position.y);
                player.transform.rotation = SpawnPlayer.transform.rotation;
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<Controller>().enabled = true;
		        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                DEAD = false;
                respawnTimer = 0.0f;
            }
            //StartCoroutine(Rispawna())Y
            //Application.LoadLevel("RageGame");
            /*player.transform.position = new Vector2(SpawnPlayer.transform.position.x, SpawnPlayer.transform.position.y);
            player.transform.rotation = SpawnPlayer.transform.rotation;
            DEAD = false;*/
        }

        /*if (Input.GetKey(KeyCode.R))
        {
            DEAD = true;
        }*/
	}

    IEnumerator Rispawna()
    {
        /*player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Controller>().enabled = false;*/
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(0.0f);
        //Spawn();
        DEAD = false;
        player.transform.position = new Vector2(SpawnPlayer.transform.position.x, SpawnPlayer.transform.position.y);
        player.transform.rotation = SpawnPlayer.transform.rotation;

        /*player.transform.position = new Vector3(0, 20, 0);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Controller>().enabled = true;*/


    }
}
