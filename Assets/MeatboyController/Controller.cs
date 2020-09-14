/********************************************************
 * 2D Meatboy style controller written entirely by Nyero.
 * 
 * Thank you for using this script, it makes me feel all
 * warm and happy inside. ;)
 *                             -Nyero
 * 
 * ------------------------------------------------------
 * Notes on usage:
 *     Please don't use the meatboy image, as your some
 * might consider it stealing.  Simply replace the sprite
 * used, and you'll have a 2D platform controller that is
 * very similar to meatboy.
 ********************************************************/
using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Controller : Photon.MonoBehaviour
{
    public class GroundState
    {
        private GameObject player;
        private float width;
        private float height;
        private float length;

        //GroundState constructor.  Sets offsets for raycasting.
        public GroundState(GameObject playerRef)
        {
            player = playerRef;
            width = player.GetComponent<Collider2D>().bounds.extents.x + 0.1f;
            height = player.GetComponent<Collider2D>().bounds.extents.y;
            length = 0.2f;
        }

        //Returns whether or not player is touching wall.
        public bool isWall()
        {
            bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length, 1 << LayerMask.NameToLayer("PossoRimbalzare"));
            bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length, 1 << LayerMask.NameToLayer("PossoRimbalzare"));

            if (left || right)
                return true;
            else
                return false;
        }

        //Returns whether or not player is touching ground.
        public bool isGround()
        {
            bool bottom1 = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, length, 1 << LayerMask.NameToLayer("Terreno"));
            bool bottom2 = Physics2D.Raycast(new Vector2(player.transform.position.x + (width - 0.2f), player.transform.position.y - height), -Vector2.up, length, 1 << LayerMask.NameToLayer("Terreno"));
            bool bottom3 = Physics2D.Raycast(new Vector2(player.transform.position.x - (width - 0.2f), player.transform.position.y - height), -Vector2.up, length, 1 << LayerMask.NameToLayer("Terreno"));
            if (bottom2 || bottom3)
                return true;
            else
                return false;
        }

        //Returns whether or not player is touching wall or ground.
        public bool isTouching()
        {
            if (isGround() || isWall())
                return true;
            else
                return false;
        }

        //Returns direction of wall.
        public int wallDirection()
        {
            bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length);
            bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);

            if (left)
                return -1;
            else if (right)
                return 1;
            else
                return 0;
        }
    }

    //Feel free to tweak these values in the inspector to perfection.  I prefer them private.
    public float speed = 14f;
    public float accel = 6f;
    public float airAccel = 3f;
    public float jump = 14f;  //I could use the "speed" variable, but this is only coincidental in my case.  Replace line 89 if you think otherwise.

    public bool StoSaltando;
    public bool SonoSuPiattaforma;
    public bool Ladder;
    private float climbVelocity;
    private float gravityStore;

    private GroundState groundState;

    private Animator anim;

    void Start()
    {
        //Create an object to check if player is grounded or touching wall
        groundState = new GroundState(transform.gameObject);
        gravityStore = GetComponent<Rigidbody2D>().gravityScale;
        anim = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("VolumeFX"))
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumeFX");
        }


        if (PlayerPrefs.HasKey("Quality"))
        {
            switch (PlayerPrefs.GetInt("Quality"))
            {
                case 2:
                    break;

                case 1:
                    Camera.main.GetComponent<Bloom>().quality = 0;
                    Camera.main.GetComponent<CameraMotionBlur>().enabled = false;
                    break;

                case 0:
                    Camera.main.GetComponent<Bloom>().enabled = false;
                    Camera.main.GetComponent<CameraMotionBlur>().enabled = false;
                    break;
            }
        }

    }

    public Vector2 input;

    void Update()
    {
        //Handle input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("JoyHorizontal") < 0)
        {
            input.x = -1;
            //GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("JoyHorizontal") > 0)
        {
            input.x = 1;
            //GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else
            input.x = 0;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0"))
        {
            input.y = 1;
            //GetComponent<Rigidbody2D>().isKinematic = false;
        }
        //else StoSaltando = false;



        //VOICE EMOTES
        if (photonView.isMine && PlayerNetwork.possoMorire && ((Input.GetKey(KeyCode.X) || Input.GetKey("joystick button 1"))))
        {
            photonView.RPC("Scoreggia", PhotonTargets.All);
        }


        if (photonView.isMine && PlayerNetwork.possoMorire && ((Input.GetKey(KeyCode.C) || Input.GetKey("joystick button 2"))))
        {
            photonView.RPC("Hello", PhotonTargets.All);
        }


        if (photonView.isMine && PlayerNetwork.possoMorire && ((Input.GetKey(KeyCode.Z) || Input.GetKey("joystick button 3"))))
        {
            photonView.RPC("ImSorry", PhotonTargets.All);
        }



        if (Ladder)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            climbVelocity = 10 * Input.GetAxisRaw("Vertical");

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, climbVelocity);
        }
        if (!Ladder)
        {
            GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        }

        //Reverse player if going different direction
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, (input.x == 0) ? transform.localEulerAngles.y : (input.x - 1) * 90, transform.localEulerAngles.z);
    }

    void FixedUpdate()
    {
        if (!SonoSuPiattaforma)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(((input.x * speed) - GetComponent<Rigidbody2D>().velocity.x) * (groundState.isGround() ? accel : airAccel), 0)); //Move player.
            GetComponent<Rigidbody2D>().velocity = new Vector2((input.x == 0 && groundState.isGround()) ? 0 : GetComponent<Rigidbody2D>().velocity.x, (input.y == 1 && groundState.isTouching()) ? jump : GetComponent<Rigidbody2D>().velocity.y); //Stop player if input.x is 0 (and grounded) and jump if input.y is 1
        }
        else
        {
            //if(input.x != )
        }

        if (groundState.isWall() && !groundState.isGround() && input.y == 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-groundState.wallDirection() * speed * 0.75f, GetComponent<Rigidbody2D>().velocity.y); //Add force negative to wall direction (with speed reduction)
            if (photonView.isMine && PlayerNetwork.possoMorire)
                photonView.RPC("SuonoSalto", PhotonTargets.All);
        }
        if (groundState.isGround() && input.y == 1 && photonView.isMine && PlayerNetwork.possoMorire)
        {
            photonView.RPC("SuonoSalto", PhotonTargets.All);
        }

        anim.SetFloat("Speed", Mathf.Abs(input.x));
        anim.SetBool("IsGrounded", groundState.isGround());
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        input.y = 0;
    }


    void OnCollisionEnter2D(Collision2D collision2D)
    {

        if (collision2D.transform.tag == "MovingPlatform" && photonView.isMine)
        {
            int gg = collision2D.transform.GetComponent<PhotonView>().viewID;
            photonView.RPC("RPCLinkToParent", PhotonTargets.All, gg);
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform" && photonView.isMine)
            photonView.RPC("RPCNoLinkToParent", PhotonTargets.All);
    }


    [PunRPC]
    void RPCLinkToParent(int gg)
    {
        PhotonView view = PhotonView.Find(gg);
        transform.parent = view.gameObject.GetComponent<Transform>();
    }

    [PunRPC]
    void RPCNoLinkToParent()
    {
        transform.parent = null;
    }


}


