using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	
	void OnCollisionEnter(Collision col)
    {
    	GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
    	Destroy(e,1.5f);

        Tank tank = col.gameObject.GetComponentInParent<Tank>();
        if (tank) { 
            tank.Damage(1);
        }

    	Destroy(gameObject);
    }

}
