using TMPro;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    private GoldManager goldManager; // GoldController referansý

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponent<GoldManager>() != null)
            {
                goldManager = other.GetComponent<GoldManager>();
            }
            goldManager.AddGold(1); // GoldController üzerinden altýný ekle
            Destroy(gameObject); // Altýný yok et
        }
    }
}
