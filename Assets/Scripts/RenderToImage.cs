using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderToImage : MonoBehaviour
{
    public RenderTexture rt;
    public Camera virtuCamera;

    private string exportPath;
    private string fakeFileName;

    void Awake()
    {
        exportPath = Application.dataPath + "/Images/";
        fakeFileName = "RenderedKubik.png";
    }

    public void ExportScreenToJPG()
    {
        Texture2D tex = toTexture2D(rt);
        byte[] _bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(exportPath + fakeFileName, _bytes);
        Debug.Log($"Saved to {fakeFileName} at {exportPath}!");
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
