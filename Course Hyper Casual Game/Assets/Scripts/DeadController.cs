using UnityEngine;

public class DeadController : MonoBehaviour
{
    public GameObject DeadPanel;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0f;
            DeadPanel.SetActive(true);

        }
    }
}


