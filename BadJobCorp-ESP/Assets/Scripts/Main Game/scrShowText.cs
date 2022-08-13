using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrShowText : MonoBehaviour
{
    public GameObject globoTexto;
    public Text texto;

    public float timerA, timerM;
    public string strShow;
    public bool mostrando = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mostrando == true)
        {
            timerA += Time.deltaTime;
            if (timerA >= timerM)
            {
                desaparecer();
            }
        }
    }

    public void aparecer(string nuevoTexto)
    {
        timerA = 0;
        strShow = nuevoTexto;
        globoTexto.SetActive(true);
        texto.text = strShow;
        mostrando = true;
    }
    void desaparecer()
    {
        globoTexto.SetActive(false);
        mostrando = false;
    }
}
