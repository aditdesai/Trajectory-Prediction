using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootScript : MonoBehaviour
{
    //AIR RESISTANCE HAS BEEN NEGLECTED
    [Header("References")]
    public Camera cam;
    public Rigidbody rb;

    [Header("General")]
    public float speed = 15f;
    private Vector3 initialPos;

    [Header("Trajectory")]
    public int numOfPoints = 10;
    public GameObject pathPoint;
    private GameObject[] pathPoints;

    private Vector3 u;
    private Vector3 uCap;

    private void Start()
    {
        initialPos = transform.position;
        pathPoints = new GameObject[numOfPoints];

        for (int i = 0; i < numOfPoints; ++i)
        {
            pathPoints[i] = Instantiate(pathPoint, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ReloadScene();
        }

        FindVelocityVector();

        transform.rotation = Quaternion.LookRotation(u);

        DrawProjectile();
        
        if (Input.GetMouseButtonUp(0))
        {
            Launch();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    void FindVelocityVector()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            u = hit.point - transform.position;//position vector, not velocity vector
        }
        uCap = u.normalized;
        u = uCap * speed;
    }

    void DrawProjectile()
    {
        if (transform.position == initialPos)
        {
            for (int i = 0; i < numOfPoints; ++i)
            {
                pathPoints[i].transform.position = PointPosition(i * 0.05f);
            }
        }
    }

    void Launch()
    {
        rb.useGravity = true;
        rb.velocity = transform.forward * speed;
    }

    Vector3 PointPosition(float t)
    {
        Vector3 currentPos = initialPos + (u * t) + (0.5f * Physics.gravity * t * t);
        return currentPos;
    }
}
