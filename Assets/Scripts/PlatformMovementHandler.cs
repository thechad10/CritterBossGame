using UnityEngine;
using System.Collections.Generic;

public class PlatformMovementHandler : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Transform positionStart, positionEnd;
    [SerializeField] private List<GameObject> platformParents = new List<GameObject>();

    void Update()
    {
        foreach (GameObject platformP in platformParents)
        {
            if (platformP.transform.position.x <= positionEnd.transform.position.x)
                platformP.transform.position = new Vector2(positionStart.transform.position.x, platformP.transform.position.y);
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject platformP in platformParents)
            platformP.GetComponent<Rigidbody2D>().linearVelocityX = -movementSpeed * Time.deltaTime;
    }
}
