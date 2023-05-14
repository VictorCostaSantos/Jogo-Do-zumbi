using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;
    [SerializeField] private Transform playerTransform;
    private Animator animator;
    public static float LifePlayer = VidaPersonagem.vida;
    private int ZombieLife = 3;

    private GameObject objeto;


    public float damageTimer = 2f; // Define o tempo entre os danos em segundos
    private float currentTimer = 0.0f; // Controla o tempo atual


    private void Awake()
    {        
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyNavMesh.isStopped = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyNavMesh.isStopped /*enemyNavMesh.isStopped == false*/)
        {
            enemyNavMesh.SetDestination(playerTransform.position);
        }
        float distanciaParaJogador = Vector3.Distance(transform.position, playerTransform.position);

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
