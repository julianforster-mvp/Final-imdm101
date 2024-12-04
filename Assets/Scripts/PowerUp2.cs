using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerUp2 : MonoBehaviour
{
    public NavMeshAgent agent;

    // Trigger detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopSeeking();
        }
    }

    public void StopSeeking()
    {
        if (agent != null)
        {
            StartCoroutine(StopAndResume());
            Debug.Log("Power-Up Picked Up!");
        }
        else
        {
            Debug.LogError("NavMeshAgent is not assigned!");
        }
    }

    IEnumerator StopAndResume()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        Destroy(gameObject);
    }
}
