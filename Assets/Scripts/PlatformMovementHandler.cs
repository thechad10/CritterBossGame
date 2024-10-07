using UnityEngine;
using System.Collections.Generic;
using System;

public class PlatformMovementHandler : MonoBehaviour
{
    public bool isMoving = true;
    [SerializeField] private float platformMovementSpeed = 200f, backgroundMovementSpeed = 100f;
    [SerializeField] private Transform positionStartPlat, positionEndPlat, positionStartBackground, positionEndBackground;
    [SerializeField] private List<GameObject> platformParents = new List<GameObject>();
    [SerializeField] private List<GameObject> backgrounds = new List<GameObject>();

    void Update()
    {
        if (isMoving)
        {
            foreach (GameObject platformP in platformParents)
            {
                if (platformP.transform.position.x <= positionEndPlat.transform.position.x)
                    platformP.transform.position = new Vector2(positionStartPlat.transform.position.x, platformP.transform.position.y);
            }

            foreach (GameObject background in backgrounds)
            {
                if (background.transform.position.x <= positionEndBackground.transform.position.x)
                    background.transform.position = new Vector2(positionStartBackground.transform.position.x, background.transform.position.y);        
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            foreach (GameObject platformP in platformParents)
                platformP.GetComponent<Rigidbody2D>().linearVelocityX = -platformMovementSpeed * Time.deltaTime;
            foreach (GameObject background in backgrounds)
                background.GetComponent<Rigidbody2D>().linearVelocityX = -backgroundMovementSpeed * Time.deltaTime;
        }
        else
        {
            foreach (GameObject platformP in platformParents)
                platformP.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            foreach (GameObject background in backgrounds)
                background.GetComponent<Rigidbody2D>().linearVelocityX = 0;
        }

    }
}
