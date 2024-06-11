using UnityEngine;
using System.Collections;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;
    public float initialSpawnRate = 3.5f;
    private float spawnRate;
    private float nextTimeToSpawn = 0f;
    private float rushSpawnRate = 7.0f;
    private float timeBetweenRushes = 15.0f;
    private float rushPeriodDuration = 5.0f;
    private bool isRushPeriod = false;
    private int level = 0;

    void Start()
    {
        spawnRate = initialSpawnRate;
        StartCoroutine(LevelManager());
        UpdateLevelUI();
    }

    void Update()
    {
        if (Time.time >= nextTimeToSpawn)
        {
            SpawnSpike();
            nextTimeToSpawn = Time.time + 1 / (isRushPeriod ? rushSpawnRate : spawnRate);
        }
    }

    void SpawnSpike()
    {
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, 180);
        Instantiate(spikePrefab, spawnPosition, rotation);
    }

    IEnumerator LevelManager()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenRushes);
            StartCoroutine(RushPeriod());
            yield return new WaitForSeconds(rushPeriodDuration + 2.0f);  // Wait for spikes to fall
            level++;
            UpdateDifficulty();
            UpdateLevelUI();
        }
    }

    IEnumerator RushPeriod()
    {
        isRushPeriod = true;
        FindObjectOfType<Player>().StartShrink(rushPeriodDuration + 2.0f);  // Extend shrink time to ensure all spikes have fallen
        yield return new WaitForSeconds(rushPeriodDuration);
        isRushPeriod = false;
    }

    void UpdateDifficulty()
    {
        rushSpawnRate += 0.5f; // Increase rush spawn rate
        spawnRate += 0.2f;    // Increase normal spawn rate
        timeBetweenRushes += 5.0f; // Increase time between rushes each level
    }

    void UpdateLevelUI()
    {
        // Assuming you have a Text object in the scene with the tag "LevelText"
        UnityEngine.UI.Text levelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<UnityEngine.UI.Text>();
        levelText.text = "Level: " + level;
    }
}
