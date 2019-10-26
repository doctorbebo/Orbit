using UnityEngine;

public class OriginalCenter : MonoBehaviour
{
    Transform shipTransform;

    private void Start()
    {
        shipTransform = GameObject.FindWithTag("Player").gameObject.transform;
    }


    private void Update()
    {
        if (shipTransform.position.x > transform.position.x + 100f)
        {
            Destroy(gameObject);
        }
    }

}
