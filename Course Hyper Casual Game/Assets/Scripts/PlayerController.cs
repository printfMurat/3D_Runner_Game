using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class PlayerController : MonoBehaviour
{
    //[Header("Physic Settings")]
    [HideInInspector]
    public Rigidbody rb; // Rigidbody bileþeni
    private float forwardSpeed = 700f; // Ýleri hýz
    private float lateralSpeed = 300f; // Yan hýz
    private float maxLateralRange; // Maksimum yan hareket mesafesi
    [Header("Magnet Settings")]
    public bool isMagnetOpen; // Mýknatýs açýk mý?
    private Vector3 magnetOverlapValues = new Vector3(6, 1, 1);
    private List<GameObject> collectionCoins = new List<GameObject>();
    private float magnetPower = 50f;
    public LayerMask coinLayer;
    public ParticleSystem ringParticle;

    public GameObject finishPanel; // Bitiþ paneli
    public GameObject platform; // Platform nesnesi

    public Animator anim;


    private bool isFinish = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini al
        CalculateTransformBounds(); // Platform geniþliðini hesapla
        anim = GetComponentInChildren<Animator>();

    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * Time.fixedDeltaTime * forwardSpeed); // Ýleri hareket
        float horizontalMove = Input.GetAxis("Horizontal"); // Yatay hareket giriþi
        rb.AddForce(Vector3.right * horizontalMove * lateralSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange); // Yan hareket
        RestrictLateralMovement(); // Yan hareketi kýsýtla
    }

    private void CalculateTransformBounds()
    {
        if (platform != null)
        {
            float platformWidth = platform.GetComponent<Renderer>().bounds.size.x; // Platform geniþliðini al
            maxLateralRange = (platformWidth / 2f) + 0.1f; // Maksimum yan hareket mesafesini hesapla
        }
        else
        {
            Debug.LogError("Platform nesnesi atanmadý!"); // Hata mesajý
        }
    }

    private void RestrictLateralMovement()
    {
        Vector3 position = rb.position; // Mevcut pozisyonu al
        position.x = Mathf.Clamp(position.x, -maxLateralRange, maxLateralRange); // Yan hareketi kýsýtla
        rb.position = position; // Pozisyonu güncelle

        Vector3 velocity = rb.linearVelocity; // Yeni kullaným: linearVelocity
        velocity.x = 0f; // Yan hýzý sýfýrla
        rb.linearVelocity = velocity; // Hýzý güncelle              
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
            StartCoroutine("MagnetTime");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            FinishMethod();

        }
    }
    private IEnumerator MagnetTime()
    {
        isMagnetOpen = true;
        yield return new WaitForSeconds(5f);
        isMagnetOpen = false;

    }
    private void Update()
    {
        if (isMagnetOpen)
        {
            // Mýknatýs menzili içindeki tüm altýnlarý bul
            Collider[] magnetGoldColliders = Physics.OverlapBox(
                gameObject.transform.position,
                magnetOverlapValues * 0.5f,
                Quaternion.identity,
                coinLayer);

            // Yeni altýnlarý toplama listesine ekle
            foreach (var coinCollider in magnetGoldColliders)
            {
                if (coinCollider.GetComponent<GoldController>() != null &&
                    coinCollider.GetComponent<Rigidbody>() != null &&
                    !collectionCoins.Contains(coinCollider.gameObject))
                {
                    collectionCoins.Add(coinCollider.gameObject);
                }
            }

            // Koleksiyon listesindeki tüm altýnlara çekim kuvveti uygula
            for (int i = collectionCoins.Count - 1; i >= 0; i--)
            {
                // Altýn artýk yoksa atla
                if (collectionCoins[i] == null)
                {
                    collectionCoins.RemoveAt(i);
                    continue;
                }

                // Oyuncuya doðru yönü hesapla
                Vector3 direction = (gameObject.transform.position - collectionCoins[i].transform.position).normalized;

                // Altýný oyuncuya doðru hareket ettirmek için kuvvet uygula
                Rigidbody coinRb = collectionCoins[i].GetComponent<Rigidbody>();

                // Y ekseninin kilitli olduðunu dikkate alarak, sadece X ve Z ekseninde hýz uygula
                Vector3 horizontalVelocity = new Vector3(direction.x, 0, direction.z).normalized * magnetPower;
                coinRb.linearVelocity = horizontalVelocity;

                // Ýsteðe baðlý: Görsel efekt için X ve Z eksenlerinde rotasyon ekle
                coinRb.angularVelocity = new Vector3(5f, 0, 5f);
            }
        }
        if (isMagnetOpen && !ringParticle.isPlaying)
        {
            ringParticle.Play();
        }
        else if (!isMagnetOpen && ringParticle.isPlaying)
        {
            ringParticle.Stop();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, magnetOverlapValues);
    }
    private void FinishMethod()
    {

        StartCoroutine(FinishAnimation());

    }
    public IEnumerator FinishAnimation()
    {
        rb.isKinematic = true;
        anim.SetBool("isFinish", true);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0;
        finishPanel.SetActive(true);
    }

}
