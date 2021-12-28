using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    player Players;
    int RandNum;
    // Update is called once per frame
    void Start()
    {
        
        
    }
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
        Players = other.GetComponent<player>();
        RandNum = Random.Range(0, 2);
        if (other.name=="Player")
        {

          
            gameObject.SetActive(false);

        }
    }
   
}
