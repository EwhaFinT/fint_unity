using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginController : MonoBehaviour
{
    public GameObject login;
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
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("���̵� �Ǵ� ��й�ȣ��\n��ġ���� �ʽ��ϴ�.");

        } else
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.OffWarn();
            // ==== userId ���� �� ���� �������� ������ ====
        }
    }

    void SignupBtnClick()
    {
        OffLogin();
        var signup = UIManager.Instance.popupSignup.GetComponent<SignupController>();
        signup.show();
    }

    void FindIdBtnClick()
    {
        OffLogin();
        var findid = UIManager.Instance.popupFindId.GetComponent<FindIdController>();
        findid.show();
    }

    void FindPwBtnClick()
    {
        OffLogin();
        var findPw = UIManager.Instance.popupFindPw.GetComponent<FindPwController>();
        findPw.show();
    }

    void OffLogin()
    {
        login.SetActive(false);
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