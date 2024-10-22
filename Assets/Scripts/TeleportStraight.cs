using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportStraight : MonoBehaviour
{
    // �ڷ���Ʈ�� ǥ���� UI
    public Transform teleportCircleUI;

    // ���� �׸� ���� ������
    LineRenderer Ir;

    private void Start()
    {
        // ������ �� ��Ȱ��ȭ�Ѵ�.
        teleportCircleUI.gameObject.SetActive(false);

        // ���� ������ ������Ʈ ������
        Ir = GetComponent<LineRenderer>();
    }


    // ���� �ڷ���Ʈ UI�� ũ��
    Vector3 originScale = Vector3.one * 0.2f;
    private void Update()
    {
        // ���� ��Ʈ�ѷ��� One ��ư�� ������ ���� ��
        if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // ���� ������ ������Ʈ Ȱ��ȭ
            Ir.enabled = true;
        }
        // ���� ��Ʈ�ѷ���One ��ư���� ���� ����
        else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // ���η����� ��Ȱ��ȭ
            Ir.enabled = false;
            if (teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                // �ڷ���Ʈ UI ��ġ�� ���� �̵�
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }

            // �ڷ���Ʈ UI ��Ȱ��ȭ
            teleportCircleUI.gameObject.SetActive(false);
        }
        // ���� ��Ʈ�ѷ��� One��ư�� ������ ���� ��
        else if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {

        }
        {

            

            // 1.���� ��Ʈ�ѷ��� �������� Ray�� �����.
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");

            // 2.Terrain�� Ray �浹 ����
            if (Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                // 3.Ray�� �ε��� ������ ���� �׸���
                Ir.SetPosition(0, ray.origin);
                Ir.SetPosition(1, hitInfo.point);

                // 4.Ray�� �ε��� ������ �ڷ���Ʈ UIǥ��
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point;

                // �ڷ���ƮUI�� ���� ���� �ֵ��� ���� ����
                teleportCircleUI.forward = hitInfo.normal;

                // �ڷ���Ʈ UI�� ũ�Ⱑ �Ÿ��� ���� �����ǵ��� ����
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                // Ray�浹�� �߻����� ������ Ray �������� �׷������� ó��
                Ir.SetPosition(0, ray.origin);
                Ir.SetPosition(1, ray.origin + ARAVRInput.LHandPosition * 200);

                // �ڷ���Ʈ UI�� ȭ�鿡�� ��Ȱ��ȭ
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
