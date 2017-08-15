using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlowSystem : MonoBehaviour {

    private bool showing;
    private GameObject AF;
    private ParticleSystem airflow;
    ParticleSystem.EmissionModule em;

    void Awake()
    {
        AF = this.gameObject;
        airflow = AF.GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        airflow.Play(false);

        //em = airflow.emission;
        //em.enabled= false;
    }

    void Update()
    {
        //print(airflow.particleCount);
    }

    public void OnAirFlow()
    {
        if (!showing)
        {
            //airflow.Play(true);
            em.enabled = true;
            showing = true;
        }
        else
        {
            //airflow.Play(false);
            em.enabled = false;
            showing = false;
        }
    }
}
