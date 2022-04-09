using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public void show()
    {
        signup.SetActive(true);
    }

    void CheckDupIdClick()
    {
        // ==== ¾ÆÀÌµð Áßº¹ °Ë»ç =====
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
            // ==== ¹é¿£µå¿Í Åë½Å ====
            // ==== ex. È¸¿ø°¡ÀÔ ¿Ï·á½Ã ·Î±×ÀÎ ÆäÀÌÁö·Î ·£´õ¸µ ====
            UIManager.Instance.OnLogin();
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
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("¾ÆÀÌµð¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        if(isDupId)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("¾ÆÀÌµð Áßº¹ È®ÀÎÀ»\nÁøÇàÇØÁÖ¼¼¿ä.");
            return false;
        }
        return true;
    }

    bool CheckPassword()
    {
        string val = password.text;
        if (val == null)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("ºñ¹Ð¹øÈ£¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        Regex eng = new Regex(@"[a-zA-Z]");
        Regex num = new Regex(@"[0-9]");
        if (val.Length < 8 || !eng.IsMatch(val) || !num.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("¿µ¹®, ¼ýÀÚ¸¦ ÇÏ³ª ÀÌ»ó Æ÷ÇÔÇÑ\n8ÀÚ¸® ºñ¹Ð¹øÈ£¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        return true;
    }

    bool CheckPasswordConfirm()
    {
        if (password.text != passwordConfirm.text)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("ºñ¹Ð¹øÈ£°¡ ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.");
            return false;
        }
        return true;
    }

    bool CheckName()
    {
        string val = userName.text;
        if (val == null)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("ÀÌ¸§À» ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        Regex kor = new Regex(@"^[°¡-ÆR]+$");
        if (val.Length < 2 || val.Length > 5 || !kor.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("2±ÛÀÚ ÀÌ»ó 5±ÛÀÚ ÀÌÇÏ\nÇÑ±Û¸¸ ÀÔ·Â °¡´ÉÇÕ´Ï´Ù.");
            return false;
        }
        return true;
    }

    bool CheckPhone()
    {
        if (phone.text == null)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("ÀüÈ­¹øÈ£¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        return true;
    }
    bool CheckEmail()
    {
        if (email.text == null)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("ÀÌ¸ÞÀÏÀ» ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        return true;
    }

    void OffSignup()
    {
        signup.SetActive(false);
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        popupWarn.OffWarn();
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