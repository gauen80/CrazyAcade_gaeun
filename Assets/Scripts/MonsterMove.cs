using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed = 3f;
    private int isVertical;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    public float Screenlength = 5f;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        
        //몬스터 생성시 상하 좌우 하나의 축을 선택
        isVertical = Random.Range(0, 2);
        if (isVertical==0)
        {
            moveDirection = Random.Range(0,2) == 0 ? Vector2.up : Vector2.down;
        }
        else
        {
            moveDirection = Random.Range(0,2) == 0 ? Vector2.left : Vector2.right;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   if(gameManager.isGameActive)
        // 물리 업데이트(FixedUpdate)에서 몬스터를 지속적으로 이동시킵니다.
        rb.linearVelocity = moveDirection * speed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag("Wall"))
        //{
        // 부딪히면 방향 바꿈
            moveDirection *= -1;
        //}
    }
}
