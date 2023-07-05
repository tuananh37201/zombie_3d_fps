using System.Collections;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Dis());
    }

    IEnumerator Dis()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
