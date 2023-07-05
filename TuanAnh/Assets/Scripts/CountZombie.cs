using TMPro;
using UnityEngine;

public class CountZombie : MonoBehaviour
{
    public GameObject teleporter;
    public TextMeshProUGUI Count;
    public int countZombie;
    // Start is called before the first frame update
    void Start()
    {
        countZombie = GameObject.FindGameObjectsWithTag("Zombie").Length;
        teleporter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        countZombie = GameObject.FindGameObjectsWithTag("Zombie").Length;
        Count.text = "Zombie Remaining: " + countZombie.ToString();

        if(countZombie == 0)
        {
            teleporter.SetActive(true);
        }
    }

}
