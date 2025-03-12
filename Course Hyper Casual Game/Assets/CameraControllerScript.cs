using TMPro;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public Transform target;
    public Vector3 rotationCamere = new Vector3();
    private Vector3 offset;

    void Start()
    {
       offset = transform.position - target.position;
    }


    private void LateUpdate()
    {
        Vector3 newPostion = new Vector3(gameObject.transform.position.z, gameObject.transform.position.y, offset.z + target.position.z);
       transform.position = newPostion;
    }
}
