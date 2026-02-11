using UnityEngine;

public class CalledCollision : MonoBehaviour 
{
    //collision properties

    #region
    [SerializeField] private Material obstacleMaterial;
    [SerializeField] private Color matColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Collided");
            Debug.Log(collision.gameObject.name);
            MeshRenderer rend = collision.gameObject.GetComponent<MeshRenderer>();
            obstacleMaterial = rend.material;
            matColor = obstacleMaterial.color;


            obstacleMaterial.color = Color.red;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Cube Stay");
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
            transform.localScale -= new Vector3(1f, 1f, 1f);
    }


    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Cube Fall");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            MeshRenderer renderer = collision.gameObject.GetComponent<MeshRenderer>();
            renderer.material.color = matColor;
        }
    }

    #endregion


    #region 


    [SerializeField] private GameObject ObstacleOne;
    [SerializeField] private GameObject ObstacleTwo;

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision contain = other.GetComponent<CheckCollision>();
        GameObject newGameOject = contain.obstacle;

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("");
    }


    private void OnTriggerExit(Collider other)
    {

    }
    #endregion

}
