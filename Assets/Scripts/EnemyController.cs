using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// a animator variable
	public Animator myAnim;
	// To keep track of the player
	private Transform target;

	[SerializeField]
	private float speed;

	// variables to restrict the range of enemies can be modified here.
	public float maxRange = 4f;

	public float minRange = 0.75f;

	// enemies max and current health
	public int maxHealth;
	public int currentHealth;
	public HealthBar healthBar;

	public float waitTime;

	// public GameObject lootDrop;

	public List<GameObject> items = new List<GameObject>();

	// Start is called before the first frame update
	void Start() {
		// getting an animator and player object to operate onto.
		myAnim = GetComponent<Animator>();
		target = FindObjectOfType<Player>().transform;
		// setup enemy health
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void TakeDamage(int damageValue) {
		currentHealth -= damageValue;
		myAnim.SetTrigger("Hurt");
		if (currentHealth <= 0){
			Die();
		}
		healthBar.SetCurrentHealth(currentHealth);
	}

	void Die(){
		// Die animation
		myAnim.SetBool("IsDead", true);
		// disable enemy script and collider
		GetComponent<Collider2D>().enabled = false;
		Invoke("DestroyEnemy", waitTime);
	}

	void DestroyEnemy(){
		// can have a death effect to if we want
		Destroy(gameObject);

		// Instantiate(lootDrop, transform.position, Quaternion.identity);
		Instantiate(items[Random.Range(0, items.Count-1)], transform.position, Quaternion.identity);
	}


}