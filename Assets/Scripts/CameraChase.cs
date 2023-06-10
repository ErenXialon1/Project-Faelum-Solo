using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{

    [SerializeField] GameObject player;
    void Update()
    {       //kameranýn oyuncuyu takip etmesi
        transform.position = new Vector3(player.transform.position.x , player.transform.position.y , -20);
    }
}
