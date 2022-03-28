using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;       // ī�޶� �����ϴ� ���
    
    [SerializeField]
    private float xMoveSpeed = 500; // ī�޶��� y�� ȸ�� �ӵ�
    [SerializeField]
    private float yMoveSpeed = 250; // ī�޶��� x�� ȸ�� �ӵ�
    private float yMinLimit = 5;    // ī�޶� x�� ȸ�� ���� �ּ� ��
    private float yMaxLimit = 80;   // ī�޶� x�� ȸ�� ���� �ִ� ��
    private float x, y;             // ���콺 �̵� ���� ��
    private float distance = 3;     // ī�޶�� target�� �Ÿ�

    private void Awake()    // (�ʱ�ȭ �Լ�)
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // ������ ���콺 ��ư�� ������ ī�޶� ȸ��
    private void Update()
    {
        if (target == null) return;

        if (Input.GetMouseButton(1))    // (1 : ������ ��ư)
        {
            x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            transform.rotation = Quaternion.Euler(y, x, 0); // ī�޶� ȸ�� ���� ����
        }
    }

    // ī�޶� ��ġ ����
    private void LateUpdate()
    {
        if (target == null) return;

        // (target���� distacne��ŭ ������)
        transform.position = transform.rotation * new Vector3(0, 1.5f, -distance) + target.position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}

// 1. Main Camera�� Camera Controller ����
// 2. Target���� Player ����