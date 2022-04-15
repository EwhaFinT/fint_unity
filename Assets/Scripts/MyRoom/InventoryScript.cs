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
        int artNum = 1; //보유한 그림 개수 (나중에 서버에서 받아올 것)
        slots = slotHolder.GetComponentsInChildren<Slot>();
        //slotCnt = imageFileInfo.Length;   // 슬롯 개수 초기화
        //inven.onSlotCountChange += SlotChange;
        SlotChange(artNum);         //보유한 그림 개수만큼 제외하고 슬롯 비활성화
        for (int i = 0; i < artNum; i++)
        {
            StartCoroutine(DownloadImage(slots[i], "https://i.ibb.co/hyX44r9/flower.jpg"));
        }

    }
    IEnumerator DownloadImage(Slot slot, string MediaUrl)          //서버에서 그림 받아와서 art에 넣기
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //액자 안 texture을 받아온 이미지로 변경
            slot.transform.GetChild(0).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
    public int slotCnt;    // 슬롯 개수 (= 내 인벤토리 속 작품 개수)
    //public int SlotCnt
    //{
    //    get => slotCnt;
    //    set
    //    {
    //        slotCnt = value;
    //        onSlotCountChange.Invoke(slotCnt);
    //    }
    //}

    private void SlotChange(int val)    // 슬롯 활성화 & 비활성화
    {
        // (기본 slot 개수 : 20개)
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
