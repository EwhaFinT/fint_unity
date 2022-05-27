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
    int artNum;     //count of my paint

    // Start is called before the first frame update
    void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
        //Button inventoryButton = UIManager.Instance.inventoryButton;
        //inventoryButton.onClick.AddListener(OnInventory);
        //slotCnt = imageFileInfo.Length;   // 슬롯 개수 초기화
        //inven.onSlotCountChange += SlotChange;
        //StartCoroutine(DownloadImage(slots[0], "https://i.ibb.co/hyX44r9/flower.jpg"));
        //for (int i = 0; i < artNum; i++)
        //    StartCoroutine(DownloadImage(slots[0], "https://i.ibb.co/hyX44r9/flower.jpg"));
        
        btn_close.onClick.AddListener(Onclicked_close);

    }

    IEnumerator GetPaint()
    {
        //start api
        string url = Manager.Instance.url + "v1/mypage?userId=" + Manager.Instance.ID;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<PaintResponse>(jsonString);
        Debug.Log(response);
        // api end
        artNum = response.paint.Count;
        SlotChange(artNum);         //slot disabled without artNum;

        for (int i = 0; i < response.paint.Count; i++)
            StartCoroutine(DownloadImage(slots[i], response.paint[i]));

    }

    public void OnInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        StartCoroutine(GetPaint());
    }

    IEnumerator DownloadImage(Slot slot, string MediaUrl)          //서버에서 그림 받아와서 art에 넣기
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //인벤토리 슬롯을 해당 이미지로 변경
            slot.transform.GetChild(0).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.Log("change inventory slot");
        }
    }
    public int slotCnt;    // 슬롯 개수 (= 내 인벤토리 속 작품 개수)

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

    public void Onclicked_close()
    {
        inventoryCanvas.SetActive(false);
    }

}

[System.Serializable]

class PaintListResponse
{
    public string paintUrl;
}
class PaintResponse
{
    public List<string> paint;
}