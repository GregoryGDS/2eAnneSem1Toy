using UnityEngine;

public class testCam : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        // Assuming you have a reference to your main camera
        mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found in the scene.");
            return;
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Get the position of the screen center in viewport space (0.5, 0.5)
            Vector3 screenCenterViewport = new Vector3(0.5f, 0.5f, mainCamera.nearClipPlane);

            // Convert the screen center from viewport space to world space
            Vector3 screenCenterWorld = mainCamera.ViewportToWorldPoint(screenCenterViewport);

            // Now 'screenCenterWorld' contains the world space position of the screen center
            //Debug.DrawRay(transform.position, screenCenterWorld, Color.blue);
        }
    }
}