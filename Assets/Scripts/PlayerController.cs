using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public GameObject waterBalloonPrefab;
    private GameManager gameManager;
    public float cellSize = 1f;

    public int water_explosionRange = 1;

    public int maxWaterBalloons = 1; // 플레이어가 최대로 놓을 수 있는 물풍선 개수
    public int currentWaterBalloons = 0; // 현재 설치된 물풍선의 개수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        gameManager = FindObjectOfType<GameManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //move player
        Vector2 moveDirection = new Vector2(x, y).normalized;
        rb.linearVelocity = moveDirection * speed;

        //if get space input, locate waterballoon
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }
        }
    }

    void PlaceBomb()
    {
        // 🚨 현재 설치된 물풍선 개수가 최대 개수보다 적을 때만 설치
        if (currentWaterBalloons < maxWaterBalloons)
        {
            Vector3 playerPosition = transform.position;

            float roundedX = Mathf.Round(playerPosition.x / cellSize) * cellSize;
            float roundedY = Mathf.Round(playerPosition.y / cellSize) * cellSize;

            Vector3 spawnPosition = new Vector3(roundedX, roundedY, playerPosition.z);

            // 물풍선을 생성하고 개수를 증가시킵니다.
            Instantiate(waterBalloonPrefab, spawnPosition, Quaternion.identity);
            currentWaterBalloons++;
        }
    }

    
}
