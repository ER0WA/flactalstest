using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField]
    GameObject triangle;

    [SerializeField]
    private GameObject Root;

    const float childSize = 0.5f;
    Vector2 childPosition = new Vector2 (2.6f, 1.13f);

    public float RandMax = 0.8f;
    public float RandMin = 0.1f;


    const int MAXITR = 4;

    private List<GameObject> triangles = new List<GameObject>();
    private List<float> triangleslife = new List<float>();


    private float timer = 0.0f;

    public float imperfect = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        createFractale(Root,1,0.5f,0.5f);


    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
    

        for( int i = 0; i < triangleslife.Count; ++i)
        {
            if(triangleslife[i] <= timer)
            {
                if(triangles[i].activeSelf == false)
                {
                    triangles[i].SetActive(true);
                  
                    if(Random.Range(0.0f,1.0f) > imperfect)
                    {
                        triangles[i].transform.localScale = new Vector3(0.0f,0.0f,0.0f);
                    }
       
                }
            }
        }

    }

    public void resetAll()
    {
        deleteAll();
        createFractale(Root, 1, 0.5f, 0.5f);
        timer = 0.0f;
    }


    void createFractale(GameObject root, int itr, float scale,float life)
    {

       if(itr > MAXITR)
        {
            return;
        }

        //GameObject go1 = Instantiate(triangle, new Vector3(-2.6f,-1.13f,0.0f),Quaternion.identity,root.transform);
        //GameObject go2 = Instantiate(triangle, new Vector3(2.6f, -1.13f, 0.0f), Quaternion.identity, root.transform);
        //GameObject go3 = Instantiate(triangle, new Vector3(0.0f, 3.32f, 0.0f), Quaternion.identity, root.transform);

        GameObject go1 = Instantiate(triangle, root.transform);
        GameObject go2 = Instantiate(triangle, root.transform);
        GameObject go3 = Instantiate(triangle, root.transform);

        go1.transform.localPosition = new Vector3(-2.6f, -1.13f, 0.0f);
        go2.transform.localPosition = new Vector3(2.6f, -1.13f, 0.0f);
        go3.transform.localPosition = new Vector3(0.0f, 3.32f, 0.0f);

        go1.name = "triangle" + itr.ToString() + "-a";
        go2.name = "triangle" + itr.ToString() + "-b";
        go3.name = "triangle" + itr.ToString() + "-c";
        go1.transform.localScale = new Vector3(scale, scale, 1.0f);
        go2.transform.localScale = new Vector3(scale, scale, 1.0f);
        go3.transform.localScale = new Vector3(scale, scale, 1.0f);

        go1.SetActive(false);
        go2.SetActive(false);
        go3.SetActive(false);
        triangles.Add(go1);
        triangles.Add(go2);
        triangles.Add(go3);
        float lifetime = Random.Range(RandMin, RandMax);
        triangleslife.Add(life + lifetime);
        createFractale(go1, itr + 1, scale, life + lifetime);

        lifetime = Random.Range(RandMin, RandMax);
        triangleslife.Add(life + lifetime);
        createFractale(go2, itr + 1, scale, life + lifetime);

        lifetime = Random.Range(RandMin, RandMax);
        triangleslife.Add( life + lifetime);
        createFractale(go3, itr + 1, scale, life + lifetime);
        





        if (Random.Range(0.0f,1.0f) > 0.3f) //40%
        {
            GameObject go4 = Instantiate(triangle, root.transform);
            go4.transform.localPosition = new Vector3(2.6f * (float)Random.Range(-3,3), 1.07f +  2.23f * (float)Random.Range(-3, 3), 0.0f);

            go4.name = "triangle" + itr.ToString() + "-d";
            go4.transform.localScale = new Vector3(scale, scale, 1.0f);


            triangles.Add(go4);
            lifetime = Random.Range(RandMin, RandMax);
            triangleslife.Add(life + lifetime);
            createFractale(go4, itr + 1, scale, life + lifetime);
            go4.SetActive(false);
        }


        if (Random.Range(0.0f, 1.0f) > 0.8f) //40%
        {
            GameObject go5 = Instantiate(triangle, root.transform);
            go5.transform.localPosition = new Vector3(2.6f * (float)Random.Range(-6, 6), 1.07f - 2.23f * (float)Random.Range(-6, 6), 0.0f);

            go5.name = "triangle" + itr.ToString() + "-d";
            go5.transform.localScale = new Vector3(scale, scale, 1.0f);


            triangles.Add(go5);
            lifetime = Random.Range(RandMin, RandMax);
            triangleslife.Add(life + lifetime);
            createFractale(go5, itr + 1, scale, life + lifetime);
            go5.SetActive(false);
        }
        
    }


    void deleteAll()
    {
        foreach (Transform n in Root.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
        triangles.Clear();
        triangleslife.Clear();
    }
}
