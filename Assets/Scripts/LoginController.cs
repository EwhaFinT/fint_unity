using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginController : MonoBehaviour
{
    public TMP_InputField identity;
    public TMP_InputField password;
    public Button loginBtn;
    public Button signupBtn;
    public Button findIdBtn;
    public Button findPwBtn;


    void Start()
    {
        loginBtn.onClick.AddListener(LoginBtnClick);
        signupBtn.onClick.AddListener(SignupBtnClick);
        findIdBtn.onClick.AddListener(FindIdBtnClick);
        findPwBtn.onClick.AddListener(FindPwBtnClick);
    }

    void Update()
    {

    }

    void LoginBtnClick()
    {
        bool isValid = IsValidUser();
        if(!isValid)
        {
            UIManager.Instance.PopupWarn(true, "���̵� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.");

        } else
        {
            UIManager.Instance.PopupWarn(false, "");
            // ==== userId ���� �� ���� �������� ������ ====
        }
    }

    void SignupBtnClick()
    {
        OffLogin();
        UIManager.Instance.OnSignup();
    }

    void FindIdBtnClick()
    {
        OffLogin();
        UIManager.Instance.OnFindId();
    }

    void FindPwBtnClick()
    {
        OffLogin();
        UIManager.Instance.OnFindPw();
    }

    void OffLogin()
    {
        UIManager.Instance.OffLogin();
        UIManager.Instance.OffWarn();
    }

    bool IsValidUser()
    {
        LoginRequest request = new LoginRequest
        {
            identity = identity.text,
            password = password.text
        };
        Debug.Log("id: " + request.identity);
        // ==== �鿣��� ����ؼ� �α��� ���� ���� �޾ƿ��� ====
        return false;
    }
}

class LoginRequest {
    public string identity;
    public string password;
}