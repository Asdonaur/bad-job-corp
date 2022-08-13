using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMark : MonoBehaviour
{
    public GameObject Vigilante;
    public GameObject controlador;
    public GameObject sonido;

    public float timeActual = 0f;
    public float timeSpeed = 0.1f;
    public float timeMax = 10f;
    private float timeMax2 = 15f;

    private bool sound = false;
    // Start is called before the first frame update
    void Start()
    {
        timeMax2 = timeMax + (timeMax / 2);
        controlador = GameObject.Find("objCntrl");
    }

    // Update is called once per frame
    void Update()
    {
        timeActual += timeSpeed;
        if (timeActual >= timeMax)
        {
            gameObject.GetComponent<Animator>().SetBool("Alarm", true);
            ruido();

            if (timeActual >= timeMax2)
            {
                controlador.GetComponent<scrCntrlInGame>().LlamarVigilante(gameObject.GetComponent<scrMark>().Vigilante);
                Destroy(gameObject);
            }
        }

    }

    void ruido()
    {
        if (sound == false)
        {
            Instantiate(sonido);
            sound = true;
        }
    }
}
