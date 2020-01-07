using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyEffect());
    }

    private IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
