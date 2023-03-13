using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy_Bullet", 3f);
    }

    // Update is called once per frame
    void Destroy_Bullet()
    {
        Destroy(this.gameObject);
    }
}
