using UnityEngine;

public class OriginalCenter : MonoBehaviour
{
    Transform shipTransform;
    public GameObject planet;

    private void Start()
    {
        shipTransform = GameObject.FindWithTag("Player").gameObject.transform;
    }


    private void Update()
    {
        if (shipTransform.position.x > transform.position.x + 35f)
        {
            InstatiateandDestroy();
        }
    }
    private void InstatiateandDestroy()
    {
        Instantiate(planet, new Vector3(PlanetBehavior.farthestXPosition + Random.Range(40, 50), Random.Range(-10f, 10f)), Quaternion.identity);
        Destroy(gameObject);
    }
}
