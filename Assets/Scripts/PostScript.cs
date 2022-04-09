using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostScript : MonoBehaviour
{   
    public GameObject post;
    public GameObject board;
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        post.SetActive(false);
        exit.onClick.AddListener(onClicked_exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        post.SetActive(true);
    }
    void onClicked_exit()
    {
        post.SetActive(false);
        board.SetActive(true);
    }
}
