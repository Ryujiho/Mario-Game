using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform came;
    public Transform target;
    player play;
    Animator animator;
    bool isDead;
    public Vector3 offset;

    public float DelayTime=3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player player_s = GameObject.Find("Player").GetComponent<player>();
        if (player_s.isDead == false && player_s.isFinish == false)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }


    }
}
