using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Movement3D movement3D;
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.Space;

    public float sensitivity = 5;

    public Camera mainCamera;
    private float m_HorizontalAngle, m_VerticalAngle;
    public GameObject ui;
    GameObject[] popupCanvas;

    public void Start()
    {
        popupCanvas = new GameObject[ui.transform.childCount];
        for (int i = 3; i < popupCanvas.Length; i++)
        {
            popupCanvas[i] = ui.transform.GetChild(i).gameObject;
        }
    }
    public void Awake()
    {
        movement3D = gameObject.GetComponent<Movement3D>();
    }

    void FixedUpdate()
    {
        //Turn();
        //LookUp();
        //        GameObject[] uiCanvas = {UIManager.Instance.popupArtInfo, UIManager.Instance.popupLogin};
        //       if (!UIManager.Instance.popupArtInfo.activeSelf);

        //Debug.Log(ui.transform.childCount);
        //Debug.Log(ui.transform.GetChild(0));
        //Debug.Log(ui.transform.GetChild(1));

        //transform[] allchildren = ui.getcomponentsinchildren<transform>(true);
        //transform[] list = { };

        //GameObject[] a = transform.GetComponentsInChildren;
        //GameObject.FindGameObjectWithTag("")(true);
        //Turn();
        //LookUp();
        bool notActiving = true;

        for(int i=3; i< popupCanvas.Length; i++)
        {
            if (popupCanvas[i].activeSelf == true)
                notActiving = false;
        }
        if (notActiving)
        {
            Turn();
            LookUp();
        }
    }

    private void Turn()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float turnPlayer = rotateHorizontal * sensitivity;
        m_HorizontalAngle = m_HorizontalAngle + turnPlayer;
        if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
        if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;
        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.y = m_HorizontalAngle;
        transform.localEulerAngles = currentAngles;
    }
    private void LookUp()
    {
        float rotateVertical = Input.GetAxis("Mouse Y");
        var turnCam = -rotateVertical * sensitivity;
        m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
        Vector3 currentAngles = mainCamera.transform.localEulerAngles;
        currentAngles.x = m_VerticalAngle;
        mainCamera.transform.localEulerAngles = currentAngles;
    }

    // 상하좌우 이동 및 점프
    private void Update()
    {
        // x, z 방향 이동
        float x = Input.GetAxisRaw("Horizontal");   // 방향키 좌/우 움직임
        float z = Input.GetAxisRaw("Vertical");     // 방향키 위/아래 움직임

        movement3D.MoveTo(new Vector3(x, 0, z));

        // 점프 (스페이스바 누르면 작동)
        if (Input.GetKeyDown(jumpKeyCode))
        {
            movement3D.JumpTo();
        }
    }

    public void SetMove(Vector3 pos)
    {
        Debug.Log("setmove : " + pos);
        movement3D.SetPosition(pos);
    }

}
// 1. Player에 Movement3D 추가 & Camera Transform으로 Main Camera 설정
// 2. Player에 PlayerController 추가 & Camera Controller로 Main Camera 설정