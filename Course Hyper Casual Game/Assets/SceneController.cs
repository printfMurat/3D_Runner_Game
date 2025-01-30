using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
