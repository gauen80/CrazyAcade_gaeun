using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int monster_cnt = 3;
    public float screen_len = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SpawnMonsters()
    {
        for(int i = 0; i< monster_cnt ; i++)
        {
            float randomX = Random.Range(-screen_len, screen_len);
            float randomY = Random.Range(-screen_len, screen_len);

            Vector2 spawnposition = new Vector2(randomX,randomY);
            Instantiate(monsterPrefab, spawnposition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
