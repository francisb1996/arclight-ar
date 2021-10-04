using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{ 
    public ARPlaneManager planeManager;
    public Camera ARCamera;
    public GameObject cursorChildObject;
    public ARRaycastManager raycastManager;
    public bool placeSet = true;
    public GameObject gameSetPrefab;
    private GameObject gameSet;

    public void Start()
    {
        gameSet = GameObject.Instantiate(gameSetPrefab);
        gameSet.SetActive(false);
    }

    public void Action()
    {
        GameObject cube = gameSet.transform.Find("Cube").gameObject;
        if (placeSet)
        {
            gameSet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            gameSet.transform.rotation = transform.rotation;
            gameSet.SetActive(true);
            placeSet = false;
            planeManager.SetTrackablesActive(false);
            planeManager.enabled = false;
        }
        else
        {
            cube.transform.position = transform.position;
            cube.transform.rotation = transform.rotation;
            cube.transform.position += transform.rotation * new Vector3(0, cube.transform.localScale.y / 2, 0);
            cube.transform.TransformPoint(0, cube.transform.localScale.y / 2, 0);
        }
    }

    void Update()
    {
        Ray ray = ARCamera.ScreenPointToRay(new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, 1))//5 meter distance
        {
            transform.position = hit.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    public void Reset()
    {
        Destroy(gameSet);
        gameSet = GameObject.Instantiate(gameSetPrefab);
        gameSet.SetActive(false);
    }
}
