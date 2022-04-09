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
        // ==== ���̵� �ߺ� �˻� =====
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
            // ==== �鿣��� ��� ====
            // ==== ex. ȸ������ �Ϸ�� �α��� �������� ������ ====
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
            UIManager.Instance.PopupWarn(true, "���̵� �Է��ϼ���.");
            return false;
        }
        if(isDupId)
        {
            UIManager.Instance.PopupWarn(true, "���̵� �ߺ� Ȯ���� �������ּ���.");
            return false;
        }
        return true;
    }

    bool CheckPassword()
    {
        string val = password.text;
        if (val == null)
        {
            UIManager.Instance.PopupWarn(true, "��й�ȣ�� �Է��ϼ���.");
            return false;
        }
        Regex eng = new Regex(@"[a-zA-Z]");
        Regex num = new Regex(@"[0-9]");
        if (val.Length < 8 || !eng.IsMatch(val) || !num.IsMatch(val))
        {
            UIManager.Instance.PopupWarn(true, "����, ���ڸ� �ϳ� �̻� ������\n8�ڸ� ��й�ȣ�� �Է��ϼ���.");
            return false;
        }
        return true;
    }

    bool CheckPasswordConfirm()
    {
        if (password.text != passwordConfirm.text)
        {
            UIManager.Instance.PopupWarn(true, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return false;
        }
        return true;
    }

    bool CheckName()
    {
        string val = userName.text;
        if (val == null)
        {
            UIManager.Instance.PopupWarn(true, "�̸��� �Է��ϼ���.");
            return false;
        }
        Regex kor = new Regex(@"^[��-�R]+$");
        if (val.Length < 2 || val.Length > 5 || !kor.IsMatch(val))
        {
            UIManager.Instance.PopupWarn(true, "2���� �̻� 5���� ����\n�ѱ۸� �Է� �����մϴ�.");
            return false;
        }
        return true;
    }

    bool CheckPhone()
    {
        if (phone.text == null)
        {
            UIManager.Instance.PopupWarn(true, "��ȭ��ȣ�� �Է��ϼ���.");
            return false;
        }
        return true;
    }
    bool CheckEmail()
    {
        if (email.text == null)
        {
            UIManager.Instance.PopupWarn(true, "�̸����� �Է��ϼ���.");
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