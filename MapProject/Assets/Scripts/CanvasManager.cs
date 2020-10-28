using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> createObjects = new List<GameObject>();
    private float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SearchButton()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;
        if (Player != null)
        {
            // get a random postion to instantiate the prefab - you can change this to be created at a fied point if desired
            Vector3 position = new Vector3(UnityEngine.Random.Range(minX + 0.5f, maxX - 0.5f), UnityEngine.Random.Range(minY + 0.5f, maxY - 0.5f), 0);

            // instantiate the object
            GameObject go = (GameObject)Instantiate(Player, position, Quaternion.identity);
        }
    }
}
