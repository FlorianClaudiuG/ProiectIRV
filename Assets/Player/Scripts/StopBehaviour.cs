﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehaviour : MonoBehaviour
{
    [SerializeField] public List<GameObject> EnemySpawns;
    [SerializeField] private float stopRadius = 10f;
    GameObject player = null;
    private bool spawnsEnabled = false;

    [SerializeField] private int directionToTurn;
    [SerializeField] private float waitTime;


    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            //player = GameManager.Instance.Player;
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer < stopRadius)
        {
            Debug.Log(EnemySpawns.Count + "Enemy Spawns");
            if (EnemySpawns.Count != 0)
            {
                if(!spawnsEnabled)
                {
                    player.GetComponent<PlayerController>().SetWaitTime(true);
                    foreach (GameObject spawn in EnemySpawns)
                    {
                        spawn.SetActive(true);
                    }
                    spawnsEnabled = true;
                }
                else
                {
                    foreach(GameObject spawn in EnemySpawns)
                    {
                        if(!spawn.activeSelf)
                        {
                            EnemySpawns.Remove(spawn);
                        }
                    }
                }
            }
            else
            {
                stopRadius = 0;
                StartCoroutine(waitInSpot());
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw attack sphere 
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, stopRadius);
    }


    IEnumerator waitInSpot()
    {
        player.GetComponent<PlayerController>().SetWaitTime(true);
        //player.GetComponent<PlayerController>().TurnInDirection(directionToTurn);
        //yield return new WaitForSeconds(waitTime);
        player.GetComponent<PlayerController>().waitTime = 1/waitTime;
        player.GetComponent<PlayerController>().rotate = true;
        player.GetComponent<PlayerController>().direction = directionToTurn;
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<PlayerController>().rotate = false;
        player.GetComponent<PlayerController>().SetWaitTime(false);
      
    }
}
