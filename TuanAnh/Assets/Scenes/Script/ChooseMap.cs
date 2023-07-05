using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMap : MonoBehaviour
{

    public void ChooseMap1()
    {
        SceneManager.LoadScene(1);

    }
    public void ChooseMap2()
    {
        SceneManager.LoadScene(2);
    }
    public void ChooseMap3()
    {
        SceneManager.LoadScene(3);
    }
    public void ChooseMap4()
    {
        SceneManager.LoadScene(4);
    }
}