using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //draw Tracks
    public Shader drawShader;
    private Material sandMaterial, drawMaterial;
    private RenderTexture splatmap;
    [Range(0, 5)]
    public float _brushSize = 5.0f;
    [Range(0, 1)]
    public float _brushStrength = 1;

    public GameObject _terrain;
    private RaycastHit _hit;
    int _layerMask;

    //Game Logic
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    public Animator animator;
    float y = 0;
    public float speed = 10f;
    bool isJump;
    bool isRun;
    public bool kill=false;
    public bool isDead=false;
    public bool isFinish = false;
    int RandNum = 0;
    float moveX;
    float moveZ;
    Vector3 moveVector;
    Rigidbody rigidbody;
    timerScript _timerScript;

    void Awake()
    {
        _timerScript = GameObject.Find("left_time").GetComponent<timerScript>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        particle.Pause();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Make Splatmap");
        //Tracks
        _layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);

        //검정배경 생성
        sandMaterial = _terrain.GetComponent<MeshRenderer>().material;
        splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        sandMaterial.SetTexture("_Splat", splatmap);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_timerScript.isStarted);
        if (_timerScript.isStarted == true)
        {
            //Tracks
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
            {
                if (Physics.Raycast(transform.position, Vector3.down, out _hit, Mathf.Infinity, _layerMask))
                {
                    Debug.Log("Enter Plane");

                    drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                    drawMaterial.SetFloat("_Strength", _brushStrength);
                    drawMaterial.SetFloat("_Size", _brushSize);

                    RenderTexture temp = RenderTexture.GetTemporary(splatmap.width, splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                    Graphics.Blit(splatmap, temp);
                    Graphics.Blit(temp, splatmap, drawMaterial);
                    RenderTexture.ReleaseTemporary(temp);
                }
            }

            if (isDead == false) animator.SetBool("Dead", false);
            if (isFinish == true) transform.Rotate(new Vector3(0, 100f * Time.deltaTime, 0));

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetButtonDown("Jump"))
            {
                if (kill == true) death();
            }
            GetInput();
            Run();
            Turn();
            Jump();



        }
    }       
        void GetInput()
        {
        
           moveX = Input.GetAxisRaw("Horizontal");
           moveZ = Input.GetAxisRaw("Vertical");
        if (moveX != 0f || moveZ != 0)
        {
            //if (isDead == true) death();
        }
        }
        void Run()
        {
        if (isDead != true&&isFinish!=true)
        {
            moveVector = new Vector3(moveX, 0f, moveZ);
            isRun = moveVector.magnitude > 0;
            transform.position += moveVector * speed * Time.deltaTime;
                animator.SetBool("isRun", moveVector != Vector3.zero);
        }
            
        }
        void Turn()
        {
            //transform.LookAt(transform.position + moveVector);
            if (!(moveX == 0 && moveZ == 0)&&!isDead&&!isFinish)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), Time.deltaTime * 20f);

            }
        }
        void Jump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!isJump&&!isDead)
                {

                    isJump = true;
                    animator.SetBool("isJump", true);
                    animator.SetTrigger("doJump");
                    rigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
                    
                }
                else
                {
                    return;
                }
                
            }
        }
       void OnCollisionEnter(Collision collision)
        {

        if (collision.gameObject.tag == "Block")
        {
            Debug.Log("Block");
        }

        if (collision.gameObject.tag=="Cube")
            {
                animator.SetBool("isJump", false);
                isJump = false;
            }

        if (collision.gameObject.tag == "Finish")
        {
            animator.SetBool("isFinish", true); 
            isFinish = true;
            camera camera_s = GameObject.Find("Main Camera").GetComponent<camera>();
            //camera_s.came.position = transform.position + new Vector3(0f, 15f, -40f);
            camera_s.came.position = transform.position + new Vector3(0f, 15f, -15f);
            camera_s.came.LookAt(transform);

        }

        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("Item");
            Destroy(collision.gameObject);
            ParticleSystem.MainModule psmain = particle.main;
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, collision.contacts[0].normal);
            Vector3 vec3 = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y + 5, collision.contacts[0].point.z);
            ParticleSystem spark = Instantiate(particle, vec3, rot);
            spark.transform.SetParent(this.transform);

            spark.Play();
            Destroy(spark, 1.0f);

            RandNum = Random.Range(0, 2);
            StartCoroutine(Example());
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
        }
            
    }

    void death()
    {
        if (kill == true&&isFinish==false)
        {
           
            animator.SetTrigger("isDead");
            isDead = true;
            animator.SetBool("Dead",true);
            camera camera_s = GameObject.Find("Main Camera").GetComponent<camera>();
            camera_s.came.position = transform.position + new Vector3(0f, 15f, -15f);
            camera_s.came.LookAt(transform);
        }
    }
    
    IEnumerator Example()
    {

        if (RandNum == 0) speed = 30f;
        else if (RandNum == 1) speed = 3f;

        yield return new WaitForSeconds(3);

        speed = 10f;
        

    }

}
