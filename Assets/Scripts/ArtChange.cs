using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtChange : MonoBehaviour
{
    public GameObject art;
    private Renderer _renderArt;
    public Texture2D texArtNew;
    // Start is called before the first frame update
    void Start()
    {
        _renderArt = art.GetComponent<Renderer>();
        Debug.Log("change tex");
        // Change Art Mat
        _renderArt.material.mainTexture = texArtNew;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetArt()
    {
        //�������� �׸� �޾ƿͼ� artList �迭�� �ִ� �ڵ� �ʿ���
    }
}
