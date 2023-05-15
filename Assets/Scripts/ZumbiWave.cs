using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZumbiWave : MonoBehaviour
{
    public GameObject prefab;
    public static Vector3 spawnPosition = Player.posicao;
    public int numberOfZombies = 10;
    public float spawnDelay = 3f;
    public float timeBetweenWaves = 5f;
    public int numberOfWaves = 3;
    public Text waveText;

    private bool isFirstWaveActive = true;
    private int zombiesCreated = 0;
    private float lastSpawnTime = 0f;
    public static int currentWave = 1;
    private float waveStartTime = 0f;

    public AudioClip waveOneSound;
    public AudioClip waveTwoSound;
    public AudioClip waveThreeSound;


    void Start()
    {
        waveStartTime = Time.time;
        UpdateWaveText();
        SpawnZombies();
    }

    void Update()
    {
        if (isFirstWaveActive)
        {
            if (Time.time > lastSpawnTime + spawnDelay && zombiesCreated < numberOfZombies)
            {
                lastSpawnTime = Time.time;
                Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                Instantiate(prefab, spawnPosition + randomPosition, Quaternion.identity);
                zombiesCreated++;
                Debug.Log("Zombies created: " + zombiesCreated);
            }

            if (zombiesCreated >= numberOfZombies && GameObject.FindGameObjectsWithTag("Zombie").Length == 0)
            {
                isFirstWaveActive = false;
                waveStartTime = Time.time;
                currentWave++;
                UpdateWaveText();
                StartCoroutine(SpawnZombiesWithDelay());
            }
        }
    }

    void SpawnZombies()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            Instantiate(prefab, spawnPosition + randomPosition, Quaternion.identity);
            zombiesCreated++;
            Debug.Log("Zombies created: " + zombiesCreated);
        }
    }

    IEnumerator SpawnZombiesWithDelay()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        zombiesCreated = 0;
        isFirstWaveActive = true;
        SpawnZombies();

        if (currentWave > numberOfWaves)
        {
            Debug.Log("Todas as ondas de zumbi completadas.");
            waveText.text = "Todas as ondas de zumbi completadas.";
            enabled = false;
        }
    }

    void UpdateWaveText()
    {
        waveText.text = "Onda " + currentWave;
        PlayWaveAudio(currentWave);

    }
    void PlayWaveAudio(int wave)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        switch (wave)
        {
            case 1:
                audioSource.PlayOneShot(waveOneSound);
                break;
            case 2:
                audioSource.PlayOneShot(waveTwoSound);
                break;
            case 3:
                audioSource.PlayOneShot(waveThreeSound);
                break;
        }
    }

}

