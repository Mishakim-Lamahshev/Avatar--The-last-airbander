using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int life = 3;
    public string LoseScene;

    // Call this method whenever the character loses a life
    public void LoseLife()
    {
        life--;

        // Check if life is zero, then load the win scene
        if (life <= 0)
        {
            SceneManager.LoadScene(LoseScene);
        }
    }
}
