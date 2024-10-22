using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 이동 속도
    public float speed = 5;
    
    // CharacterController 컴포넌트
    CharacterController cc;

    // 중력 가속도의 크기
    
    public float gravity = -20;
    
    // 수직 속도
    
    float yVelocity = 0;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 사용자의 입력에 따라 전후좌로 이동하고 싶다.

        // 1.사용자의 입력을 받는다.
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        // 2.방향을 만든다.
        Vector3 dir = new Vector3(h, 0, v);

        // 2.1중력을 적용한 수직 방향 추가 v=v0+at
        yVelocity += gravity * Time.deltaTime;

        // 2.2바닥에 있을 경우, 수직 항력을 처리하기 위해 속도를 0으롷 나다.
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        dir.y = yVelocity;
        // 3.이동한다.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
