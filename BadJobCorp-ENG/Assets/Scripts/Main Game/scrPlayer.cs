using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPlayer : MonoBehaviour
{
    #region VARIABLES
    public GameObject monitor;
    public GameObject controlador;
    public GameObject partDormir;
    public GameObject cafeDespertar;

    public int vida = 100;
    public int vidaMax = 100;
    private int vidaLose = 1;
    private float vida100;

    public bool isWorking = false;
    public bool goneCrazy = false;
    public bool sleeping = false;

    public float timeRActual = 0f;
    public float timeRSpeed = 0.01f;
    public float timeRMax = 1f;
    public int vidaR = 200;

    public float timeCActual = 0f;
    public float timeCSpeed = 0.5f;
    public float timeCMax = 1f;

    public Text textHealth;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //ss
    }

    // Update is called once per frame
    void Update()
    {
        if (controlador.activeSelf == true) // Si el controlador está activo...
        {
            // -- Bases --
            vida100 = (vida * 100) / vidaMax;

            // -- Control de UI --
            textHealth.text = vida100 + "%";

            //  ---  ESTADO: Normalmente ---
            if ((sleeping == false))
            {
                // -- Trabajar con la tecla ESPACIO --
                if (Input.GetKeyDown("space"))
                {
                    isWorking = true;
                    GetComponent<Animator>().SetBool("SleepStop", false);
                    GetComponent<AudioSource>().mute = false;
                    GetComponent<Animator>().SetBool("Typing", true);
                    monitor.GetComponent<Animator>().SetBool("Typing", true);
                    sancionGradual(-1, 2);
                    controlador.GetComponent<scrCntrlInGame>().quitarSancion = true;
                }

                if (Input.GetKey("space"))
                {
                    vida = vida - (vidaLose); // Perder vida
                    controlador.GetComponent<scrCntrlInGame>().subirPuntaje(1); //Subir puntaje
                }
                else // Si no, entonces recupera vida gradualmente y aumenta la barra de despido
                {
                    if (!(vida >= vidaMax))
                    {
                        timeRActual += timeRSpeed; // Sumar timer R
                        if (timeRActual >= timeRMax)
                        {
                            timeRActual = 0;
                            recuperarVida(vidaR); // Recuperar vida
                        }
                    }

                    if (goneCrazy == false) // La barra de despido aumenta por inactividad solo si el jugador no esta loco para no solapar con la sancion de la locura
                    {
                        sancionGradual(1, 5);
                    }
                }

                if (Input.GetKeyUp("space"))
                {
                    dejarDeEscribir();
                    controlador.GetComponent<scrCntrlInGame>().quitarSancion = false;
                }

                // -- Volverse loco --
                if (vida > vidaMax + vidaR)
                {
                    locura();
                }
                else
                {
                    sinLocura();
                }

                // -- Quedarse dormido --
                if (vida100 <= 0)
                {
                    dormir();
                }

                //  ---  ESTADO MEDIO: Locura --- 
                if (goneCrazy == true)
                {
                    sancionGradual(2, 4);
                }
            }

            //  ---  ESTADO: Durmiendo --- 
            if (sleeping == true)
            {
                vida += 1;
                if (vida >= vidaMax)
                {
                    despertar(false);
                    GetComponent<Animator>().SetBool("Typing", false);
                }
            }
        }
        else // Cuando pierdes
        {
            GetComponent<Animator>().speed = 0f;
            GetComponent<AudioSource>().mute = true;
            monitor.GetComponent<Animator>().SetBool("Typing", false);

            if (partDormir.activeSelf == true)
            {
                partDormir.GetComponent<AudioSource>().mute = true;
            }
        }
    }

    public void dejarDeEscribir()
    {
        isWorking = false;
        GetComponent<AudioSource>().mute = true;
        monitor.GetComponent<Animator>().SetBool("Typing", false);
        GetComponent<Animator>().SetBool("Typing", false);
    }
    public void recuperarVida(int recover)
    {
        while(recover > 0)
        {
            recover -= 1;
            vida += 1;
        }
    }

    public void dormir()
    {
        dejarDeEscribir();
        sleeping = true;
        GetComponent<Animator>().SetBool("Sleep", true);
        partDormir.SetActive(true);
    }

    public void despertar(bool cafe)
    {
        sleeping = false;
        GetComponent<Animator>().SetBool("SleepStop", true);
        GetComponent<Animator>().SetBool("Sleep", false);
        partDormir.SetActive(false);

        if (cafe == true)
        {
            Instantiate(cafeDespertar);
        }
        else
        {
            vida = vidaMax;
        }
    }

    public void locura()
    {
        goneCrazy = true;
        GetComponent<Animator>().speed = 1.5f;
        controlador.GetComponent<AudioSource>().pitch = 1.5f;
    }
    public void sinLocura()
    {
        goneCrazy = false;
        GetComponent<Animator>().speed = 1;
        controlador.GetComponent<AudioSource>().pitch = 1f;
    }

    public void sancionGradual(int mult, int mot)
    {
        timeCActual += timeCSpeed;
        if (timeCActual >= timeCMax)
        {
            timeCActual = 0;
            controlador.GetComponent<scrCntrlInGame>().Sancion(mult, mot);
        }
    }
}
