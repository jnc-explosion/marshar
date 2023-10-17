using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float power = 10f;
    [SerializeField] float accuracy = 10f;

    void Start()
    {
        
    }

    int timer=0;

    void FixedUpdate()
    {
        if(Input.GetButton("Shot"))
        {
            // set deviation
            var deviation = new Vector3();
            deviation = Random.insideUnitSphere;

            var bullet = Instantiate(Bullet, this.transform.position, Quaternion.identity);
            // lunch
            bullet.GetComponent<Rigidbody>().AddForce((this.transform.forward * power) + (this.transform.forward * accuracy) + deviation * power / 16);
        }
    }
}
