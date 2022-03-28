using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public class RegisterFormController : MonoBehaviour
{
    string path;
    public Text filePath;

    // 사용자 입력값
    public TMP_InputField title;           // 작품명
    public TMP_InputField author;          // 작가
    public TMP_InputField reservedPrice;   // 경매 시작가
    public Text auctionDate;               // 경매 일자
    public TMP_InputField description;     // 작품 설명


    public GameObject warningWindow;    // 경고창

    void Start()
    {
        warningWindow = GameObject.Find("WarningWindow");
        warningWindow.SetActive(false);
    }

    bool CheckValidation()  // 유효성 검사
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        if (path == null || path.Length < 4)
        {
            MakeWarningWindow("작품 파일을 선택하세요");
            return false;
        }
        if (title.text.Length < 2)
        {
            MakeWarningWindow("작품명은\n2글자 이상이어야 합니다.");
            return false;
        }
        if (author.text.Length < 2)
        {
            MakeWarningWindow("작가 이름은\n2글자 이상이어야 합니다.");
            return false;
        }
        if (reservedPrice.text.Length < 1)
        {
            MakeWarningWindow("경매 시작가를 입력하세요.");
            return false;
        }
        if (!Regex.IsMatch(auctionDate.text, @"^(20|21)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$"))
        {
            MakeWarningWindow("경매 일자를 선택하세요.");
            return false;
        }
        if (String.Compare(auctionDate.text, today) != 1)
        {
            MakeWarningWindow("경매 일자는\n익일 이후여야 합니다.");
            return false;
        }
        if(description.text.Length < 3)
        {
            MakeWarningWindow("작품 설명은\n3글자 이상이어야 합니다.");
            return false;

        }
        return true;
    }

    void RegisterButtonClick()   // 작품 등록 버튼 클릭시
    {
        if (CheckValidation())   // 유효성 검사 통과
        {
            SendNewFileInfo();

            // ==== 서버와 통신하는 부분 필요 ====
            // ==== NFT API 사용해서 NFT 주소도 만들어야 함 ====

            // 작품 성공적으로 등록시 작품 등록 폼 닫음
            InitializeForm();
            gameObject.SetActive(false);
        }
    }

    void OpenExplorerButtonClick()   // 파일 탐색기 버튼 클릭시
    {
        // (pc 기준으로 구현한 것, 모바일로 할 경우 변경해야 할 듯)
        path = EditorUtility.OpenFilePanel("내 작품 등록하기", "", "jpg");
        GetImage();
    }

    void GetImage()     // 작품 선택시 버튼 text 변경하기
    {
        if(path != null)
        {
            filePath.text = path;
        }
    }

    void InitializeForm()   // 작품 등록 폼 초기화
    {
        path = null;
        filePath.text = "파일 탐색";
        ClearInputField(title);
        ClearInputField(author);
        ClearInputField(description);
        ClearInputField(reservedPrice);
        auctionDate.text = "경매 일자 선택";
    }

    void ClearInputField(TMP_InputField inputField)
    {
        inputField.text = "";
    }

    void MakeWarningWindow(string msg)  // 경고창 만들기
    {
        TMP_Text message = warningWindow.transform.GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
        warningWindow.SetActive(true);
    }

    public void WarningWindowCloseButtonClick()     // 경고창 닫기
    {
        warningWindow.SetActive(false);
    }

    void SendNewFileInfo()
    {
        GameObject emptyGameObject = GameObject.Instantiate(
                new GameObject(filePath.text),
                this.transform.position,
                Quaternion.identity
                );
        emptyGameObject.transform.SetParent(this.transform);
    }
}