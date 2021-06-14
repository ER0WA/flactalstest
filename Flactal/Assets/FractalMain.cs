using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalMain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject root = null;

    [SerializeField]
    Material basicMat;

    [SerializeField]
    UIManager2 uiManager2;

    public List<TriangleData> datas = null;// new List<TriangleData>();
    
    private int MaxGeneration = 5;
    public int NextMaxGeneration = 5;

    public bool mode = true;

    public float IncSpeed = 10.0f;
    public float DecSpeed = 10.0f;

    private int totalNodeNum = 1;



    public struct TriangleData
    {
        public Vector3[] positions;
        public Color color;
        public float rot;
        public float thicness;
        public float rate { get; set; }
        public TriangleData[] children;
        public int generation;
        public LineRenderer line;
        public int index;

        public TriangleData(Vector3 a,float r, float size, int gen)
        {
            positions = new Vector3[5];
            positions[0] = a;
            float r1 = 30.0f * Mathf.Deg2Rad;
            float r2 = 330.0f * Mathf.Deg2Rad;
            positions[1] = a + new Vector3(size * Mathf.Sin( r1), size * Mathf.Cos(r1), 0.0f);
            positions[2] = a + new Vector3(size * Mathf.Sin(r2), size * Mathf.Cos(r2), 0.0f);
            positions[3] = positions[0];
            positions[4] = positions[0];
            rot = 0.0f;
            color = Color.white;
            thicness = 1.0f;
            rate = 0.0f;
            generation = gen;
            children = new TriangleData[3];
            line = null;
            index = -1;

        }
    }

    private LineRenderer[] lines = new LineRenderer[2];


    void Start()
    {
        SetupLineRenderer(root.transform, new Vector3(0.0f, -5.0f, 0.0f),4.0f,1);
       
    }

    public void SwitchMode()
    {
        mode = !mode;
    }

    public void ModeOff()
    {
        mode = false;
    }

    public void ModeOn()
    {
        mode = true;
    }

    public void Restart()
    {
        DeleteAll();
        MaxGeneration = NextMaxGeneration;
        totalNodeNum = 0;
        SetupLineRenderer(root.transform, new Vector3(0.0f, -5.0f, 0.0f), 4.0f, 1);
    }
    
    void Update()
    {
        if (mode)
        {
            UpdateInner(datas[0]);
        }
        else
        {
            DecreaseInner(datas[0]);
        }

        uiManager2.UpdateGauge(GetCompRate());

    }

    public int GetCompRate()
    {
        int comp = 0;
        foreach(var d in datas)
        {
            if(d.rate >=1.0f)
            {
                comp++;
            }
        }
        float a = (float)comp;
        float b = (float)totalNodeNum;
        return Mathf.CeilToInt( a / b * 100.0f);
    }


    void  UpdateInner(TriangleData data)
    {
       
        if (data.generation <= MaxGeneration && data.index != -1)
        {
            
            if (datas[data.index].rate == 1.0f)
            {
                for (int i = 0; i < data.children.Length; ++i)
                {
                    UpdateInner(datas[data.index].children[i]);
                }
                   
            }else
            {
                data.rate = datas[data.index].rate + Time.deltaTime * (data.generation+1) / MaxGeneration * IncSpeed; //進かな
                
                if (data.rate >= 1.0f)
                {
                    data.rate = 1.0f;
                }

                if (data.rate >= 0.0f)
                {
                    UpdateLine(data, data.rate);
                }
                else
                {
                    UpdateLine(data, 0.0f);
                }
                datas[data.index] = data;
            }
        }
    }

    void DecreaseInner(TriangleData data)
    {
        if (data.generation <= MaxGeneration && data.index != -1)
        {
            if (datas[data.index].generation == MaxGeneration && data.rate >= 0.0f)
            {
                data.rate = datas[data.index].rate - Time.deltaTime * (data.generation + 1) / MaxGeneration * DecSpeed;

                if (data.rate <= 0.0f)
                {
                    data.rate = 0.0f;
                    UpdateLine(data, 0.0f);
                }

                if (data.rate > 0.0f)
                {
                    UpdateLine(data, data.rate);
                }
                datas[data.index] = data;

            } else if (datas[data.children[0].index].rate <= 0.0f && datas[data.children[1].index].rate <= 0.0f && datas[data.children[2].index].rate <= 0.0f && datas[data.index].generation != MaxGeneration)
            {
                data.rate = datas[data.index].rate - Time.deltaTime * (data.generation + 1) / MaxGeneration * DecSpeed;

                if (data.rate <= 0.0f)
                {
                    data.rate = 0.0f;
                    UpdateLine(data, 0.0f);
                }

                if (data.rate > 0.0f)
                {
                    UpdateLine(data, data.rate);
                }
                datas[data.index] = data;
            }
            else
            {
                for (int i = 0; i < data.children.Length; ++i)
                {
                    DecreaseInner(datas[data.children[i].index]);
                }

            }
        }
    }


    private void initLine(TriangleData data)
    {
        data.line.positionCount = data.positions.Length;
        data.line.SetPositions(new Vector3[5] { data.positions[0], data.positions[0], data.positions[0], data.positions[0], data.positions[0] });
        data.line.startWidth = 0.03f;
        data.line.endWidth = 0.03f;
        data.line.startColor = Color.white;
        data.line.endColor = Color.white;
        data.line.loop = false;

        data.line.material = basicMat;
        data.line.numCapVertices = 5;
        data.line.numCornerVertices = 5;
        data.line.enabled = false;

    }

    private void UpdateLine(TriangleData data,float rate)
    {
        Vector3[] pos = new Vector3[5] { data.positions[0], data.positions[0], data.positions[0], data.positions[0], data.positions[0] };
        
        if (data.rate > 0.0f)
        {
            data.line.enabled = true;
            if (rate <= 0.33f)
            {
                Vector3 currentpos = data.positions[0] + (rate)/0.33f * (data.positions[1] - data.positions[0]);
                pos = new Vector3[5] { data.positions[0], currentpos, currentpos, currentpos, currentpos };
                if (data.line == null)
                {
                    Debug.Log(data.generation);
                }
                else
                {
                    data.line.SetPositions(pos);
                }

            }
            else if (data.rate <= 0.67f)
            {

                Vector3 currentpos = data.positions[1] + (rate - 0.33f) / 0.33f * (data.positions[2] - data.positions[1]);
                pos = new Vector3[5] { data.positions[0], data.positions[1], currentpos, currentpos, currentpos };
                data.line.SetPositions(pos);

            }
            else
            {

                Vector3 currentpos = data.positions[2] + (rate - 0.67f) / 0.33f * (data.positions[3] - data.positions[2]);
                pos = new Vector3[5] { data.positions[0], data.positions[1], data.positions[2], currentpos, currentpos };
                data.line.SetPositions(pos);

            }
        }else
        {
            data.line.enabled = false;
        }
        

    }

    TriangleData SetupLineRenderer(Transform root,Vector3 pos, float size, int gen)
    {
        TriangleData data = new TriangleData(pos, 0.0f, size, gen);
        GameObject go = new GameObject(gen.ToString());
        go.transform.parent = root; 
        go.AddComponent<LineRenderer>();
       
        data.line = go.GetComponent<LineRenderer>();
        initLine(data);
        if(datas == null)
        {
            datas = new List<TriangleData>();
        }
        data.index = datas.Count;

        
        datas.Add(data);

        data.rate = Random.Range(0.0f,2.0f);
        data.rate = data.rate * data.rate * -1.0f;


        if (data.generation <= MaxGeneration)
        {
            totalNodeNum = totalNodeNum + 1;
            data.children[0] = SetupLineRenderer(root, new Vector3(data.positions[1].x, data.positions[0].y, 0.0f), size / 2.0f, gen + 1);
            data.children[1] = SetupLineRenderer(root, new Vector3(data.positions[2].x, data.positions[0].y, 0.0f), size / 2.0f, gen + 1);
            data.children[2] = SetupLineRenderer(root, new Vector3(data.positions[0].x, data.positions[1].y, 0.0f), size / 2.0f, gen + 1);
        }
        
        datas[data.index] = data;


        return data;

    }

    void DeleteAll()
    {
        foreach (Transform n in root.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
        datas.Clear();
    }


}
