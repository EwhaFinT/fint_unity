using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class SignupController : MonoBehaviour
{
    public TMP_InputField identity;
    public TMP_InputField password;
    public TMP_InputField passwordConfirm;
    public TMP_InputField userName;
    public TMP_InputField phone;
    public TMP_InputField email;
    public Button checkDupIdBtn;
    public Button signupBtn;
    public Button exitBtn;

    bool isDupId = true;

    void Start()
    {
        checkDupIdBtn.onClick.AddListener(CheckDupIdClick);
        signupBtn.onClick.AddListener(SignupBtnClick);
        exitBtn.onClick.AddListener(OffSignup);
    }

    void Update()
    {
        
    }

    void CheckDupIdClick()
    {
        // ==== 아이디 중복 검사 =====
        isDupId = false;
    }

    void SignupBtnClick()
    {
        if(CheckValidation())
        {
            SignupRequest request = new SignupRequest
            {
                identity = identity.text,
                password = password.text,
                name = userName.text,
                phone = phone.text,
                email = email.text
            };
            Debug.Log("id: " + request.identity);
            // ==== 백엔드와 통신 ====
            // ==== ex. 회원가입 완료시 로그인 페이지로 랜더링 ====
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
        if (identity.text == null)
        {
            UIManager.Instance.PopupWarn(true, "아이디를 입력하세요.");
            return false;
        }
        if(isDupId)
        {
            UIManager.Instance.PopupWarn(true, "아이디 중복 확인을 진행해주세요.");
            return false;
        }
        return true;
    }

    bool CheckPassword()
    {
        string val = password.text;
        if (val == null)
        {
            UIManager.Instance.PopupWarn(true, "비밀번호를 입력하세요.");
            return false;
        }
        Regex eng = new Regex(@"[a-zA-Z]");
        Regex num = new Regex(@"[0-9]");
        if (val.Length < 8 || !eng.IsMatch(val) || !num.IsMatch(val))
        {
            UIManager.Instance.PopupWarn(true, "영문, 숫자를 하나 이상 포함한\n8자리 비밀번호를 입력하세요.");
            return false;
        }
        return true;
    }

    bool CheckPasswordConfirm()
    {
        if (password.text != passwordConfirm.text)
        {
            UIManager.Instance.PopupWarn(true, "비밀번호가 일치하지 않습니다.");
            return false;
        }
        return true;
    }

    bool CheckName()
    {
        string val = userName.text;
        if (val == null)
        {
            UIManager.Instance.PopupWarn(true, "이름을 입력하세요.");
            return false;
        }
        Regex kor = new Regex(@"^[가-힣]+$");
        if (val.Length < 2 || val.Length > 5 || !kor.IsMatch(val))
        {
            UIManager.Instance.PopupWarn(true, "2글자 이상 5글자 이하\n한글만 입력 가능합니다.");
            return false;
        }
        return true;
    }

    bool CheckPhone()
    {
        if (phone.text == null)
        {
            UIManager.Instance.PopupWarn(true, "전화번호를 입력하세요.");
            return false;
        }
        return true;
    }
    bool CheckEmail()
    {
        if (email.text == null)
        {
            UIManager.Instance.PopupWarn(true, "이메일을 입력하세요.");
            return false;
        }
        return true;
    }

    void OffSignup()
    {
        UIManager.Instance.OffSignup();
        UIManager.Instance.OffWarn();
    }
}

class SignupRequest
{
    public string identity;
    public string password;
    public string name;
    public string phone;
    public string email;
}