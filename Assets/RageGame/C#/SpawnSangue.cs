using UnityEngine;
using System.Collections;

public class SpawnSangue : Photon.MonoBehaviour{

    public Transform Inizio, Fine;
    public bool spotted = false;
    public GameObject sangue;
    private float respawnTimer,delayTime = 0.6f;

    private RaycastHit2D SpawnPoint;
    private Vector2 fwd;

    private bool SangueSpawnato = false;
    public GameObject esplosioneSangue;
    public GameObject esplosioneSangueContinuato;

    private bool spawnato = false;


	// Use this for initialization
	void Start () {
	    fwd = transform.TransformDirection(Vector2.down);
	}
	
	// Update is called once per frame
	void Update () {
        Raycasting();
        if (PlayerNetwork.DEAD) { spawnato = true; SangueSpawnato = false; }
	        respawnTimer += Time.deltaTime;
        if (respawnTimer > delayTime && spawnato)
        {
		    esplosioneSangue.SetActive(false); 
		    respawnTimer = 0.0f;
	    }
	        /*if(PlayerNetwork.DEAD){
		        esplosioneSangueContinuato.GetComponent<ParticleRenderer>().enabled = false;
	        }
	        else{
		        esplosioneSangueContinuato.GetComponent<ParticleRenderer>().enabled = true;
	        }*/
	    }

    
    void Raycasting()
    {
        //Debug.DrawLine(Inizio.position, Fine.position, Color.red);
        Debug.DrawRay(Inizio.transform.position, fwd * 15, Color.green);
        //spotted = Physics2D.Raycast(Inizio.transform.position, fwd, 15 , 1 << LayerMask.NameToLayer("Terreno"));
        SpawnPoint = Physics2D.Raycast(Inizio.transform.position, fwd, 15, 1 << LayerMask.NameToLayer("Terreno"));
        if (PlayerNetwork.DEAD && !SangueSpawnato && /*Physics2D.Raycast(Inizio.transform.position, fwd, 15, 1 << LayerMask.NameToLayer("Terreno"))*/ SpawnPoint)
        {
            //Instantiate(sangue,SpawnPoint.point, sangue.transform.rotation);    //Instantiate(sangue,);
            photonView.RPC("SangueATerra", PhotonTargets.All);            
        }
    }

    [PunRPC]
    void SangueATerra()
    {
        if(spawnato && photonView.isMine)PhotonNetwork.Instantiate("Sangue", SpawnPoint.point, new Quaternion(0, 0, 0, 0), 0, null);
        SangueSpawnato = true;
        esplosioneSangue.SetActive(true);
        spawnato = false;
    }

    IEnumerator WaitBloodContinuo()
    {
        yield return new WaitForSeconds(5f);
	    esplosioneSangueContinuato.GetComponent<ParticleRenderer>().enabled = true;
    }
}
