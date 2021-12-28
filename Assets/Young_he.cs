    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Young_he : MonoBehaviour
{
    private float time = 0f;
    public Animator animator;
    // 오디오 소스 생성해서 추가
    public AudioClip soundToPlay;
    public AudioSource source;
    timerScript _timerScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Example());
        source = GetComponent<AudioSource>();
        source.Stop();
    }
    IEnumerator Example()
    {
        //animator.SetBool("test2", true);
        yield return new WaitForSeconds(5);
        while (true)
        {
            animator.SetTrigger("test");
            source.PlayOneShot(soundToPlay);
            yield return new WaitForSeconds(11);
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*this.time += Time.deltaTime;
        if (this.time > 5f)
        {
            animator.SetTrigger("test");
            this.time = 0;
        }*/
    }

   void death()
    {
        player player_s = GameObject.Find("Player").GetComponent<player>();
   
        player_s.kill = true;
       
    }
    
    void live()
    {
        player player_s = GameObject.Find("Player").GetComponent<player>();
       
        player_s.kill= false;
    }
}
