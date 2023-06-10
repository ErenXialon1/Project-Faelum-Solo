using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // D��man�n h�z�
    private Transform player; // Oyuncunun konumu
    
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu etiketi "Player" olarak belirlenmi� olmal�d�r
        
    }
  
  
    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}