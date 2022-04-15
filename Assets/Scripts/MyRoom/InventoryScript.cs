using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Slot[] slots;
    public Transform slotHolder;

    // Start is called before the first frame update
    void Start()
    {
        int artNum = 1; //������ �׸� ���� (���߿� �������� �޾ƿ� ��)
        slots = slotHolder.GetComponentsInChildren<Slot>();
        //slotCnt = imageFileInfo.Length;   // ���� ���� �ʱ�ȭ
        //inven.onSlotCountChange += SlotChange;
        SlotChange(artNum);         //������ �׸� ������ŭ �����ϰ� ���� ��Ȱ��ȭ
        for (int i = 0; i < artNum; i++)
        {
            StartCoroutine(DownloadImage(slots[i], "https://i.ibb.co/hyX44r9/flower.jpg"));
        }

    }
    IEnumerator DownloadImage(Slot slot, string MediaUrl)          //�������� �׸� �޾ƿͼ� art�� �ֱ�
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //���� �� texture�� �޾ƿ� �̹����� ����
            slot.transform.GetChild(0).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
    public int slotCnt;    // ���� ���� (= �� �κ��丮 �� ��ǰ ����)
    //public int SlotCnt
    //{
    //    get => slotCnt;
    //    set
    //    {
    //        slotCnt = value;
    //        onSlotCountChange.Invoke(slotCnt);
    //    }
    //}

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
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

}
