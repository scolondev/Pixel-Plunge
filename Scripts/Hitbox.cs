using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
	//Default Damage
	public Stat damage = new Stat(1,10,0);
	public float duration = 0.2f;
	
	public bool destroyOnCollision = false;
	public List<string> ignore = new List<string>() {
      "Untagged",
      "enemy",
      "node_center"
    };

	public virtual void Start(){
		Destroy(this.gameObject, duration);
	}
	
	private void OnTriggerEnter2D(Collider2D collision){
        if (!ignore.Contains(collision.tag)){

            collision.gameObject.SendMessage("TakeDamage", damage.value, SendMessageOptions.DontRequireReceiver);

            if (destroyOnCollision)
            {
                Debug.Log(collision.tag);
                Destroy(this.gameObject);
            }
        }
	}
}
