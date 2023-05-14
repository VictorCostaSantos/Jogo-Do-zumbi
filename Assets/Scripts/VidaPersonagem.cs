using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VidaPersonagem : MonoBehaviour
{

    public Image cheia;
    public static bool morto = false;

     

    public static float vida = 100;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vida = Mathf.Clamp(vida, 0, 100);

        cheia.fillAmount = vida / 100;

        if (vida <= 0)
        {
            GameOver();
        }; 

    }
    static public  void GameOver()
    {
        
        morto = true;
        
        
    }
    
}
