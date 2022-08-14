using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraMove : MonoBehaviour
{
    public GameObject controlador;

    // Update is called once per frame
    void Update()
    {
        if (controlador.GetComponent<scrCntrlInGame>().FActual >= controlador.GetComponent<scrCntrlInGame>().FMax - (controlador.GetComponent<scrCntrlInGame>().FMax / 5))
        {
            gameObject.GetComponent<Animator>().SetBool("Danger", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Danger", false);
        }

        if (controlador.GetComponent<scrCntrlInGame>().FActual >= controlador.GetComponent<scrCntrlInGame>().FMax)
        {
            gameObject.GetComponent<Animator>().speed = 0f;
        }
    }
}
