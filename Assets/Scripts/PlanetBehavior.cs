using UnityEngine;

public class PlanetBehavior : MonoBehaviour
{
    public static float farthestXPosition;
    public static float LastYPosition;

    public Material[] materials;

    MeshRenderer rend;
    Transform shipTransform;


    private void Start()
    {

        rend = GetComponent<MeshRenderer>();
        rend.material = materials[Random.Range(0, 3)];

        if (farthestXPosition < transform.position.x)
        {
            farthestXPosition = transform.position.x;
        }

        shipTransform = GameObject.FindWithTag("Player").gameObject.transform;
        if (ShipController.gameMode == ShipController.GameMode.Learning || PlayerPrefs.GetInt ("highScore", 0) == 0)
        {
            transform.position = new Vector3(transform.position.x, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Random.Range(-10f, 10f));
        }
    }


    private void Update()
    {
        if (shipTransform.position.x > transform.position.x + 40f)
        {
            ResetPlanet();
        }
    }
    private void ResetPlanet()
    {
        transform.position = new Vector3(farthestXPosition + Random.Range(40, 50f), Random.Range(-10f, 10f));
        farthestXPosition = transform.position.x;
        rend.material = materials[Random.Range(0, 3)];
    }
    
}