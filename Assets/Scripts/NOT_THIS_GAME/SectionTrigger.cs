using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject roadSection;
    public GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(roadSection, new Vector3((float)-0.4, 4, (float)-188.8), Quaternion.identity);
        }
    }
}
