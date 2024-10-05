using UnityEngine;
using System.Collections.Generic;

public class SquirrelPrimary : MonoBehaviour
{
    [SerializeField] private GameObject squirrelProjectile;
    [SerializeField] private int spawnPoolAmount;
    private List<GameObject> projectilePool = new List<GameObject>();
    private int listIncrementor;

    [SerializeField] private float attackSpeed = 0.25f;
    private float attackCounter;
    private bool attackReady;

    private PlayerInput pInput;

    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        for (int i = 0; i < spawnPoolAmount; i++)
        {
            GameObject spwanedProj = Instantiate(squirrelProjectile);
            spwanedProj.GetComponent<SpriteRenderer>().enabled = false;
            projectilePool.Add(spwanedProj);
        }
    }

    void Update()
    {
        if (attackCounter > 0)
            attackCounter -= Time.deltaTime;
        else
            attackReady = true;

        if (pInput.Player.Attack.IsInProgress() && attackReady)
        {
            attackReady = false;
            attackCounter = attackSpeed;
            projectilePool[listIncrementor].GetComponent<SpriteRenderer>().enabled = true;
            // other boolet logic here
            listIncrementor++;

            if (listIncrementor >= projectilePool.Count)
                listIncrementor = 0;
        }
    }
}