using UnityEngine;

public class DeadController : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject deadPanel;

    private void Start()
    {
        Time.timeScale = 1f;
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Time.timeScale = 0f;
            deadPanel.SetActive(true);
            
        }
    }
}
