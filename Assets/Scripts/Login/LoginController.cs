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
            popupWarn.MakePopupWarn("아이디 또는 비밀번호가\n일치하지 않습니다.");

        } else
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.OffWarn();
            // ==== userId 저장 후 메인 페이지로 랜더링 ====
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
        // ==== 백엔드와 통신해서 로그인 성공 여부 받아오기 ====
        return false;
    }
}

class LoginRequest {
    public string identity;
    public string password;
}