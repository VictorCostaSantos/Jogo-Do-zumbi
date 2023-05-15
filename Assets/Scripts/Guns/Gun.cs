using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float damage;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private AudioClip fireSound;


    private void Start()
    {
        
    }
    public void Fire()
    {
        //Executar animações, sons e efeitos de tiro da arma
        Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 10.0f);
    }
}
