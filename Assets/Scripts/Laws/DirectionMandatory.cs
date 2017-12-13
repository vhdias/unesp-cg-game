using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMandatory : MonoBehaviour {
    public Vector3 mandatoryDirection;

    private void OnTriggerStay(Collider other)
    {
        var player = other.transform.parent;
        if (player)
        {
            player = player.parent;
            if (!player) return;
            Vector3 currentDirection = GetLocalDirection(player);
            if (mandatoryDirection.magnitude != 1) throw new UnityException("Direção inválida");
            if ((mandatoryDirection.x == 1 && Mathf.Round(currentDirection.x) < 0) ||
                (mandatoryDirection.y == 1 && Mathf.Round(currentDirection.y) < 0) ||
                (mandatoryDirection.z == 1 && Mathf.Round(currentDirection.z) < 0))
            {
                Debug.Log("Você está na direção contrária da via");
                return;
            }
            throw new UnityException("Direção inválida");
        }
    }

    Vector3 GetLocalDirection(Transform body)
    {
        return body.InverseTransformVector(body.GetComponent<Rigidbody>().velocity);
    }
}
