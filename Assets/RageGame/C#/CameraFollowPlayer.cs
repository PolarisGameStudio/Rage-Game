using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraFollowPlayer : MonoBehaviour
{
    public float influenceX = 1f;
    public float influenceY = 1f;
    private Transform player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.GetComponent<ProCamera2D>().AddCameraTarget(player, influenceX, influenceY, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
