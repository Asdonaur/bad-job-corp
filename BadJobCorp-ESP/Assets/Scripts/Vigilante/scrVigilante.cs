using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrVigilante : MonoBehaviour
{
    #region VARIABLES
    public GameObject jugador;
    public GameObject controlador;
    public GameObject partEnojo;
    public GameObject texto;

    public string what = "work";
    public bool want = true;

    private float timeActual = 0f;
    private float timeMax = 5f;
    private float timeSpeed = 0.05f;

    public int castigo = 5;

    public bool visto = false;
    public bool castigado = false;
    bool decision = false;

    public string sayAngry;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        jugador = GameObject.Find("objPlayer");
        controlador = GameObject.Find("objCntrl");
        texto = GameObject.Find("objShowText");
    }

    // Update is called once per frame
    void Update()
    {
        timeActual += timeSpeed;
        if (timeActual >= timeMax)
        {
            Destroy(gameObject);
        }

        //Castigar
        if ((visto == true) && (castigado == false) && (decision == false))
        {
            if (jugador.GetComponent<scrPlayer>().sleeping == true)
            {
                castigar(5, 3);
            }

            switch (what)
            {
                case "work":
                    if (jugador.GetComponent<scrPlayer>().isWorking == false)
                    {
                        castigar(1, 1);
                        controlador.GetComponent<scrCntrlInGame>().perderMotivo = 1;
                    }
                break;

                case "no":
                    if (jugador.GetComponent<scrPlayer>().isWorking == true)
                    {
                        castigar(1, 2);
                    }
                break;
            }

            decision = true;
        }
    }

    void castigar(int multi, int motivoLo)
    {
        controlador.GetComponent<scrCntrlInGame>().Sancion(gameObject.GetComponent<scrVigilante>().castigo * multi, motivoLo);
        castigado = true;
        Instantiate(partEnojo);
        texto.GetComponent<scrShowText>().aparecer(sayAngry);
    }
}
