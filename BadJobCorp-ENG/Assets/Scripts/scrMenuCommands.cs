using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrMenuCommands : MonoBehaviour
{
    public GameObject menuNormal;
    public GameObject menuCredits;
    public GameObject controlador;
    public GameObject sButton;

    public AudioClip musicNormal;
    public AudioClip musicBoss;

    public void creditsShow()
    {
        menuNormal.SetActive(false);
        menuCredits.SetActive(true);
        Instantiate(sButton);
    }

    public void creditsHide()
    {
        menuNormal.SetActive(true);
        menuCredits.SetActive(false);
        Instantiate(sButton);
    }

    public void cargarEscenaJuegoN()
    {
        cargarEscenaJuegoParametros(musicNormal);
    }

    public void cargarEscenaJuegoB()
    {
        cargarEscenaJuegoParametros(musicBoss);
    }

    public void cargarEscenaJuegoParametros(AudioClip musica)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("sceneInGame");
        controlador = GameObject.Find("objCntrl");
        controlador.GetComponent<AudioSource>().clip = musica;
        Instantiate(sButton);
    }
}
