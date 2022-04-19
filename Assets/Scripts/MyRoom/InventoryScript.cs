using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public Slot[] slots;
    public Transform slotHolder;
    public Button btn_close;

    public GameObject[] frame;
    public Texture art;

    // Start is called before the first frame update
    void Start()
    {
        int artNum = 1; //������ �׸� ���� (���߿� �������� �޾ƿ� ��)
        slots = slotHolder.GetComponentsInChildren<Slot>();
        //slotCnt = imageFileInfo.Length;   // ���� ���� �ʱ�ȭ
        //inven.onSlotCountChange += SlotChange;
        SlotChange(artNum);         //������ �׸� ������ŭ �����ϰ� ���� ��Ȱ��ȭ
        StartCoroutine(DownloadImage(slots[0], "https://i.ibb.co/hyX44r9/flower.jpg"));
        //for (int i = 0; i < artNum; i++)
        //{
        //    StartCoroutine(DownloadImage(slots[0], "https://i.ibb.co/hyX44r9/flower.jpg"));
        //}


        btn_close.onClick.AddListener(Onclicked_close);

    }
    IEnumerator DownloadImage(Slot slot, string MediaUrl)          //�������� �׸� �޾ƿͼ� art�� �ֱ�
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //�κ��丮 ������ �ش� �̹����� ����
            slot.transform.GetChild(0).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.Log("change inventory slot");
        }
    }
    public int slotCnt;    // ���� ���� (= �� �κ��丮 �� ��ǰ ����)

    private void SlotChange(int val)    // ���� Ȱ��ȭ & ��Ȱ��ȭ
    {
        // (�⺻ slot ���� : 20��)
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < val)
            {
                Color colorwhite = new Color(255, 255, 255, 255);
                slots[i].transform.GetChild(0).GetComponent<RawImage>().color = colorwhite;
                slots[i].GetComponent<Button>().interactable = true;
 //               slots[i].GetComponent<Button>().onClick.AddListener(OnClick_Slot);
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    //public void OnClick_Slot()
    //{
    //    frame = GameObject.FindGameObjectsWithTag("MyRoomFrame");      //myroom �� frame�� �迭
    //    art = slot.transform.GetChild(0).GetComponent<RawImage>().texture;
    //    frame[0].SetActive(true);
    //    Material[] mt = frame[0].GetComponent<Renderer>().materials;
    //    mt[1].SetTexture("_MainTex", art);
    //}
    public void Onclicked_close()
    {
        inventoryCanvas.SetActive(false);
    }

}
