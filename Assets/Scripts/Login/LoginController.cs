using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
        StartCoroutine(Login());
    }

    void SignupBtnClick()
    {
        OffWarn();
//        OffLogin();
        var signup = UIManager.Instance.popupSignup.GetComponent<SignupController>();
        signup.show();
    }

    void FindIdBtnClick()
    {
        OffWarn();
//        OffLogin();
        var findid = UIManager.Instance.popupFindId.GetComponent<FindIdController>();
        findid.show();
    }

    void FindPwBtnClick()
    {
        OffWarn();
 //       OffLogin();
        var findPw = UIManager.Instance.popupFindPw.GetComponent<FindPwController>();
        findPw.show();
    }

    void OffLogin()
    {
        login.SetActive(false);
    }

    void OffWarn()
    {
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        popupWarn.OffWarn();
    }

    IEnumerator Login() // 로그인 요청
    {
        string url = "https://fintribe.herokuapp.com/v1/login?identity=" + identity.text + "&password=" + password.text;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<LoginResponse>(jsonString);
        
        if (response.userId == "")    // 로그인 실패
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn(response.message);
        }
        else
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.OffWarn();
            OffLogin();
            // TODO : userId 저장 후 메인 페이지로 랜더링
            Manager.Instance.ID = response.userId;
            Debug.Log(Manager.Instance.ID);
        }
    }
}

class LoginResponse
{
    public string userId;
    public string message;
}