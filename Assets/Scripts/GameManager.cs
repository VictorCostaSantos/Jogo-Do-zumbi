using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject onScreenControllers;



    [SerializeField] GameObject tela;


    void Start()
    {
#if UNITY_ANDROID
        onScreenControllers.SetActive(true);
#else
        onScreenControllers.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
        if (VidaPersonagem.vida <= 0)
        {
            
            tela.SetActive(true);
            Time.timeScale = 0f;
            
        }
    }

 
    public void Jogar()
    {
        tela.SetActive(false);
        print("oi");
        SceneManager.LoadScene("jogo");
        ReiniciarJogo();

    }
    public void Sair()
    {
        Debug.Log("Vai quitar negão?");
        Application.Quit();

    }

    public void ReiniciarJogo()
    {
        // Redefinir variáveis
        VidaPersonagem.vida = 100;
        EnemyBehavior.LifePlayer = VidaPersonagem.vida;
        

        // Ativar tempo novamente
        Time.timeScale = 1f;

        // Recarregar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
