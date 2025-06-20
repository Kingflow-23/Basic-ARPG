using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    public float followSpeed = 0.125f;
    public float zoomSpeed = 3f;
    private float zoom = 12f;
    public float maxZoom = 25f;
    public float minZoom = 10f;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }

        zoom = (maxZoom + minZoom) / 2f;
        if (target != null)
        {
            transform.position = target.position + (zoom * offset);
        }
    }

    void LateUpdate()
    {
        if (target == null) return; // In case the target is destroyed

        zoom = Mathf.Clamp(-Input.GetAxis("Mouse ScrollWheel") * zoomSpeed + zoom, minZoom, maxZoom);
        Vector3 targetPos = target.position + (zoom * offset);
        Vector3 lerpPos = Vector3.Lerp(transform.position, targetPos, followSpeed);

        transform.position = lerpPos;
        transform.LookAt(target);
    }
}