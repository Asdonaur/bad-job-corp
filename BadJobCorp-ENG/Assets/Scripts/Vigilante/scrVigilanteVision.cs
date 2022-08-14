using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrVigilanteVision : MonoBehaviour
{
    public GameObject jugador;
    public GameObject padre;

    public float growSpeed = 0.1f;
    public int growDir = 1;
    public float growEnd = 8f;
    public float growActual = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        growActual = growSpeed * growDir;
        transform.position = new Vector3(transform.position.x - growActual, transform.position.y, transform.position.z);
        if (transform.position.x <= -growEnd)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider coli)
    {
        if (coli.tag == "Player")
        {
            padre.GetComponent<scrVigilante>().visto = true;
        }
    }
}
