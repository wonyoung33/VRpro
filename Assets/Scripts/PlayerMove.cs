using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed = 5;
    
    // CharacterController ������Ʈ
    CharacterController cc;

    // �߷� ���ӵ��� ũ��
    
    public float gravity = -20;
    
    // ���� �ӵ�
    
    float yVelocity = 0;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // ������� �Է¿� ���� �����·� �̵��ϰ� �ʹ�.

        // 1.������� �Է��� �޴´�.
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");

        // 2.������ �����.
        Vector3 dir = new Vector3(h, 0, v);

        // 2.1�߷��� ������ ���� ���� �߰� v=v0+at
        yVelocity += gravity * Time.deltaTime;

        // 2.2�ٴڿ� ���� ���, ���� �׷��� ó���ϱ� ���� �ӵ��� 0���� ����.
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        dir.y = yVelocity;
        // 3.�̵��Ѵ�.
        cc.Move(dir * speed * Time.deltaTime);
    }
}
