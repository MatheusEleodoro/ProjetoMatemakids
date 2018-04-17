using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour {
    private float TempTime = 0, SlideTemp = 5;
	void Start () {
		
	}

	void Update () {


        if (TempTime < SlideTemp)
        {
            TempTime += Time.deltaTime;
            if (TempTime >= SlideTemp)
            {
                SceneManager.LoadScene("Inicio");
            }

        }
       
    }
}
