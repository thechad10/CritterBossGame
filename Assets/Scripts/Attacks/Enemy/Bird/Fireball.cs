using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 10;
    [SerializeField]
    private float ballSpeed = 1;
    private float timer;
    private void OnEnable()
    {
        timer = 0;
    }
    private void Update()
    {
        transform.position += (transform.up) * Time.deltaTime * ballSpeed;
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
