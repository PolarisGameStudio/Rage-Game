using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using Steamworks;

public class PlayerNetwork : Photon.MonoBehaviour {

    public static string playerWhoIsIt;

    public GameObject myCamera;
    public GameObject LevelManager;
    public GameObject NetworkManager;
    public GameObject SpawnPlayer;
    public GameObject playerName;
    private GameObject fineLivello;
    private bool SonoSulFineLivello = false;
    public GameObject immaginepersonaggio;
    private GameObject immagineOcchi;
    private float ColoreOcchi = 1;
    private float moltiplicatoreColoreOcchi = 0.02f;
    private GameObject Decals;
    private GameObject tastoDisconnetti;
    private GameObject testoDead;
    private GameObject testoChièMorto;


    //AUDIO
    public AudioClip scoreggia;
    public AudioClip hello;
    public AudioClip imsorry;
    public AudioClip salto;
    public AudioClip morte;
    public AudioClip audioFineliv;
    bool canzone = false;


    public float lerpSmoothing = 10.0f;

    public static bool DEAD = false;
    bool DEADProvvisorio;
    static public int contatoremorti;
    private static string SonoMortoIo;
    public static bool possoMorire = true;
    public GameObject player;

    private static int cont,numeropersone, maxpersone;


    //ANIMATOR
    private Animator animator;
    private bool isGrounded;
    private float Speed, vSpeed;


    //SPAWN SANGUE
    public Transform Inizio, InizioSangue, FineSangue;
    public bool spotted = false;
    public GameObject sangue;
    private float respawnTimer, delayTime = 0.6f;

    public GameObject raycast;
    private RaycastHit2D SpawnPoint, SpawnPointSangue;
    private Vector2 fwd;

    private bool SangueSpawnato = false;
    public GameObject esplosioneSangue;
    public GameObject esplosioneSangueContinuato;

    public bool spawnato = false;

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;





    void Awake()
    {
        //DEAD = true;
        contatoremorti = 0;
        animator = GetComponent<Animator>();
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        LevelManager = GameObject.FindGameObjectWithTag("GestoreLivello");
        SpawnPlayer = GameObject.FindGameObjectWithTag("SpawnPlayer");
        NetworkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        fineLivello = GameObject.FindGameObjectWithTag("FineLivello");
        Decals = GameObject.FindGameObjectWithTag("Decals");
        GameObject canvas;
        canvas = GameObject.FindGameObjectWithTag("NetworkManager");
        canvas = canvas.transform.Find("Canvas").gameObject;
        tastoDisconnetti = canvas.transform.Find("Disconnetti").gameObject;
        testoDead = canvas.transform.Find("Dead").gameObject;
        testoChièMorto = canvas.transform.Find("ChièMorto").gameObject;
        immagineOcchi = transform.Find("Textures").gameObject;
        immagineOcchi = immagineOcchi.transform.Find("FurBall_Jump_11_256_1024_0").gameObject;
        //immagineOcchi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        DEAD = false;
        possoMorire = true;
        spawnato = true;
        SangueSpawnato = false;

        if (photonView.isMine)
        {
            //photonView.RPC("RemoveChild", PhotonTargets.All);
            if (PlayerPrefs.HasKey("IDColor"))
            {
                photonView.RPC("CambiaColorePersonaggio", PhotonTargets.AllBuffered, PlayerPrefs.GetInt("IDColor"));
            }
            else photonView.RPC("CambiaColorePersonaggio", PhotonTargets.AllBuffered, 0);
        }
    }


