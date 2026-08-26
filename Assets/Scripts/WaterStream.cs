using UnityEngine;

public class WaterStream : MonoBehaviour
{
    // 폭발 조각이 유지될 시간
    public float explosionLifeTime = 1.5f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // 일정 시간 후에 이 게임 오브젝트를 파괴
        Destroy(gameObject, explosionLifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그가 "WaterBalloon"인지 확인
        if (other.CompareTag("WaterBalloon"))
        {
            // 다른 물풍선과 충돌하면, 해당 물풍선 스크립트의 ExplodeImmediately()를 즉시 호출
            WaterBalloonController otherBalloon = other.GetComponent<WaterBalloonController>();
            if (otherBalloon != null)
            {
                otherBalloon.ExplodeImmediately();
            }
        }
        else if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
            gameManager.MonsterDied();
        }
        else if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
    }

}