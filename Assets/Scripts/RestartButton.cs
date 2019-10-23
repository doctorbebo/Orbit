using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void RestartButtonPress()
    {
        PlanetBehavior.farthestXPosition = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
