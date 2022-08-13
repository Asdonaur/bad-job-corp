using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class srcQuitarPausa : MonoBehaviour
{
    public GameObject controlador = GameObject.Find("objCntrl");
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            controlador.SetActive(true);
            controlador.GetComponent<scrCntrlInGame>().pausa(false);
            Destroy(gameObject);
        }
    }
}
