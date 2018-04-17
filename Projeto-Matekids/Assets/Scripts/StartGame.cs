using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
   public  bool click;
   private float temp = 0, fimtemp = 1;
    
    void Start() {

    }


    void Update() {
       
         if(click == true)
        {
           
            if(temp < fimtemp)
            {
                temp += Time.deltaTime;
                if (temp > fimtemp)
                {
                    SceneManager.LoadScene("Matekids");
                }
            }
            
        }
        
    }

    public void InitiGame(bool clicked)
    {
        click = clicked;
    }

    public void InitiRecordes (bool clicked)
    {
        if (clicked)
        {
            SceneManager.LoadScene("Records");
        }
    }

    public void InitiCreditos (bool clicked)
    {
        if (clicked)
        {
            SceneManager.LoadScene("Creditos");
        }
    }

    public void InitiInicio (bool clicked)
    {
        SceneManager.LoadScene("Inicio");
    }

}
