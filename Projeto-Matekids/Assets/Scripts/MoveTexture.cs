using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour {

    private Material c_material;
    public float velocidade;
    private float offset;


	void Start () {
        c_material = GetComponent<Renderer>().material;
	}
	
	
	void Update () {
        offset += velocidade * Time.deltaTime;
        c_material.SetTextureOffset("_MainTex", new Vector2(offset,0));
	}
}
