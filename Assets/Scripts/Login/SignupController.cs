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

    bool isDupId = true;

    void Start()
    {
        checkDupIdBtn.onClick.AddListener(CheckDupIdClick);
        signupBtn.onClick.AddListener(SignupBtnClick);
    }

    void Update()
    {
        
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
            UIManager.Instance.PopupWarn(true, "¾ÆÀÌµð¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        if(isDupId)
        {
            UIManager.Instance.PopupWarn(true, "¾ÆÀÌµð Áßº¹ È®ÀÎÀ» ÁøÇàÇØÁÖ¼¼¿ä.");
            return false;
        }
        return true;
    }

    bool CheckPassword()
    {
        string val = password.text;
        if (val == null)
        {
            UIManager.Instance.PopupWarn(true, "ºñ¹Ð¹øÈ£¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        Regex eng = new Regex(@"[a-zA-Z]");
        Regex num = new Regex(@"[0-9]");
        if (val.Length < 8 || !eng.IsMatch(val) || !num.IsMatch(val))
        {
            UIManager.Instance.PopupWarn(true, "¿µ¹®, ¼ýÀÚ¸¦ ÇÏ³ª ÀÌ»ó Æ÷ÇÔÇÑ\n8ÀÚ¸® ºñ¹Ð¹øÈ£¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        return true;
    }

    bool CheckPasswordConfirm()
    {
        if (password.text != passwordConfirm.text)
        {
            UIManager.Instance.PopupWarn(true, "ºñ¹Ð¹øÈ£°¡ ÀÏÄ¡ÇÏÁö ¾Ê½À´Ï´Ù.");
            return false;
        }
        return true;
    }

    bool CheckName()
    {
        string val = userName.text;
        if (val == null)
        {
            UIManager.Instance.PopupWarn(true, "ÀÌ¸§À» ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        Regex kor = new Regex(@"^[°¡-ÆR]+$");
        if (val.Length < 2 || val.Length > 5 || !kor.IsMatch(val))
        {
            UIManager.Instance.PopupWarn(true, "2±ÛÀÚ ÀÌ»ó 5±ÛÀÚ ÀÌÇÏ\nÇÑ±Û¸¸ ÀÔ·Â °¡´ÉÇÕ´Ï´Ù.");
            return false;
        }
        return true;
    }

    bool CheckPhone()
    {
        if (phone.text == null)
        {
            UIManager.Instance.PopupWarn(true, "ÀüÈ­¹øÈ£¸¦ ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        return true;
    }
    bool CheckEmail()
    {
        if (email.text == null)
        {
            UIManager.Instance.PopupWarn(true, "ÀÌ¸ÞÀÏÀ» ÀÔ·ÂÇÏ¼¼¿ä.");
            return false;
        }
        return true;
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