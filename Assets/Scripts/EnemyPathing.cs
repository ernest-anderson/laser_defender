﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;        
    List<Transform> waypoints;
    int waypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {        
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {        
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetWaypoint = waypoints[waypointIndex].transform.position;
            var movementFrame = waveConfig.GetMovementSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, movementFrame);

            if (transform.position == targetWaypoint)
            {                
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}