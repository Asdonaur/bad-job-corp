using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCafetera : MonoBehaviour
{
    #region VARIABLES
    public GameObject jugador;
    public GameObject controlador;
    public GameObject hover;
    public GameObject smell;
    public GameObject soundCant, soundFinish, soundSwallow;
    public GameObject texto;

    public int vidaSuma = 10;

    public float timer = 0f;
    public float timerSpeed = 0.01f;
    public float timerMax = 1f;

    public bool listo = false;
    bool actuar = true;
    private Quaternion rotation = new Quaternion();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        smell.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10);
    }

    // Update is called once per frame
    void Update()
    {
        if ((listo == false) && (actuar == true)) //Contador para esperar antes de volver a tomar cafe
        {
            if (timer >= timerMax)
            {
                timer = 0f;
                listo = true;
                smell.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                Instantiate(soundFinish);
            }
            else
            {
                timer += timerSpeed;
            }
        }

        if (controlador.activeSelf == false)
        {
            actuar = false;
        }
    }

    // -- Subir energia si se hace clic en la cafetera
    void OnMouseDown()
    {
        if ((listo == true))
        {
            curar();
        }
        else
        {
            Instantiate(soundCant, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10), rotation);
            texto.GetComponent<scrShowText>().aparecer("Still not ready. Wait a moment.");
        }
    }
    void OnMouseOver()
    {
        hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        hover.SetActive(false);
    }

    private void curar()
    {
        if (jugador.GetComponent<scrPlayer>().sleeping == false)
        {
            jugador.GetComponent<scrPlayer>().vida += vidaSuma;
            Instantiate(soundSwallow);
        }
        else
        {
            jugador.GetComponent<scrPlayer>().despertar(true);
        }

        listo = false;
        smell.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10);
    }
}
