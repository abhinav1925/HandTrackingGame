using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public Transform damage;
    public Transform dead;
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FireBall")
        {
            Debug.Log("Damaged");
            
            if(health > 0 )
            {
                StartCoroutine(ShowDamage());
                health = health - 10;
            }else
            {
                dead.gameObject.SetActive(true);
            }
           


        }
    }


    IEnumerator ShowDamage()
    {
        damage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        damage.gameObject.SetActive(false);
    }
}