	void Start () {
        fwd = transform.TransformDirection(Vector2.down);
        player = NetworkManager.GetComponent<NetworkManager>().player;

        correctPlayerPos = SpawnPlayer.transform.position;
        correctPlayerRot = Quaternion.identity;


        if (photonView.isMine)
        {
            transform.parent = null;
            photonView.RPC("Contatore", PhotonTargets.All, 0);
            cont = 0;

            photonView.RPC("TaggedPlayer", PhotonTargets.AllBuffered, PhotonNetwork.playerName);
            myCamera.GetComponent<CameraFollowPlayer>().enabled = true;
            //myCamera.GetComponent<CameraFit>().enabled = true;
            player.GetComponent<Controller>().enabled = true;
            tastoDisconnetti.SetActive(true);
            //LevelManager.GetComponent<GestoreLivello>().enabled = true;
        }
        else
        {
            //playerName.GetComponent<TextMesh>().text = ;
        }


	}
	
	// Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.room.playerCount > maxpersone)
        {
            maxpersone = PhotonNetwork.room.playerCount;

            transform.parent = null;
            /*ColoreOcchi = 1f;
            immagineOcchi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);*/
            contatoremorti = 0;
            testoDead.GetComponent<Text>().text = "DEAD: " + contatoremorti;
        }
        else if (maxpersone > PhotonNetwork.room.playerCount)
        {
            photonView.RPC("Contatore", PhotonTargets.All, 0);
            maxpersone = PhotonNetwork.room.playerCount;

            transform.parent = null;
            /*ColoreOcchi = 1f;
            immagineOcchi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);*/
            cont = 0;
            contatoremorti = 0;
            testoDead.GetComponent<Text>().text = "DEAD: " + contatoremorti;
        }


        /*if (photonView.isMine && PhotonNetwork.room.playerCount < 2)
        {
            GetComponent<Controller>().enabled = false;
        }
        else if (photonView.isMine && PhotonNetwork.room.playerCount == 2)
        {
            GetComponent<Controller>().enabled = true;
        }*/

        //Raycasting();
        Debug.DrawRay(raycast.transform.position, raycast.transform.forward, Color.red);

        respawnTimer += Time.deltaTime;
        if (DEADProvvisorio == true && photonView.isMine && possoMorire)
        {
            DEAD = true;
        }

        if (respawnTimer > delayTime && spawnato)
        {
            photonView.RPC("SincronizzaEsplosione", PhotonTargets.All);
            respawnTimer = 0.0f;
        }

        if (!photonView.isMine)
        {
            animator.SetFloat("Speed", Speed);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("vSpeed", vSpeed);
            /*transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 8);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 8);*/
        }


        //Debug.Log(numeropersone);
        if (DEAD && photonView.isMine)
        {
            GetComponent<AudioSource>().clip = morte;
            GetComponent<AudioSource>().Play();
            photonView.RPC("SpawnSanguePaint", PhotonTargets.AllBuffered);
            photonView.RPC("Dead", PhotonTargets.All, true);
            testoDead.GetComponent<Text>().text = "DEAD: " + contatoremorti;
            DEAD = false;

            int numdead = PlayerPrefs.GetInt("NumeroDead");
            numdead++;
            PlayerPrefs.SetInt("NumeroDead", numdead);
            //photonView.RPC("SettaDead", PhotonTargets.All, false);
        }

        else
        {
            //transform.position = Vector3.Lerp(transform.position, this.realPosition, Time.deltaTime * 8);
            //transform.rotation = Quaternion.Lerp(transform.rotation, this.realRotation, Time.deltaTime * 20);
        }


