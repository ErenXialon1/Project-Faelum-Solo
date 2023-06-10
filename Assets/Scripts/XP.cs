using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    public float speed;
    public SkillManager skillManagerScript;
    private Transform playerPosition;
    public float distanceBetweenPlayer;
    // Start is called before the first frame update
    void Start()
    {
        skillManagerScript = FindObjectOfType<SkillManager>().GetComponent<SkillManager>();
        playerPosition = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenPlayer = Vector2.Distance(playerPosition.position, transform.position);

        if (distanceBetweenPlayer < skillManagerScript.distanceNeeded)
        {
            Vector2 direction = playerPosition.position - transform.position;
            direction.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        }
    }
   
}
