// Example call from a scene script (e.g., MainMenu, Level1, EndScene)
using UnityEngine;

public class EndScene : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic("EndScreen");
    }
}
