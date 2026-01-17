using UnityEngine;

public class SnapToTrack : MonoBehaviour
{
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2000f))
        {
            transform.position = hit.point + Vector3.up * 0.5f;
        }
    }
}

