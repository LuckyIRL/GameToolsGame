using UnityEngine.SceneManagement;
using UnityEngine;

public class FloatingOrigin : MonoBehaviour
{
    public float threshold = 1000.0f;
    public float transitionSpeed = 1.0f; // Adjust this value to control the speed of the transition
    public LevelLayoutGenerator levelLayoutGenerator;

    private Vector3 targetPosition;

    void LateUpdate()
    {
        Vector3 cameraPosition = gameObject.transform.position;
        cameraPosition.y = 0f;

        if (cameraPosition.magnitude > threshold)
        {
            for (int z = 0; z < SceneManager.sceneCount; z++)
            {
                foreach (GameObject go in SceneManager.GetSceneAt(z).GetRootGameObjects())
                {
                    go.transform.position -= cameraPosition;
                }
            }

            Vector3 originDelta = Vector3.zero - cameraPosition;
            levelLayoutGenerator.UpdateSpawnOrigin(originDelta);
            Debug.Log("Recentering, origin delta = " + originDelta);

            // Set the target position for the camera
            targetPosition = gameObject.transform.position + originDelta;
        }

        // Smoothly move the camera towards the target position
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, transitionSpeed * Time.deltaTime);
    }
}
