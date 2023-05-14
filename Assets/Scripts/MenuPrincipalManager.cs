using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDoJogo;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevelDoJogo);

    }
    public void Sair()
    {
        Debug.Log("Vai quitar negão?");
        Application.Quit();

    }


}
