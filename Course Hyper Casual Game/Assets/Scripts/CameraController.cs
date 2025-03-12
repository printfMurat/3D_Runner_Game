using UnityEngine;

/// <summary>
/// Kamera, oyuncuyu yumuþak bir þekilde takip eder.
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform targetObject; // Oyuncunun Transform bileþeni
    public Vector3 cameraStartPosition = new Vector3(0f, 4.15f, -20.75f);
    public Quaternion cameraStartRotation = Quaternion.Euler(26.05f, 4.243f, 0f);
    private Vector3 offset;

    private void Start()
    {
        cameraStartPosition = transform.position;
        cameraStartRotation = transform.rotation;
        offset = cameraStartPosition - targetObject.position; // Kamera ile oyuncu arasýndaki mesafe
        Debug.Log("Offset: " + offset);
    }

    private void LateUpdate()
    {
        if (targetObject != null)
        {
            // Hedef kamera konumu
            Vector3 targetCameraPosition = targetObject.position + offset;

            // Yumuþak geçiþ
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition, Time.deltaTime * 20f);

            Debug.Log($"Time.deltaTime: {Time.deltaTime * 5f}");


        }
    }
}
