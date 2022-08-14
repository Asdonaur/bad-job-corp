using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSoundOnce : MonoBehaviour
{
    public float timer = 0f;
    public float timerSpeed = 0.05f;
    public float timerMax = 1f;

    float variacion;

    public bool variar;

    // Start is called before the first frame update
    void Awake()
    {
        variacion = Random.Range(0.9f, 1.1f);

        if ((gameObject.GetComponent<AudioSource>()) && (variar == true))
        {
            gameObject.GetComponent<AudioSource>().pitch = variacion;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += timerSpeed;
        if (timer >= timerMax)
        {
            Destroy(gameObject);
        }
    }
}
