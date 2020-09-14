using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

    public Transform Loadingbar;


    [SerializeField]
    private float currentAmount;

    [SerializeField]
    private float speed;


	
	
	void Update () {
	
        if(currentAmount > 100)
             currentAmount = 0;
        else
            currentAmount += speed * Time.deltaTime;
        

        Loadingbar.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
