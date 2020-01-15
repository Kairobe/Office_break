using UnityEngine;

public class ShootSlingshot : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private Vector3 direction;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        Player player;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        direction = player.GetDirection();

        Destroy(gameObject, 3.0f);
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    /// <summary> Called when the GameObject collides with another GameObject. </summary>
    /// <param name="collider">
    /// The <see cref="Collider"/> with details about the trigger event, such as the name of its GameObject.
    /// </param>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemigo"))
        {
            Enemy enemy = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<Enemy>();

            enemy.HitWithArrow();
        }
        else if (!collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}