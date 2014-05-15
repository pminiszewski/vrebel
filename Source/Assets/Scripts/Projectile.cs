using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{

    public float LifeSpan = 4;
    public float Speed = 10;
    public float Damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision colision)
    {
        Character c = colision.collider.GetComponent<Character>();
        if (c != null)
        {
            c.OnHit(this);
        }
        Destroy(gameObject);
    }

    IEnumerator BulletTimeout()
    {
        while (true)
        {

            yield return new WaitForSeconds(LifeSpan);
            Destroy(gameObject);
        }

    }
}
