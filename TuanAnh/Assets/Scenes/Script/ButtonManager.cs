using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