        if (numeropersone == PhotonNetwork.room.playerCount && SonoSulFineLivello)
        {
            if (photonView.isMine)
            {
                GetComponent<Controller>().enabled = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                GetComponent<AudioSource>().clip = audioFineliv;
                if (!GetComponent<AudioSource>().isPlaying && canzone == false) { canzone = true; GetComponent<AudioSource>().Play(); }
            }
            StartCoroutine(fineLiv());
        }

        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.Alpha0))
            {
                SteamUserStats.ResetAllStats(true);
                SteamUserStats.RequestCurrentStats();
            }
        }



    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(animator.GetFloat("Speed"));
            stream.SendNext(animator.GetBool("IsGrounded"));
            stream.SendNext(animator.GetFloat("vSpeed"));
            stream.SendNext(DEAD);
            /*stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);*/
        }
        else
        {
            //Network player, receive data
            Speed = (float)stream.ReceiveNext();
            isGrounded = (bool)stream.ReceiveNext();
            vSpeed = (float)stream.ReceiveNext();
            this.DEADProvvisorio = (bool)stream.ReceiveNext();
            /*this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();*/
        }
    }
    

    void Morte(){
        if (possoMorire)
        {
            DEAD = true;
            possoMorire = false;
        }
        testoChièMorto.SetActive(true);
        testoChièMorto.GetComponent<Text>().text = SonoMortoIo;
        immaginepersonaggio.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        playerName.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DopoMorte());
        //player.GetComponent<PlayerNetwork>().DEAD = dead;
    }

    IEnumerator DopoMorte()
    {
        //if(photonView.isMine)photonView.RPC("Dead", PhotonTargets.All, false);
        yield return new WaitForSeconds(3);
        transform.position = new Vector2(SpawnPlayer.transform.position.x, SpawnPlayer.transform.position.y);
        transform.rotation = SpawnPlayer.transform.rotation;
        immaginepersonaggio.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Controller>().enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        playerName.GetComponent<MeshRenderer>().enabled = true;
        SonoMortoIo = "";
        testoChièMorto.GetComponent<Text>().text = "";
        testoChièMorto.SetActive(false);
        /*if (ColoreOcchi > moltiplicatoreColoreOcchi)
        {
            ColoreOcchi = ColoreOcchi - moltiplicatoreColoreOcchi;
            Debug.Log(ColoreOcchi);
            immagineOcchi.GetComponent<SpriteRenderer>().color = new Color(1, ColoreOcchi, ColoreOcchi, 1);
        }*/

        yield return new WaitForSeconds(1);
        possoMorire = true;
        spawnato = true; SangueSpawnato = false;
    }

    IEnumerator fineLiv()
    {
        yield return new WaitForSeconds(3);

        int numliv = PlayerPrefs.GetInt("NumeroLivelliCompletati");
        numliv++;
        PlayerPrefs.SetInt("NumeroLivelliCompletati", numliv);
        /*if (fineLivello.GetComponent<FineLivello>().nomeLivello == "Menu")
        {
            PhotonNetwork.Disconnect();
        }*/
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LoadLevel(fineLivello.GetComponent<FineLivello>().nomeLivello);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Spine" && photonView.isMine && possoMorire)
        {
            possoMorire = false;
            DEAD = true;
            photonView.RPC("SettaIlColpevole", PhotonTargets.AllBuffered, PhotonNetwork.playerName);
            Debug.Log("Morto");
        }
        else if (coll.transform.tag == "FineLivello")
        {
            if (photonView.isMine) SonoSulFineLivello = true;
            cont++;
            photonView.RPC("Contatore", PhotonTargets.All, cont);
        }
        else if (coll.transform.tag == "MovingPlatform" && photonView.isMine)
        {
            //photonView.RPC("AddChild", PhotonTargets.AllBuffered, coll.transform.GetComponent<PhotonView>().viewID);
        }
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.transform.tag == "Spine" && photonView.isMine && possoMorire)
		{
			possoMorire = false;
			DEAD = true;
            photonView.RPC("SettaIlColpevole", PhotonTargets.AllBuffered, PhotonNetwork.playerName);
			Debug.Log("Morto");
		}
       /* else if (coll.transform.tag == "MovingPlatform" && photonView.isMine)
        {
            photonView.RPC("AddChild", PhotonTargets.AllBuffered, coll.transform.GetComponent<PhotonView>().viewID);
        }*/
	}

    void OnTriggerStay2D(Collider2D coll)
    {

    }

	void OnTriggerExit2D(Collider2D coll){
		/*if (coll.transform.tag == "MovingPlatform" && photonView.isMine)
		{
			photonView.RPC("RemoveChild", PhotonTargets.AllBuffered);
		}*/
	}

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "FineLivello")
        {
            if(photonView.isMine) SonoSulFineLivello = false;
            cont--;
            photonView.RPC("Contatore", PhotonTargets.All, cont);
        }
        else if(coll.transform.tag == "MovingPlatform" && photonView.isMine){
            //photonView.RPC("RemoveChild", PhotonTargets.All);
        }

    }



    public void OnGUI()
    {
        GUILayout.Space(10);
        
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
            photonView.RPC("SpawnSangue", PhotonTargets.All);
        }
    }

    [PunRPC]
    void SettaDead(bool dead)
    {
        DEAD = false;
    }

    [PunRPC]
    void SettaIlColpevole(string nickname)
    {
        SonoMortoIo = nickname;
    }

    [PunRPC]
    void Dead(bool dead)
    {
        Debug.Log("SAD");
        //DEAD = dead;
        Morte();
        //if (DEAD) Morte();
    }

    [PunRPC]
    void TaggedPlayer(string playerID)
    {
        playerWhoIsIt = playerID;
        playerName.GetComponent<TextMesh>().text = playerWhoIsIt;
        Debug.Log("TaggedPlayer: " + playerID);
    }

    [PunRPC]
    void Scoreggia()
    {
        GetComponent<AudioSource>().clip = scoreggia;
        if(!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }


    [PunRPC]
    void Hello()
    {
        GetComponent<AudioSource>().clip = hello;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }


    [PunRPC]
    void ImSorry()
    {
        GetComponent<AudioSource>().clip = imsorry;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }

    [PunRPC]
    void SuonoSalto()
    {
        Debug.Log("Ho Saltato");
        GetComponent<AudioSource>().clip = salto;
        GetComponent<AudioSource>().Play();
    }




    [PunRPC]
    void Contatore(int numero)
    {
        numeropersone = numero;
    }

    [PunRPC]
    void SpawnSangue()
    {
        if (spawnato && photonView.isMine) PhotonNetwork.Instantiate("Sangue", SpawnPoint.point, new Quaternion(0, 0, 0, 0), 0, null);
        SangueSpawnato = true;
        esplosioneSangue.SetActive(true);
        spawnato = false;
    }

    [PunRPC]
    void SincronizzaEsplosione()
    {
        esplosioneSangue.SetActive(false);
    }


    [PunRPC]
     void SpawnSanguePaint()
    {
        if (photonView.isMine && spawnato) contatoremorti++;
        Ray ray = new Ray(raycast.transform.position, raycast.transform.forward);

        RaycastHit hit;

        if (spawnato && Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("SANGUEEEEEEEEEEEEEEEE");
            // Paint!
            Color color = Color.red;
            if(photonView.isMine)Decals.GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, color, 1); // Step back a little
            spawnato = false;
        }
    }

    [PunRPC]
    void CambiaColorePersonaggio(int i)
    {
        switch (i)
        {
            case 0: immagineOcchi.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255); break;
            case 1: immagineOcchi.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255); break;
            case 2: immagineOcchi.GetComponent<SpriteRenderer>().color = new Color32(0, 23, 255, 255); break;
            case 3: immagineOcchi.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 44, 255); break;
        }
    }

    [PunRPC]
    void AddChild(int ID)
    {
        Transform MovingPlatform = PhotonView.Find(ID).gameObject.GetComponent<Transform>();
        //MovingPlatform = MovingPlatform.transform.parent;
        //GetComponent<Rigidbody2D>().isKinematic = true;
        transform.parent = MovingPlatform.transform;
    }

    [PunRPC]
    void RemoveChild()
    {
        //GetComponent<Rigidbody2D>().isKinematic = false;
        transform.parent = null;
    }
}
