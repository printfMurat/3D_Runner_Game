    using TMPro;
    using UnityEngine;

    public class GoldController : MonoBehaviour
    {
        private GoldManager playerGoldManager; // GoldController referansý

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<GoldManager>() != null)
            {
                playerGoldManager = other.GetComponent<GoldManager>();
                playerGoldManager.AddGold(1); // Oyuncuya altýn ekle
                Destroy(gameObject); // Altýný yok et
            }
        }
    }
}
