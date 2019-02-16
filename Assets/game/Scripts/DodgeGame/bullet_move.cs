using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move : MonoBehaviour {

    private float MoveSpeed;     // 미사일이 날라가는 속도
    public float MinSpeed = 0.3f;     // 미사일이 날라가는 속도
    public float MaxSpeed = 1.0f;     // 미사일이 날라가는 속도
    public GameObject _player;

    public float DestroyXPos;   // 미사일이 사라지는 지점
    public float DestroyYPos;   // 미사일이 사라지는 지점

    private Vector2 dir;

    void Start()
    {
        dir.x = Random.Range(-5.0f, 5.0f);
        dir.y = Random.Range(-5.0f, 5.0f);

        MoveSpeed = Random.Range(MinSpeed, MaxSpeed);
    }

    void Update()
    {
        // 매 프레임마다 미사일이 MoveSpeed 만큼 up방향(Y축 +방향)으로 날라갑니다.
        //transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        transform.Translate(dir * MoveSpeed * Time.deltaTime);

        // 만약에 미사일의 위치가 DestroyYPos를 넘어서면
        if (transform.position.x >= DestroyXPos || transform.position.y >= DestroyYPos ||
            transform.position.x <= -DestroyXPos || transform.position.y <= -DestroyYPos)
        {
            // 미사일을 제거
            //Destroy(gameObject);
            GetComponent<Collider2D>().enabled = false;

        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // 부딛히는 collision을 가진 객체의 태그가 "Enemy"일 경우
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("총알 충돌");
    //        GetComponent<Collider2D>().enabled = false;

    //        Debug.Log("OnTriggerEnter2D" + _player.GetComponent<PlayerHealth>());
    //        _player.GetComponent<PlayerHealth>().TakeDamage(10);
    //    }
    //}

}
