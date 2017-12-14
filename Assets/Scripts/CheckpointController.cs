using System.Collections;
using UnityEngine;


public class CheckpointController : MonoBehaviour {
    public int actualCheckpoint = 0;
    int lastCheckpoint;
    public bool inCheckPoint = false;
    float delay = 5;
    // Use this for initialization
    void Start()
    {
        lastCheckpoint = transform.childCount;
        inCheckPoint = false;
    }

    public Transform ActualCheckpoint()
    {
        return transform.GetChild(actualCheckpoint);
    }

    public void SetDelay(float t)
    {
        delay = t;
    }

    public void DelayAndTryToMove(GameObject g)
    {
        StartCoroutine(DelayToMove(delay,g));
    }

    public void MoveToActualCheckpoint(GameObject g)
    {
        StartCoroutine(DelayToMove(0,g));
    }

    bool VerifyPlayer(GameObject g)
    {
        var t = g.transform.parent;
        if (t)
        {
            t = t.parent;
            if (t && t.gameObject.name.Contains("Jogador")) return true;
        }
        return false;
    }

    public void NextCheckpoint()
    {
        actualCheckpoint = (actualCheckpoint + 1) % lastCheckpoint;
        inCheckPoint = true;
    }

    public void CheckPoint(int checkpoint)
    {
        checkpoint--;
        if (checkpoint > lastCheckpoint) throw new UnityException("Checkpoint inválido");
        actualCheckpoint = checkpoint;
        inCheckPoint = true;
    }

    public void OutCheckpoint()
    {
        inCheckPoint = false;
    }

    public void InCheckpoint()
    {
        inCheckPoint = true;
    }

    IEnumerator DelayToMove(float s, GameObject g)
    {
        //Debug.Log("Delay para tentar mover");
        yield return new WaitForSecondsRealtime(delay);
        //Debug.Log("Fim delay");
        if (!inCheckPoint)
        {
            Debug.Log(inCheckPoint);
            Debug.Log("Movendo");
            var rigid = g.GetComponent<Rigidbody>();
            if (rigid)
            {
                rigid.velocity = Vector3.zero;
                rigid.angularVelocity = Vector3.zero;
            }
            var checkpointTransform = ActualCheckpoint();
            g.transform.position = checkpointTransform.position;
            g.transform.rotation = checkpointTransform.rotation;
            inCheckPoint = true;
        }
    }

}
