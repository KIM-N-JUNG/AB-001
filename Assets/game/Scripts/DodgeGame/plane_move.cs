using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class plane_move : MonoBehaviour {
    public Joystick joystick;   //조이스틱 스크립트
    public float MoveSpeed;     //플레이어 이동속도

    public Canvas controlPanel;

    private BoxCollider2D shuttleBox;

    private Vector3 _moveVector; //플레이어 이동벡터
    private Transform _transform;

    void Start()
    {
        _transform = transform;      //Transform 캐싱
        _moveVector = Vector3.zero;  //플레이어 이동벡터 초기화
        shuttleBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ActiveScene : " + (SceneManager.GetActiveScene().buildIndex == 0 ? "Main Menu" : "Play Mode"));

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }

        // 매 프레임마다 메소드 호출
        //MoveKey();

        //터치패드 입력 받기
        HandleInput();
    }

    void FixedUpdate()
    {
        //플레이어 이동
        Move();
    }

    // 움직이는 기능을 하는 메소드
    private void MoveKey()
    {
        if (Input.GetKey(KeyCode.UpArrow))  // ↑ 방향키를 누를 때
        {
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }
        if (Input.GetKey(KeyCode.DownArrow))  // ↓ 방향키를 누를 때
        {
            transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))  // → 방향키를 누를 때
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))  // ← 방향키를 누를 때
        {
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
    }

    public void HandleInput()
    {
        _moveVector = PoolInput();
    }

    public Vector3 PoolInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();

        bool flag = SingletonClass.Instance.acceleration;
        //Debug.Log("acceleration : " + flag);

        Vector3 moveDir;
        if (flag)
        {
            moveDir = new Vector3(h, v, 0);
            //Debug.Log("SetAcceleration : 111");
        } else
        {
            moveDir = new Vector3(h, v, 0).normalized;
            //Debug.Log("SetAcceleration : 222");
        }

        return moveDir;
    }

    public void Move()
    {
        Vector3 tmp = _moveVector * MoveSpeed * Time.deltaTime;
        Vector3 curr = _transform.position;
        _transform.Translate(tmp);
        curr = _transform.position;

        Vector3 shuttleMin = Camera.main.WorldToScreenPoint(shuttleBox.bounds.min);
        Vector3 shuttleMax = Camera.main.WorldToScreenPoint(shuttleBox.bounds.max);

        float shuttleWidth  = shuttleMax.x - shuttleMin.x;
        float shuttleHeight = shuttleMax.y - shuttleMin.y;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(curr);
        int width = Screen.width;
        int height = Screen.height;
        if (screenPos.x < shuttleWidth / 2)
        {
            screenPos.x = shuttleWidth / 2;
        }
        else if (screenPos.x > width - (shuttleWidth / 2))
        {
            screenPos.x = width - (shuttleWidth / 2);
        }

        if (screenPos.y < controlPanel.transform.position.y * 2.2f)
        {
            screenPos.y = controlPanel.transform.position.y * 2.2f;
        }
        else if (screenPos.y > height - (shuttleHeight))
        {
            screenPos.y = height - (shuttleHeight);
        }

        Vector3 finalPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.SetPositionAndRotation(finalPos, new Quaternion());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딛히는 collision을 가진 객체의 태그가 "Enemy"일 경우
        if (collision.CompareTag("Bullets"))
        {
            //Debug.Log("총알 충돌");
            bullet_move bullet = collision.GetComponent<bullet_move>();
            Vector3 position = collision.transform.position;
            GetComponent<PlayerHealth>().TakeDamage(10, position);
            collision.enabled = false;
        }
    }

    public void SetAcceleration(bool flag)
    {
        SingletonClass.Instance.acceleration = flag;

        Debug.Log("SetAcceleration : " + flag);
    }
}
