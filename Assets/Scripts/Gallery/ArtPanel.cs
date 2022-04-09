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
        Img();

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

    void Img()
    {

        var x = thisImg.GetComponent<RawImage>();
        var artManager = Manager.Instance.gallaryPrefab.GetComponent<ArtChange>();
        frame_center = GameObject.Find("Frame_Center");
        frame_left = GameObject.Find("Frame_Left");
        frame_right = GameObject.Find("Frame_Right");

        // ���� ���콺 ��ư�� Ŭ��������
        if (Input.GetMouseButtonDown(0))  //0�̸� ��Ŭ��, 1�̸� ��Ŭ��, 2�̸� �߾��� Ŭ��
        {
            target = GetClickedObject();
            if (target.Equals(frame_center))
            {
                x.texture = artManager.art1;
            }
            else if (target.Equals(frame_left))
            {
                x.texture = artManager.art2;
            }
            else if (target.Equals(frame_right))
            {
                x.texture = artManager.art3;
            }

        }
        
    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ����Ʈ ��ó ��ǥ�� �����. 

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //���콺 ��ó�� ������Ʈ�� �ִ��� Ȯ��
        {
            //������ ������Ʈ�� �����Ѵ�.
            target = hit.collider.gameObject;

        }
        return target;
    }

}
