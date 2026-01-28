using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SampleCode : MonoBehaviour
{
    //[SerializeField] private GameObject prefab;
    [SerializeField] private GameObject moveObject;
    [SerializeField] private float speed = 5;
    void Start()
    {
        /*
        //Instantiate(prefab, new Vector2(-2, 0), Quaternion.identity); //(x, y, z, w)
        GameObject newGameObject = Instantiate(prefab);
        //newGameObject.transform.position = new Vector2(5, 0);
        newGameObject.transform.SetPositionAndRotation
            (
                new Vector3(5, 0, 0),
                Quaternion.Euler(45, 30, 0)
            );
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Left click is pressed");
        }

        else if (Input.GetMouseButtonDown(1)) 
        {
            Debug.Log("Right click is pressed");
        }
        */

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y);

        moveObject.transform.Translate(move * Time.deltaTime * speed);
    }
}
