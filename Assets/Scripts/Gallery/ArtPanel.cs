using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtPanel : MonoBehaviour
{
    public Button btn_close, btn_auction;
    public GameObject artPanel;
    public RawImage thisImg;
    public GameObject target;
    private GameObject frame_center, frame_left, frame_right;

    // Start is called before the first frame update
    void Start()
    {
        btn_close.onClick.AddListener(Onclicked_close);
        btn_auction.onClick.AddListener(Onclicked_auction);
//        artPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void panelStart()
    {
        Debug.Log("show art panel");
        artPanel.SetActive(true);
 //        var auctionPanel = GameObject.Find("Canvas_Auction").GetComponent<AuctionPanel>();
        
        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelClose();

    }
    public void Onclicked_close()
    {
        artPanel.SetActive(false);
    }

    public void Onclicked_auction()
    {
        artPanel.SetActive(false);
        var auctionPanel = UIManager.Instance.popupAuction.GetComponent<AuctionPanel>();
        auctionPanel.panelStart();
    }

    public void changeImg(GameObject frame)
    {
        var x = thisImg.GetComponent<RawImage>();
        Material[] mt = frame.GetComponent<Renderer>().materials;
        x.texture = mt[1].GetTexture("_MainTex");
    }

    //void Img()
    //{
    //    GameObject[] frame;
    //    frame = GameObject.FindGameObjectsWithTag("GalleryFrame");      //gallery내 frame의 배열
    //    var x = thisImg.GetComponent<RawImage>();
    //    // 왼쪽 마우스 버튼을 클릭했을때
    //    if (Input.GetMouseButtonDown(0))  //0이면 좌클릭, 1이면 우클릭, 2이면 중앙을 클릭
    //    {
            
    //        target = GetClickedObject();
    //        for(int i=0; i<frame.Length; i++)
    //        {
    //            if (target.Equals(frame[i]))
    //            {
    //                Material[] mt = frame[i].GetComponent<Renderer>().materials;
    //                x.texture = mt[1].GetTexture("_MainTex");
    //            }
                    
    //        }

    //    }
        
    //}

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;

        }
        return target;
    }

}
