using System.Collections;
using UnityEngine;

public class DeadController : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject deadPanel;
    public bool isDead = false; 

    private void Start()
    {
        Time.timeScale = 1f;
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {           
           
            StartCoroutine(Dead());

        }
    }
    private IEnumerator Dead()      
    {
        isDead = true;
        playerController.rb.isKinematic = true;
        playerController.anim.SetBool("isDead", true);  
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        deadPanel.SetActive(true);
    }
}
