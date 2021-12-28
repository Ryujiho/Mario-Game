using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoardPatternTexture : MonoBehaviour
{
    public Texture2D mainTexture;

    public int mainTexWidth;
    public int mainTexHeight;
    // Start is called before the first frame update
    void Start()
    {
        SetMainTextureSize();
        CreatePattern();
    }
    void CreatePattern()
    {
        for (int i = 0; i < mainTexWidth; i++)
        {
            for (int j = 0; j < mainTexWidth; j++)
            {
                if (((i + j) % 2) == 1)
                {
                    mainTexture.SetPixel(i, j, new Color(0.07f, 0.57f,0.07f));
                }
                else
                {
                    mainTexture.SetPixel(i, j, new Color(20.0f, 146.0f, 20.0f));
                }
            }
        }
        mainTexture.Apply(); 
        GetComponent <Renderer>().material.mainTexture = mainTexture;
        mainTexture.wrapMode = TextureWrapMode.Clamp;
        mainTexture.filterMode = FilterMode.Point;
    }

    void SetMainTextureSize()
    {
        mainTexture = new Texture2D(mainTexWidth, mainTexHeight);
    }

}
