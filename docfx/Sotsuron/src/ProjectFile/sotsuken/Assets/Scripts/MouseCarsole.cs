using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCarsole : MonoBehaviour
{
    [SerializeField] GameObject effect=null;
    private Vector3 position = Vector3.zero;
    private GameObject obj = null;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            position = Input.mousePosition; 
            obj = Instantiate(effect, Camera.main.ScreenToWorldPoint(position), Quaternion.identity);
            obj.transform.position = new Vector3 (obj.transform.position.x, obj.transform.position.y, 0f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(obj);
        }
        try
        {
            Debug.Log(obj.transform.position);
            position = Input.mousePosition;
            obj.transform.position = Camera.main.ScreenToWorldPoint(position);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0f);
        }
        catch
        {

        }
    }
}
