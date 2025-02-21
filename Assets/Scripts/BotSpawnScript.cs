using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawnScript : MonoBehaviour
{
    public GameObject botPrefab; // Spawn edilecek bot prefab�
    public Transform[] spawnPoints; // 5 adet spawn noktas�

    public int currentLevel = 1; // Ba�lang�� seviyesi
    public int maxLevel = 5; // Toplam level say�s�

    private void Start()
    {
        StartLevel(currentLevel);
    }

    void StartLevel(int level)
    {
        if (level > maxLevel)
        {
            return;
        }
        int botsToSpawn = 3 + (level - 1) * 2; // �lk levelde 3, sonra her levelde +2 eklenerek artar
        StartCoroutine(SpawnBots(botsToSpawn));
    }

    IEnumerator SpawnBots(int botCount)
    {
        for (int i = 0; i < botCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Rastgele spawn noktas� se�
            Instantiate(botPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f); // Botlar aras� k�sa bir gecikme
        }
        yield return new WaitForSeconds(30f); // Yeni level ba�lamadan �nce biraz bekleme s�resi

        currentLevel++;
        StartLevel(currentLevel);
    }
}
