using UnityEngine;
using System.Collections;

public class WaterBalloonController : MonoBehaviour
{
    public GameObject centerPrefab;
    public GameObject verticalPrefab;
    public GameObject HorizontalPrefab;

    public int explosionRange = 2;
    public float cellSize = 1f;
    public float delay = 3f;
    private GameManager gameManager;
    private PlayerController playerController; // 🚨 플레이어 컨트롤러 변수 추가

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>(); // 🚨 플레이어 컨트롤러 찾기
        
        if (IsNearExplosion())
        {
            ExplodeImmediately();
        }
        else
        {
            StartCoroutine(ExplosionCoroutine(delay));
        }
    }

    IEnumerator ExplosionCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChainExplode();
        // 🚨 물풍선이 폭발한 후 개수를 감소시킵니다.
        if (playerController != null)
        {
            playerController.currentWaterBalloons--;
        }
        Destroy(gameObject);
    }
    
    bool IsNearExplosion()
    {
        LayerMask mask = ~LayerMask.GetMask("Player", "Monster");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cellSize * 0.5f, mask);

        foreach(Collider2D collider in colliders)
        {
            if (collider.GetComponent<WaterStream>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void ExplodeImmediately()
    {
        if (gameObject.activeSelf)
        {
            StopAllCoroutines(); 
            ChainExplode();
            // 🚨 물풍선이 즉시 폭발한 후 개수를 감소시킵니다.
            if (playerController != null)
            {
                playerController.currentWaterBalloons--;
            }
            Destroy(gameObject);
        }
    }

    void ChainExplode()
    {
        Explode();
        
        ScanForWaterBalloons(Vector2.up);
        ScanForWaterBalloons(Vector2.down);
        ScanForWaterBalloons(Vector2.left);
        ScanForWaterBalloons(Vector2.right);
    }

    void Explode()
    {
        Instantiate(centerPrefab, transform.position, Quaternion.identity);

        CreateStreams(Vector2.up, verticalPrefab);
        CreateStreams(Vector2.down, verticalPrefab);
        CreateStreams(Vector2.left, HorizontalPrefab);
        CreateStreams(Vector2.right, HorizontalPrefab);
    }
    
    void ScanForWaterBalloons(Vector2 direction)
    {
        for (int i = 1; i <= explosionRange; i++)
        {
            Vector2 checkPosition = (Vector2)transform.position + direction * cellSize * i ;
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, direction, 0.1f);
            
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("WaterBalloon"))
                {
                    WaterBalloonController otherBalloon = hit.collider.GetComponent<WaterBalloonController>();
                    if (otherBalloon != null)
                    {
                        otherBalloon.ExplodeImmediately();
                    }
                }
                
                if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Wall"))
                {
                    break;
                }
            }
        }
    }

    void CreateStreams(Vector2 direction, GameObject prefab)
    {
        for (int i = 1; i <= explosionRange; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + direction * cellSize * i* 0.5f;
            RaycastHit2D hit = Physics2D.Raycast(spawnPosition, direction, 0.1f);
            
            if (hit.collider != null && (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Wall")))
            {
                break;
            }
            else
            {
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}