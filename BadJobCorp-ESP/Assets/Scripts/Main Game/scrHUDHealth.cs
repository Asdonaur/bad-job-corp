using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrHUDHealth : MonoBehaviour
{
    public GameObject jugador;

    private float sizeActual = 200f;
    private float sizeMax = 200f;
    private float size100 = 100f;

    public float sizeMinus = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        size100 = sizeActual / 2;

        var medida = GetComponent<RectTransform>();

        sizeActual = ((jugador.GetComponent<scrPlayer>().vida * sizeMax) / jugador.GetComponent<scrPlayer>().vidaMax) - sizeMinus;
        medida.sizeDelta = new Vector2(100f, sizeActual);
    }
}
