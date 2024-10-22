using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeleportStraight : MonoBehaviour
{
    // 텔레포트를 표시할 UI
    public Transform teleportCircleUI;

    // 선을 그릴 라인 렌더러
    LineRenderer Ir;

    private void Start()
    {
        // 시작할 때 비활성화한다.
        teleportCircleUI.gameObject.SetActive(false);

        // 라인 렌더러 컴포넌트 얻어오기
        Ir = GetComponent<LineRenderer>();
    }


    // 최초 텔레포트 UI의 크기
    Vector3 originScale = Vector3.one * 0.2f;
    private void Update()
    {
        // 왼쪽 컨트롤러의 One 버튼을 누르고 있을 떄
        if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인 렌더러 컴포넌트 활성화
            Ir.enabled = true;
        }
        // 왼쪽 컨트롤러의One 버튼에서 손을 떼면
        else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인렌더러 비활성화
            Ir.enabled = false;
            if (teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                // 텔레포트 UI 위치로 순간 이동
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }

            // 텔레포트 UI 비활성화
            teleportCircleUI.gameObject.SetActive(false);
        }
        // 왼쪽 컨트롤러의 One버튼을 누르고 있을 때
        else if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {

        }
        {

            

            // 1.왼쪽 컨트롤러를 기준으로 Ray를 만든다.
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");

            // 2.Terrain만 Ray 충돌 검출
            if (Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                // 3.Ray가 부딪힌 지점에 라인 그리기
                Ir.SetPosition(0, ray.origin);
                Ir.SetPosition(1, hitInfo.point);

                // 4.Ray가 부딪힌 지점에 텔레포트 UI표시
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point;

                // 텔러포트UI가 위로 누워 있도록 방향 설정
                teleportCircleUI.forward = hitInfo.normal;

                // 텔레포트 UI의 크기가 거리에 따라 보정되도록 설정
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                // Ray충돌이 발생하지 않으면 Ray 방향으로 그려지도록 처리
                Ir.SetPosition(0, ray.origin);
                Ir.SetPosition(1, ray.origin + ARAVRInput.LHandPosition * 200);

                // 텔레포트 UI는 화면에서 비활성화
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
