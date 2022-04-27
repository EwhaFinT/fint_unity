using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtPanel : MonoBehaviour
{
    public Button btn_close, btn_auction;
    public GameObject artPanel;
    public RawImage thisImg;
    public Text artInfo;

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

    public void changeArtInfo()
    {
        artInfo.text = 
            "��ǰ�� : " + "�Ϳ��� �����\n" +
            "�۰� : " + "321codus\n" +
            "��ǰ �Ұ� : " + "�Ϳ��� ����̰� ������ ���մϴ�. ���󿡼� ���� ���� ������ ���Կ� ��....\n" +
            "����������\n" +
            "�� �� ������\n" +
            "���� | �� |\n" +
            "���� | �� |\n" +
            "���� | �� \\\n" +
            "������ �� \\\n" +
            "�� /�� ����?������ �� _\n" +
            "�� |�������� Y����������_ ��\n" +
            "�� |����������������|��\n" +
            "�� ���������������� }\n" +
            "��/����������������/\n" +
            " /";
    }


}

//void Img()
//{
//    GameObject[] frame;
//    frame = GameObject.FindGameObjectsWithTag("GalleryFrame");      //gallery�� frame�� �迭
//    var x = thisImg.GetComponent<RawImage>();
//    // ���� ���콺 ��ư�� Ŭ��������
//    if (Input.GetMouseButtonDown(0))  //0�̸� ��Ŭ��, 1�̸� ��Ŭ��, 2�̸� �߾��� Ŭ��
//    {

//        target = GetClickedObject();
//        for (int i = 0; i < frame.Length; i++)
//        {
//            if (target.Equals(frame[i]))
//            {
//                Material[] mt = frame[i].GetComponent<Renderer>().materials;
//                x.texture = mt[1].GetTexture("_MainTex");
//            }

//        }

//    }

//}

//private GameObject GetClickedObject()
//{
//    RaycastHit hit;
//    GameObject target = null;
//    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ����Ʈ ��ó ��ǥ�� �����. 

//    if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //���콺 ��ó�� ������Ʈ�� �ִ��� Ȯ��
//    {
//        //������ ������Ʈ�� �����Ѵ�.
//        target = hit.collider.gameObject;

//    }
//    return target;
//}
