using UnityEngine;

public class Garlic : MonoBehaviour
{
    public bool scaleEnabled = false;
    private Vector3 originalScale;
    public float garlicDamage = 2;
    public float garlicCooldown ;
    public bool cooledDown;
    public Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        
        originalScale = transform.localScale;
        
        
        
        
    }
    
    private void Update()
    {
        if (scaleEnabled)
        {
            transform.localScale *= 1.25f;
            scaleEnabled = false;
        }
        garlicCooldown += Time.deltaTime;
        if(garlicCooldown >= 0.05f)
        {
            garlicCooldown = 0f;
            cooledDown = true;
        }
        transform.position = playerTransform.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && cooledDown == true)
        {


            collision.gameObject.GetComponent<Enemy>().enemyCurrentHP -= garlicDamage;
          cooledDown = false;
            if (collision.gameObject.GetComponent<Enemy>().enemyCurrentHP <= 0)
            {
                collision.gameObject.GetComponent<Enemy>().EnemyDeath();
            }
        }
    }

    public void ToggleScale()
    {
        scaleEnabled = !scaleEnabled;
    }



}