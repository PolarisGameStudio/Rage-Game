using UnityEngine;
using System.Collections;

public class MovingPlatform2 : MonoBehaviour {

    public GameObject platform;
    public float moveSpeed;
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelection;

	// Use this for initialization
	void Start () {
        currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
        Vector3 direction = (currentPoint.position - platform.transform.position).normalized;
        //platform.GetComponent<Rigidbody2D>().MovePosition(platform.transform.position + direction * moveSpeed * Time.fixedDeltaTime);

        if (platform.transform.position == currentPoint.position)
        {
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }


	}
}
