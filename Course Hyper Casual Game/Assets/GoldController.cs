using TMPro;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    private GoldManager playerGoldManager; // GoldController referansý
    private PlayerController playerController;
    private AudioSource coinVoice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (other.GetComponent<GoldManager>() != null && other.GetComponent<PlayerController>() != null)
            {
                playerController = other.GetComponent<PlayerController>();
                playerGoldManager = other.GetComponent<GoldManager>();
                coinVoice = playerController.GetComponent<AudioSource>();
                if (!other.GetComponent<ParticleSystem>().isPlaying)
                {
                    playerController.GetComponent<ParticleSystem>().Play();
                }
                if(!coinVoice.isPlaying)
                {
                    coinVoice.Play();
                }
                   
                
                playerGoldManager.AddGold(1);// Oyuncuya altýn ekle
                Destroy(gameObject); // Altýný yok et


            }
            else
            {
                Debug.Log("Player objesinden gelen gold managerin ve particle istemin boþ olup olmadýðýný tekrar kontrol edin ");
            }
        }
    }
}
