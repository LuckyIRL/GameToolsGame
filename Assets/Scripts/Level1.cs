// Example call from a scene script (e.g., MainMenu, Level1, EndScene)
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMusic("Game");
    }
}
