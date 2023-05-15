using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;

    private AudioSource soundEffect; //som

    public static Vector3 posiction;
    private Animator animator;
    public static float LifePlayer = VidaPersonagem.vida;
    private int ZombieLife = 3;

   


    public float damageTimer = 2f; // Define o tempo entre os danos em segundos
    private float currentTimer = 0.0f; // Controla o tempo atual

      
    private void Awake()
    {        
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyNavMesh.isStopped = true;
        animator = GetComponent<Animator>();

            soundEffect = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        posiction = Player.posicao;
        
        if (!enemyNavMesh.isStopped /*enemyNavMesh.isStopped == false*/)
        {
            enemyNavMesh.SetDestination(posiction);
        }
        float distanciaParaJogador = Vector3.Distance(transform.position, posiction);

        if (distanciaParaJogador <= 4)
        {
            animator.SetBool("ataque", true);
           

            if (currentTimer <= 0.0f)
            {
                VidaPersonagem.vida -= 1;
                
                currentTimer = damageTimer;
            }
            else
            {
                currentTimer -= Time.deltaTime * 2;
            }
        }
        else
        {
            animator.SetBool("ataque", false);
        }

    }
      
    
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyNavMesh.isStopped = false;
            soundEffect.Play(); // Tocar o som quando

        }
        animator.SetBool("andando", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            ZombieLife -= 1;
            if (ZombieLife < 0)
            {
                enemyNavMesh.isStopped = true;
                animator.SetBool("morte", true);
                soundEffect.Stop();
                Invoke("DesativarInimigo", 10f);
                Destroy(collision.gameObject);

            }
        }
    }

    void DesativarInimigo()
    {
        // desativa o GameObject do inimigo
        gameObject.SetActive(false);

        // destrói o objeto inimigo
        Destroy(gameObject);
    }


    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("andando", false);
    }

  
}
