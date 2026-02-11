using UnityEngine;

public class PT2_Code : MonoBehaviour
{



    [SerializeField] private GameObject Prefab;
    [SerializeField] private GameObject moveObject;
    [SerializeField] private float speed = 7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Instantiate(Prefab, new Vector2(-2, 0), Quaternion.identity);
        //GameObject newGameobject = Instantiate(Prefab);

        //newGameobject.transform.SetPositionAndRotation
        //    (
        //        new Vector3(5, 0, 0),
        //        Quaternion.Euler(45, 30, 0)

        //    );
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical"); 


        Vector2 move = new Vector2(x, y);

        moveObject.transform.Translate(move * Time.deltaTime * speed);


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("LEFT MOUSE BUTTON");

            GameObject newGameObject = Instantiate(Prefab);

            newGameObject.transform.position = moveObject.transform.position;
        }




        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Left Click");
        //}
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    Debug.Log("right Click");
        //}
    }
}
