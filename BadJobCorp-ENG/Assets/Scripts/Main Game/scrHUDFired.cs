using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrHUDFired : MonoBehaviour
{
    public GameObject jugador;
    public GameObject controlador;

    private float sizeActual = 200f;
    private float sizeMax = 200f;

    // Start is called before the first frame update -- textShow.text = size100 + "%";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var medida = GetComponent<RectTransform>();

        sizeActual = (controlador.GetComponent<scrCntrlInGame>().FActual * sizeMax) / controlador.GetComponent<scrCntrlInGame>().FMax;
        medida.sizeDelta = new Vector2(100f, sizeActual);
    }
}
