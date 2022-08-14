using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrCntrlInGame : MonoBehaviour
{
    #region VARIABLES
    [Header("Objetos")]
    public GameObject hudInGame;
    public GameObject hudLose;
    public GameObject hudTexto, hudAlertFired;
    public GameObject screenCover;
    public GameObject sndDanger, sndLose;
    public GameObject objPausaManager;
    public GameObject camara;

    [Header("Vigilantes y relacoinados")]
    public GameObject marcaContainer;
    public int marcaIndex = 0;
    public int marcaIndexM = 2;
    public GameObject marcaJefe;
    public GameObject marcaGuy;
    public GameObject marcaZack;
    public GameObject marcaSpard;

    public GameObject mscNormal, mscAlien;

    [Header("Textos en HUD")]
    public Text FText;
    public Text PText;
    public Text motivoText;
    public Text consejoText;
    public Text puntosLoseText;
    public Text recordText;

    [Header("Sistamas de puntajes")]
    public int puntaje = 0;
    public int record = 0;
    public int FActual = 0;
    public int FMax = 100;

    [Header("Otros")]
    private float timerVActual = 0f;
    private float timerVSpeed = 0.01f;
    public float timerVMax = 7f;
    public float timerVMaxM = 4f;
    public float timerVMinus = 0.25f;

    public float timerFActual = 0f;
    public float timerFSpeed = 0.05f;
    public float timerFMax = 25f;
    float timerFFactor = 1;

    public float altura = 1f;

    private bool pausado = false, quitarPausa = true;

    //                           0                        1                                       2                                                         3                               4                               5
    string[] perderRazones = { "Because the debug",    "Your boss saw you doing nothing",                "Ignoring coworkers",                               "Sleeping at work",                    "Madness attack",               "Inactivity" };
    string[] perderConsejos = { "Stop using hacks",    "Do not stop working when the boss looks at you", "Stop working when someone except the boss is here","Always care about your energy bar", "Coffee in excess is not good","Your dismissal bar raises up when you are doing nothing"};
    public int perderMotivo = 0;

    public bool quitarSancion = true;

    private Quaternion rotacion = new Quaternion();
    #endregion
    void Awake()
    {
        record = PlayerPrefs.GetInt("rec", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG
        if (Input.GetKey("q"))
        {
            Sancion(5, 0);
        }

        if (Input.GetKey("a"))
        {
            screenshot();
        }

        // -- Pausar juego --
        if (Input.GetKeyDown("p"))
        {
            pausa(false);
        }

        // -- Para que FActual no sea menor a 0 --
        if (FActual < 0)
        {
            FActual = 0;
        }

        // -- Control de UI --
        FText.text = FActual + "%";

        if (FActual > 0) // BAJAR BARRA DE DESPIDO
        {
            if (quitarSancion == true)
            {
                timerFActual += timerFSpeed;
                if (timerFActual >= timerFMax * timerFFactor)
                {
                    FActual -= 1;
                    timerFActual = 0f;
                }
            }
        }

        if (FActual >= FMax - (FMax / 5)) // -- Alertar al jugador que esa a punto de perder --
        {
            sndDanger.SetActive(true);
            camara.GetComponent<Animator>().SetBool("Danger", true);
            hudAlertFired.SetActive(true);
            timerFFactor = 0.5f;
        }
        else
        {
            sndDanger.SetActive(false);
            camara.GetComponent<Animator>().SetBool("Danger", false);
            hudAlertFired.SetActive(false);
            timerFFactor = 1f;
        }

        // -- Si se llena la barra de despido pierdes --
        if (FActual >= FMax)
        {
            Despido();
        }

        // -- Llamar vigilante --
        timerVActual += timerVSpeed;
        if (timerVActual >= timerVMax)
        {
            if (timerVMax >= timerVMaxM) //Aumentar la velocidad de aparicion progresivamente
            {
                timerVMax -= timerVMinus;
            }

            marcaIndex = Random.Range(0, marcaIndexM + 1);
            switch (marcaIndex)
            {
                case 0:
                    LlamarMarca(marcaJefe);
                    break;
                case 1:
                    LlamarMarca(marcaGuy);
                    break;
                case 2:
                    LlamarMarca(marcaZack);
                    break;
                case 3:
                    LlamarMarca(marcaSpard);
                    break;
            }
            timerVActual = 0f;
        }
    }

    public void subirPuntaje(int puntos)
    {
        puntaje += puntos;
        PText.text = "Score: " + puntaje;
    }
    public void Sancion(int suma, int motivoL)
    {
        FActual += suma;
        perderMotivo = motivoL;
    }

    void screenshot()
    {
        ScreenCapture.CaptureScreenshot("captura.png");
    }
    public void pausa(bool perder)
    {
        if (pausado == false)
        {
            hudInGame.SetActive(false);
            screenCover.SetActive(true);
            gameObject.GetComponent<AudioSource>().mute = true;

            gameObject.SetActive(false);
            sndDanger.SetActive(false);

            pausado = true;

            if (perder == true)
            {
                quitarPausa = false;
            }
            else
            {
                Instantiate(objPausaManager);
            }
        }
        else
        {
            if (quitarPausa == true)
            {
                hudInGame.SetActive(true);
                screenCover.SetActive(false);
                gameObject.GetComponent<AudioSource>().mute = false;

                gameObject.SetActive(true);

                if (FActual >= FMax - (FMax / 5))
                {
                    sndDanger.SetActive(true);
                }
                else
                {
                    sndDanger.SetActive(false);
                }

                pausado = false;
            }
        }
        
    }

    void Despido()
    {
        pausa(true);
        Instantiate(sndLose);
        hudLose.SetActive(true);

        motivoText.text = "PRINCIPAL REASON:\n" + perderRazones[perderMotivo];
        consejoText.text = " HINT:\n" + perderConsejos[perderMotivo];
        puntosLoseText.text = "SCORE:\n" + puntaje;

        if (puntaje > record)
        {
            record = puntaje;
            PlayerPrefs.SetInt("rec", record);
        }

        recordText.text = "HIGHSCORE:\n" + record;

        hudTexto.SetActive(false);
        camara.GetComponent<Animator>().SetBool("Danger", false);
    }
    public void LlamarMarca(GameObject marca)
    {
        Instantiate(marca, new Vector3(marcaContainer.transform.position.x, marcaContainer.transform.position.y, marcaContainer.transform.position.z), rotacion);
    }

    public void LlamarVigilante(GameObject vigilante)
    {
        Instantiate(vigilante, new Vector3(marcaContainer.transform.position.x, marcaContainer.transform.position.y, marcaContainer.transform.position.z), rotacion);
    }
}
