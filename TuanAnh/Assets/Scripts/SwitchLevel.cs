using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
