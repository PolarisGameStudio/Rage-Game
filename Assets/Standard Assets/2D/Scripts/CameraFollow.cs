using UnityEngine;
using System.Collections;
//using Com.LuisPedroFonseca.ProCamera2D;

public class CameraFollow : MonoBehaviour
{

    Transform asd;
    // Use this for initialization
    void Start()
    {
        asd = GameObject.FindGameObjectWithTag("Player").transform;
        //transform.GetComponent<ProCamera2D>().AddCameraTarget(asd, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
