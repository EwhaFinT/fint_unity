
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class InventoryUI : MonoBehaviour
{
    
    public GameObject inventoryPanel;   // (Inventory 할당)
    bool activeInventory = false;       // 인벤토리 활성화 여부
    Inventory inven;                    // (Inventory.cs 객체 저장하는 변수)

    public Slot[] slots;                // (Inventory는 slot 저장소)
    public Transform slotHolder;        // (slotHolder는 slot들을 담고 있는 Scroll View-Viewport-Content)

    // (테스트용: 로컬 파일 정보)
    FileLoaderSystem fileLoaderSystem;
    private DirectoryInfo directory;    // 특정 디렉토리
    private FileInfo[] fileInfo;        // 특정 디렉토리의 하위 파일
    public FileInfo[] imageFileInfo;    // 특정 디렉토리의 하위 이미지 파일

    public GameObject registerForm;     // 작품 등록 폼
    bool registerButtonClick = false;   // 작품 등록 버튼 클릭 여부

    private void Start()
    {
        // -------- 슬롯 가져오기 시작 --------
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();

        // ==== 현재 로컬 파일 불러오는 것으로 구현한 상태 ====
        // ==== 추후 데이터베이스에서 사용자 정보 보내면 받아와 슬롯을 초기화 해야 함 ====
        // ==== 데이터베이스에서 사진을 받아올 때 myRoom 전시 여부를 받아와 전시된 작품은 인벤토리에 추가하지 않도록 해야 함 ====
        LoadLocalFile();    // 로컬 이미지 파일 불러오기

        inven.slotCnt = imageFileInfo.Length;   // 슬롯 개수 초기화

        inven.onSlotCountChange += SlotChange;
        // -------- 슬롯 가져오기 끝 --------

        inventoryPanel.SetActive(activeInventory);

        registerForm = GameObject.Find("RegisterForm");
        registerForm.SetActive(false);
    }

    private void SlotChange(int val)    // 슬롯 활성화 & 비활성화
    {
        // (기본 slot 개수 : 20개)
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))     // 슬롯 가시성 변환
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

        if (!registerForm.activeSelf && registerButtonClick)    // 작품 등록 성공시 슬롯 증가
        {
            ReadNewFileInfo();
            inven.SlotCnt++;
            registerButtonClick = false;
        }
    }

    public void RegisterImage()     // + 버튼 클릭시 작품 등록 폼 띄우기
    {
        registerForm.SetActive(true);
    }

    void LoadLocalFile() // (테스트용 : 로컬 파일 불러오기)
    {
        fileLoaderSystem = this.gameObject.AddComponent<FileLoaderSystem>();
        directory = new DirectoryInfo("C://DS/Unity/photo");
        fileInfo = directory.GetFiles("*");

        List<FileInfo> tmpList = new List<FileInfo>();
        Debug.Log("File Info " + fileInfo.Length);
        Debug.Log("File |" + fileInfo[0] + "| ");

        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileLoaderSystem.IsItArtwork(fileInfo[i]))
            {
                tmpList.Add(fileInfo[i]);
//                SetImage(i, fileInfo[i]);
            }
        }
        imageFileInfo = tmpList.ToArray();
        Debug.Log("Image File Info " + imageFileInfo.ToString());
    }

    private void SetImage(int i, FileInfo fileInfo)   // 이미지 파일 슬롯에 할당
    {
        Debug.Log("Slot" + slots + " length : " + slots.Length);
        fileLoaderSystem.LoadFile(
                    slots[i].transform.GetChild(0).GetComponent<Image>(),
                    fileInfo
                );
    }

    public void RegisterButtonClick()   // 작품 등록 버튼 클릭시
    {
        registerButtonClick = true;
    }

    public void CancelButtonClick()     // 취소 버튼 클릭시
    {
        registerButtonClick = false;
        registerForm.SetActive(false);
    }

    void ReadNewFileInfo()  // 새로 등록된 작품 정보 읽어오기
    {
        //Debug.Log(registerForm.transform.GetChild(4).name);
        //string pathString = registerForm.transform.GetChild(4).name.Replace("(Clone)", "");    // (name == 새로운 파일 경로)
        string pathString = "C:/DS/Unity/photo/The_Scream.jpg";
 //       Destroy(registerForm.transform.GetChild(4).gameObject);                             // (파일 경로만 얻으면 오브젝트 더이상 필요하지 않음)
        
        FileInfo fileInfo = new FileInfo(pathString);
        Debug.Log(fileInfo);
        List<FileInfo> tmpList = new List<FileInfo>(imageFileInfo);
        fileLoaderSystem.LoadFile(
                slots[inven.SlotCnt].transform.GetChild(0).GetComponent<Image>(),
                fileInfo
            );
        tmpList.Add(fileInfo);
        imageFileInfo = tmpList.ToArray();
     
    }

    
    // ==== UI : 작품 등록폼 종료 버튼 만들기 ====
    // ==== myRoom에 전시되어 있는 작품과 change하는 함수 필요 =====


}
