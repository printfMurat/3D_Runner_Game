using UnityEngine;

/// <summary>
/// Kamera, oyuncuyu yumuþak bir þekilde takip eder.
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform targetObject; // Oyuncunun Transform bileþeni
    public Vector3 cameraStartPosition = new Vector3(0f, 4.15f, -20.75f);
    public Quaternion cameraStartRotation = Quaternion.Euler(26.05f, 4.243f, 0f);
    private Vector3 deadCondition = new Vector3(0, 0,-2f); // Ölüm durumunda kamera geri çekilme miktarý
    private Vector3 offset;
    public DeadController deadController;
            

    private Vector3 currentVelocity = Vector3.zero; // SmoothDamp için hýz referansý
    private float smoothTime = 1.35f; // Yumuþak geçiþ süresi

    private void Start()
    {
        cameraStartPosition = transform.position;
        cameraStartRotation = transform.rotation;
        offset = cameraStartPosition - targetObject.position; // Kamera ile oyuncu arasýndaki mesafe
        Debug.Log("Offset: " + offset);
    }

    private void FixedUpdate()
    {
        if (targetObject != null)
        {
            // Hedef kamera konumu
            Vector3 targetCameraPosition = targetObject.position + offset;

            if (deadController != null && deadController.isDead)
            {
                Debug.Log("Camera ilk:" + transform.position);
                Vector3 deadCameraPosition = targetCameraPosition + deadCondition; 
                transform.position = Vector3.SmoothDamp(transform.position, deadCameraPosition, ref currentVelocity, smoothTime);
                Debug.Log("Camera son:" + transform.position);
            }
            else
            {
                // Normal durumda kamerayý hedef pozisyona yumuþak bir þekilde hareket ettir
                transform.position = Vector3.Lerp(transform.position, targetCameraPosition, Time.deltaTime * 20f);
            }
        }
        
}
}
