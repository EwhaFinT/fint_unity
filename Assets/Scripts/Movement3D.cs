using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;     // 이동 속도
    private Vector3 moveDirection;      // 이동 방향

    private float gravity = -9.81f;     // 중력 계수
    [SerializeField]
    private float jumpForce = 3.0f;     // 뛰는 힘

    [SerializeField]
    private Transform cameraTransform;   // 카메라 Transform 컴포넌트

    private CharacterController characterController;    // (player에 characterController 추가한 상태)

    public void Awake()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.isGrounded == false)     // 중력 적용
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        Vector3 movedis = cameraTransform.rotation * direction;
        moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);
    }

    public void SetPosition(Vector3 pos)
    {
        var offset = pos - characterController.transform.position;
        characterController.enabled = false;
        characterController.transform.position = pos;
        characterController.enabled = true;
    }

    public void JumpTo() // 점프 함수
    {
        if (characterController.isGrounded == true)
        {
            moveDirection.y = jumpForce;
        }
    }
}