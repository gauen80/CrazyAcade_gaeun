using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int monstercnt;
    public TextMeshProUGUI  scoreText;
    public SpawnManager spawnManager;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager.SpawnMonsters();
        monstercnt = spawnManager.monster_cnt;
        UpdateMonsterCount();
        isGameActive = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void UpdateMonsterCount()
    {
        scoreText.text = "Monster: " + monstercnt;
    }

    public void MonsterDied()
    {
        monstercnt--;
        UpdateMonsterCount();
        
        // 모든 몬스터를 처치했을 때의 로직 (예: 게임 승리)
        if (monstercnt <= 0)
        {
            GameOver();
            Debug.Log("모든 몬스터를 처치했습니다!");
        }
    }
}
