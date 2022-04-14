using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;

public class SignupController : MonoBehaviour
{
    public GameObject signup;
    public TMP_InputField identity;
    public TMP_InputField password;
    public TMP_InputField passwordConfirm;
    public TMP_InputField userName;
    public TMP_InputField phone;
    public TMP_InputField email;
    public Button checkDupIdBtn;
    public Button signupBtn;
    public Button exitBtn;

    bool dupCheck = false;

    void Start()
    {
        checkDupIdBtn.onClick.AddListener(CheckDupIdClick);
        signupBtn.onClick.AddListener(SignupBtnClick);
        exitBtn.onClick.AddListener(OffSignup);
    }

    void Update()
    {
        
    }

    public void show()
    {
        signup.SetActive(true);
    }

    void CheckDupIdClick()
    {
        StartCoroutine(IdCheck());
    }

    void SignupBtnClick()
    {
        if(CheckValidation())
        {
            StartCoroutine(Signup());
        }
    }

    bool CheckValidation()
    {
        if (!CheckId()) return false;
        if (!CheckPassword()) return false;
        if (!CheckPasswordConfirm()) return false;
        if (!CheckName()) return false;
        if (!CheckPhone()) return false;
        if (!CheckEmail()) return false ;
        return true;
    }

    bool CheckId()
    {
        if (identity.text.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("아이디를 입력하세요.");
            return false;
        }
        if(!dupCheck)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("아이디 중복 확인을\n진행해주세요.");
            return false;
        }
        return true;
    }

    bool CheckPassword()
    {
        string val = password.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("비밀번호를 입력하세요.");
            return false;
        }
        Regex eng = new Regex(@"[a-zA-Z]");
        Regex num = new Regex(@"[0-9]");
        if (val.Length < 8 || !eng.IsMatch(val) || !num.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("영문, 숫자를 하나 이상 포함한\n8자리 비밀번호를 입력하세요.");
            return false;
        }
        return true;
    }

    bool CheckPasswordConfirm()
    {
        if (password.text != passwordConfirm.text)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("비밀번호가 일치하지 않습니다.");
            return false;
        }
        return true;
    }

    bool CheckName()
    {
        string val = userName.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("이름을 입력하세요.");
            return false;
        }
        Regex kor = new Regex(@"^[가-힣]+$");
        if (val.Length < 2 || val.Length > 5 || !kor.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("2글자 이상 5글자 이하\n한글만 입력 가능합니다.");
            return false;
        }
        return true;
    }

    bool CheckPhone()
    {
        string val = phone.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("전화번호를 입력하세요.");
            return false;
        }
        Regex num = new Regex(@"^01[016789][^0][0-9]{2,3}[0-9]{3,4}$");
        if(!num.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("올바르지 않은\n전화번호 입니다.");
            return false;
        }
        return true;
    }
    bool CheckEmail()
    {
        string val = email.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("이메일을 입력하세요.");
            return false;
        }
        Regex mail = new Regex(@"[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}");
        if(!mail.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("올바르지 않은\n이메일 주소 입니다..");
            return false;
        }
        return true;
    }

    void OffSignup()
    {
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        popupWarn.OffWarn();
        signup.SetActive(false);
    }

    IEnumerator IdCheck() // 아이디 중복 검사 요청
    {
        string url = "http://localhost:8080/v1/check-id?identity=" + identity.text;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<IdCheckResponse>(jsonString);

        if (response.idCheckSuccess == 0)    // 아이디 중복
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("중복된 아이디가 존재합니다.");
        }
        else                                // 사용할 수 있는 아이디
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("사용 가능한 아이디입니다.");
            dupCheck = true;
        }
    }

    IEnumerator Signup() // 아이디 중복 검사 요청
    {
        string url = "http://localhost:8080/v1/signup";

        SignupRequest signupRequest = new SignupRequest
        {
            identity = identity.text,
            password = password.text,
            name = userName.text,
            phone = phone.text,
            email = email.text
        }; 
        string jsonBody = JsonUtility.ToJson(signupRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<SignupResponse>(jsonString);

        if (response.signupSuccess == 0)    // 회원가입 실패
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("중복된 아이디가 존재합니다.");
            dupCheck = false;
        }
        else                                // 회원가입 성공
        {
            OffSignup();
            UIManager.Instance.OnLogin();
        }
    }
}

class IdCheckResponse
{
    public int idCheckSuccess;
}

class SignupRequest
{
    public string identity;
    public string password;
    public string name;
    public string phone;
    public string email;
}

class SignupResponse
{
    public int signupSuccess;
}