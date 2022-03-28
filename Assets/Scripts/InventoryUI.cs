
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class InventoryUI : MonoBehaviour
{
    
    public GameObject inventoryPanel;   // (Inventory �Ҵ�)
    bool activeInventory = false;       // �κ��丮 Ȱ��ȭ ����
    Inventory inven;                    // (Inventory.cs ��ü �����ϴ� ����)

    public Slot[] slots;                // (Inventory�� slot �����)
    public Transform slotHolder;        // (slotHolder�� slot���� ��� �ִ� Scroll View-Viewport-Content)

    // (�׽�Ʈ��: ���� ���� ����)
    FileLoaderSystem fileLoaderSystem;
    private DirectoryInfo directory;    // Ư�� ���丮
    private FileInfo[] fileInfo;        // Ư�� ���丮�� ���� ����
    public FileInfo[] imageFileInfo;    // Ư�� ���丮�� ���� �̹��� ����

    public GameObject registerForm;     // ��ǰ ��� ��
    bool registerButtonClick = false;   // ��ǰ ��� ��ư Ŭ�� ����

    private void Start()
    {
        // -------- ���� �������� ���� --------
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();

        // ==== ���� ���� ���� �ҷ����� ������ ������ ���� ====
        // ==== ���� �����ͺ��̽����� ����� ���� ������ �޾ƿ� ������ �ʱ�ȭ �ؾ� �� ====
        // ==== �����ͺ��̽����� ������ �޾ƿ� �� myRoom ���� ���θ� �޾ƿ� ���õ� ��ǰ�� �κ��丮�� �߰����� �ʵ��� �ؾ� �� ====
        LoadLocalFile();    // ���� �̹��� ���� �ҷ�����

        inven.slotCnt = imageFileInfo.Length;   // ���� ���� �ʱ�ȭ

        inven.onSlotCountChange += SlotChange;
        // -------- ���� �������� �� --------

        inventoryPanel.SetActive(activeInventory);

        registerForm = GameObject.Find("RegisterForm");
        registerForm.SetActive(false);
    }

    private void SlotChange(int val)    // ���� Ȱ��ȭ & ��Ȱ��ȭ
    {
        // (�⺻ slot ���� : 20��)
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
        if(Input.GetKeyDown(KeyCode.I))     // ���� ���ü� ��ȯ
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

        if (!registerForm.activeSelf && registerButtonClick)    // ��ǰ ��� ������ ���� ����
        {
            ReadNewFileInfo();
            inven.SlotCnt++;
            registerButtonClick = false;
        }
    }

    public void RegisterImage()     // + ��ư Ŭ���� ��ǰ ��� �� ����
    {
        registerForm.SetActive(true);
    }

    void LoadLocalFile() // (�׽�Ʈ�� : ���� ���� �ҷ�����)
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

    private void SetImage(int i, FileInfo fileInfo)   // �̹��� ���� ���Կ� �Ҵ�
    {
        Debug.Log("Slot" + slots + " length : " + slots.Length);
        fileLoaderSystem.LoadFile(
                    slots[i].transform.GetChild(0).GetComponent<Image>(),
                    fileInfo
                );
    }

    public void RegisterButtonClick()   // ��ǰ ��� ��ư Ŭ����
    {
        registerButtonClick = true;
    }

    public void CancelButtonClick()     // ��� ��ư Ŭ����
    {
        registerButtonClick = false;
        registerForm.SetActive(false);
    }

    void ReadNewFileInfo()  // ���� ��ϵ� ��ǰ ���� �о����
    {
        //Debug.Log(registerForm.transform.GetChild(4).name);
        //string pathString = registerForm.transform.GetChild(4).name.Replace("(Clone)", "");    // (name == ���ο� ���� ���)
        string pathString = "C:/DS/Unity/photo/The_Scream.jpg";
 //       Destroy(registerForm.transform.GetChild(4).gameObject);                             // (���� ��θ� ������ ������Ʈ ���̻� �ʿ����� ����)
        
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

    
    // ==== UI : ��ǰ ����� ���� ��ư ����� ====
    // ==== myRoom�� ���õǾ� �ִ� ��ǰ�� change�ϴ� �Լ� �ʿ� =====


}
