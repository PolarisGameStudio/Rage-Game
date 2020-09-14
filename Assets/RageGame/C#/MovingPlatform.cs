using UnityEngine;
using System.Collections;

public class MovingPlatform : Photon.MonoBehaviour {

	[SerializeField]
	Transform platform;

	[SerializeField]
	Transform startTransform;

	[SerializeField]
	Transform endTransform;

	[SerializeField]
	float platformSpeed;

	public Vector3 direction;
	Transform destination;
    Vector3 position;
    public Transform player;
    bool ItsOk = false;
		
	void Start (){
        position = startTransform.transform.position;
		SetDestination(startTransform);
	}

	void FixedUpdate(){
        if (ItsOk)
        {
            //Debug.Log(direction.x);
            platform.GetComponent<Rigidbody2D>().MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);
            //if(player)player.GetComponent<Rigidbody2D>().MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
            {
                SetDestination(destination == startTransform ? endTransform : startTransform);
            }
        }
	}

    void Update()
    {
    }


	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(startTransform.position, platform.localScale);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(endTransform.position, platform.localScale);
	}

	void SetDestination(Transform dest){
		destination = dest;
		direction = (destination.position - platform.position).normalized;
	}

    void OnEnable()
    {
        platform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        platform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        ItsOk = true;
    }

    void OnDisable()
    {
        ItsOk = false;
        platform.transform.position = startTransform.position;
        platform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }


    void OnApplicationPause(bool pauseStatus) {
         
         while(true) {
  
             /*Debug.Log ("About to play aiff.");
             LocalNotification notif = new LocalNotification(); 
             notif.alertAction = "alert";
             notif.alertBody = "Playing aiff sound";
             
             notif.fireDate = System.DateTime.Now.AddSeconds(120);
             notif.applicationIconBadgeNumber = -1;
                 
             break;*/
             //System.DateTime.Now.AddSeconds(120);
             Time.timeScale = 1.0f;
             
         }
     
     }

}
