using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileLoaderSystem : MonoBehaviour
{
    private ImageLoader imageLoader;

    private void Awake()
    {
        imageLoader = GetComponent<ImageLoader>();
    }

    //public void LoadFile(Image imageDrawTexture, FileInfo file)
    //{
    //    imageLoader.OnLoad(imageDrawTexture, file);
    //}

    public bool IsItArtwork(FileInfo file)
    {
        if (file.FullName.Contains(".jpg") || file.FullName.Contains(".png") || file.FullName.Contains(".jpeg"))
            return true;
        else return false;
    }
}