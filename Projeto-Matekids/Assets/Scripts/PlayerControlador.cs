using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerControlador : MonoBehaviour
{
    //Declaração de variaveis 

    public Rigidbody2D PlayerRB; //Corpo do Personagem
    public Animator Anim; //controle das animações
    public int JumpForca;// força do pulo do player
    private bool InitToque = false;
    private bool pulo, slide;
    private float temp =0, fimtemp =0.2f;// Variaveis para Criação de Delays

    public Transform Check_Chao; // Variaveis p/ verificaçao se player esta no chão ou não
    public bool check_c = false;
    private float chaoRadios = 0.2f;
    public LayerMask issoechao; //fim

    public float SlideTemp, TempTime; // P controle do tempo do slide


    public float sensibilidade = 20f;// sensibilidade do touch
    private bool validacao = true, validacao_aux2 =true,validacao_aux = false;
    private Vector2 pcima, pbaixo;// controle de direção touch
    public Transform colisor; // Sistema de Colisão

    public  AudioSource audio; // Controle de Audio
    public  AudioClip SoundJump, SoundSlide, SoundScore; // fim Controle de audio

    public static int score; // Controle Pontuação
    public static bool soundscore;
    public Text txtScore;


    // Fim Declaração

    // Inicialização
    void Start()
    {
        Debug.Log("Jogo Iniciado");
        score = 0;
        PlayerPrefs.SetInt("Score", score);
    }

    // Update is called once per frame
    void Update()
    {

        CheckChao(); // Instancia Verificação constante se personagem esta no chão ou não
        txtScore.text = score.ToString();
        
        if (soundscore)
        {
            AtivSoundPoint();
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                pcima = touch.position;
                pbaixo = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (!InitToque)
                {

                    pbaixo = touch.position;
                    CheckSwipe();

                }
            }
        } //Captura a posição do touch EIXO Y E EIXO X PARA CHECKSWIPE DA DIREÇÃO


    }
    //Incio CheckSwipe - FUNÇÃO QUE IDENTIFICA DIREÇÃO CIMA OU BAIXO PARA PASSAR PARA OS CONTROLADORES
    void CheckSwipe()
    {
        if (VerticalMove() > sensibilidade && VerticalMove() > HorizontalMove())
        {
            if (pbaixo.y - pcima.y > 0)
            {
               

               if (temp < fimtemp)
                {
                    temp += Time.deltaTime;
                    if (temp >= fimtemp)
                    {
                        validacao_aux2 = true;
                        temp = 0;
                    }
                }

                if (validacao_aux2)
                {
                    OnControllerCima();
                }
                
            }
            else if (pbaixo.y - pcima.y < 0)
            {
                OnControllerBaixo();
            }

        }

    }
    //Fim CheckWipe

    float VerticalMove()
    {
        return Mathf.Abs(pbaixo.y - pcima.y);
    }
    float HorizontalMove()
    {
        return Mathf.Abs(pbaixo.x - pcima.x);
    }
    void OnControllerCima()//REALIZA CONTROLE DE PULO DO PERSONAGEM
    {

        
        if (check_c && validacao_aux2) // Bloqueio de Slide no AR, ou pulo constante
        {
            PlayerRB.AddForce(new Vector2(0, JumpForca));
            Debug.Log(JumpForca);
            pulo = true;
            audio.volume = 1;
            audio.PlayOneShot(SoundJump);
            
           
            if (slide)
            {
              //  Debug.Log("Slide 1");
                colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.1f, colisor.position.z);
                slide = false;
                validacao_aux = true;
                validacao = true;
            }
            else
            {
                validacao_aux = false;
            }

            validacao_aux2 = false;
            
            
        }


    }
    void OnControllerBaixo()
    {
        

       
        if (check_c && !slide)
        {
            slide = true;
            audio.volume = 0.2f;
            audio.PlayOneShot(SoundSlide);
            if (validacao)
            {
                colisor.position = new Vector3(colisor.position.x, colisor.position.y - 0.1f, colisor.position.z);
                validacao = false;
            }
                
            
        }
    }// REALIZA ATIVAÇÃO DO SLIDE DO PERSONAGEM TRANSFORMANDO VARIAVEL slide em TRUE

    void CheckChao() //Verifica se personagem esta no chão
    {

        check_c = Physics2D.OverlapCircle(Check_Chao.position, chaoRadios, issoechao); // Cria Bloco de Colisão na posição do personagem 

        if (check_c)// Verifica se ele esta no chao e se pode realizar o pulo
        {
            pulo = false;
        }

        if (slide == true && check_c == true)
        {

            if (TempTime >= SlideTemp)
            {
                TempTime = 0;
                
            }
            //Debug.Log("Chamando");
            TempSlide();
        }  //Controle e Validação do Tempo de Slide

        Anim.SetBool("Jump", pulo); // Passa VERDADEIRO ou FALSO para as Animações dentro do Unity
        Anim.SetBool("Slide", slide);        
       
    }
    void TempSlide()
    {

        if (slide == true)
        {
            if (TempTime < SlideTemp)
            {
                TempTime += Time.deltaTime;
                if (TempTime >= SlideTemp)
                {
                    slide = false;
                    if (!validacao_aux)
                    {
                    //    Debug.Log("Slide 2"); 
                        colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.1f, colisor.position.z);
                        validacao = true;
                    }
                    
                }

            }

            
        }

    }// Função que Marca tempo de 1s de duração para o slide 

    void OnTriggerEnter2D ()
    {
        // Application.LoadLevel("GameOver");
        PlayerPrefs.SetInt("Score", score);
        if(score > PlayerPrefs.GetInt("Recorde"))
        {
            PlayerPrefs.SetInt("Recorde", score);
        }
        SceneManager.LoadScene("Gameover");

    }

     void  AtivSoundPoint()
    {
       
            audio.PlayOneShot(SoundScore);
            audio.volume = 1;
            soundscore = false;
        
    }
}