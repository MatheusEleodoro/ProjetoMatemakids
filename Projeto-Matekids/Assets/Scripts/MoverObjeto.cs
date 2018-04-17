using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour {
    public float velocidade;
    private float px;
    public GameObject Player;
    private bool scored;
	
	void Start () {
        Player = GameObject.Find("Player") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        px = transform.position.x;
        px += velocidade * Time.deltaTime;
        transform.position = new Vector3(px,transform.position.y,transform.position.z); //Movimentação do obstaculo

        if(px <= -5)
        {
            Destroy(transform.gameObject);
        }

        if(px < Player.transform.position.x && !scored)
        {
            scored = true;
            PlayerControlador.score++;
            PlayerControlador.soundscore = true;
        }
	}
}
