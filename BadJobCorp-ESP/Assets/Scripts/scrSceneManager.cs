using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrSceneManager : MonoBehaviour
{
    public GameObject sButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cargarEscenaJuego()
    {
        Instantiate(sButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene("sceneInGame");
    }

    public void cargarEscenaMenu()
    {
        Instantiate(sButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene("sceneMainMenu");
    }

    public void cerrarJuego()
    {
        Instantiate(sButton);
        Application.Quit();
    }
}
