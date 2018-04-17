using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamerOver : MonoBehaviour {
    public Text Score, Recorde;
    
	void Start () {

        Score.text = PlayerPrefs.GetInt("Score").ToString();
        Recorde.text = PlayerPrefs.GetInt("Recorde").ToString ();
	}
	

}
