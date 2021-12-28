using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public Shader drawShader;
    private Material sandMaterial, drawMaterial;
    private RenderTexture splatmap;
    [Range(0, 5)]
    public float _brushSize = 4.5f;
    [Range(0, 1)]
    public float _brushStrength = 1;

    public GameObject _terrain;
    private RaycastHit _hit;
    int _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);

        //검정배경 생성
        sandMaterial = _terrain.GetComponent<MeshRenderer>().material;
        splatmap = new RenderTexture(1024, 2048, 0, RenderTextureFormat.ARGBFloat);
        sandMaterial.SetTexture("_Splat", splatmap);
    }

    // Update is called once per frame
    void Update()
    {
        // (Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
            if (Input.GetKey(KeyCode.Mouse0))//방향키
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _hit, Mathf.Infinity, _layerMask))
            {
                transform.position += new Vector3(0.5f, 0, 0);//플레이어위치

                drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength", _brushStrength);
                drawMaterial.SetFloat("_Size", _brushSize);

                RenderTexture temp = RenderTexture.GetTemporary(splatmap.width, splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatmap, temp);
                Graphics.Blit(temp, splatmap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 512, 512), splatmap, ScaleMode.ScaleToFit, false, 1);
    }
}
