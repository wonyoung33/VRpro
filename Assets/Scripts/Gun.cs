using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletImpact;      // �Ѿ� ���� ȿ��
    ParticleSystem bulletEffect;        // �Ѿ� ���� ��ƼŬ �ý���
    AudioSource bulletAudio;            // �Ѿ� �߻� ����
    public Transform crosshair;         // crosshair�� ���� �Ӽ�

    void Start()
    {
        // �Ѿ� ȿ�� ��ƼŬ �ý��� ������Ʈ ��������
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();

        // �Ѿ� ȿ�� ����� �ҽ� ������Ʈ ��������
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
    }
        
   
    void Update()
    {
        // ũ�ν���� ǥ��
        ARAVRInput.DrawCrosshair(crosshair);


       // ����ڰ� Index Trigger ��ư�� ������
       if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            // �Ѿ� ����� ���
            bulletAudio.Stop();
            bulletEffect.Play();

            // Ray�� ī�޶��� ��ġ�κ��� �������� �����.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);

            // Ray�� �浹 ������ �����ϱ����� ���� ����
            RaycastHit hitInfo;

            // �÷��̾� ���̾� ������
            int playerLayer = 1 << LayerMask.NameToLayer("Player");

            // Ÿ�� ���̾� ������
            int towerLayer = 1 << LayerMask.NameToLayer("Tower");
            int layerMask = playerLayer | towerLayer;

            // Ray�� ���. ray�� �ε��� ������ hitinfo�� ����.
            if (Physics.Raycast(ray, out  hitInfo, 200, ~layerMask))
            {
                // �Ѿ� ����Ʈ�� ����ǰ� ������ ���߰� ���
                bulletEffect.Stop();
                bulletEffect.Play();
                
                // �ε��� ���� �ٷ� ������ ����Ʈ�� ���̵��� ����
                bulletImpact.position = hitInfo.point;

                // �ε��� ������ �������� �Ѿ� ����Ʈ�� ������ ����
                bulletImpact.forward = hitInfo.normal;
            }

        }
    }
}
