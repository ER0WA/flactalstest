using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystem : MonoBehaviour
{

    GameObject m_Root = null;

    const float FORWARD_DISTANCE = 0.2f;

    class Pointer
    {
        public Vector3 pos = Vector3.zero;
        public Quaternion rot = Quaternion.identity;
    }

    //string m_InitialString = "F";
    string m_InitialString = "-F";
    string m_CurrentString = "";
    Dictionary<string, string> m_RuleTable = null;
    Pointer m_Pointer = null;
    int m_Index = 0;

    void Awake()
    {
        Buildup();
    }


    public void Buildup()
    {
        if(m_Root != null)
        {
            Destroy(m_Root);
            Debug.Log("Delete");
        }


        m_Pointer = new Pointer();
        m_Pointer.pos = Vector3.zero;
        m_Pointer.rot = Quaternion.identity;
        m_Root = new GameObject("Tree");
        m_Root.transform.position = Vector3.zero;
        m_Root.transform.rotation = Quaternion.identity;
        m_RuleTable = null;
        m_RuleTable = new Dictionary<string, string>();
        //m_RuleTable.Add("F", "FFF-FF-F-F+F+FF-F-FFF");

        int lenght = Random.Range(3, 5);
        string tmp = "";
        for (int i = 0; i < lenght; ++i)
        {
           switch(Random.Range(0,3))
            {
                case 0:
                    tmp += "FF";
                    break;
                case 1:
                    tmp += "F";
                    break;
                case 2:
                    tmp += "FFF";
                    break;
                default:
                    break;
            }

            switch (Random.Range(0, 2))
            {
                case 0:
                    tmp += "+";
                    break;
                case 1:
                    tmp += "-";
                    break;
                default:
                    break;
            }

        }

        m_RuleTable.Add("F", tmp);
        //m_RuleTable.Add("F", "FFF-FF-F-F+F+FF-F-FFF");
        m_RuleTable.Add("-", "-");
        m_RuleTable.Add("+", "+");
        string cmd = m_InitialString;
        for (int i = 0; i < 4; ++i)
        {
            m_CurrentString = "";
            foreach (char s in cmd)
            {
                m_CurrentString += m_RuleTable[s.ToString()];
            }
            cmd = m_CurrentString;
        }

        Build(m_CurrentString);

    }

    void Build(string command)
    {
        foreach (char s in command)
        {
            ParseCommand(s);
        }
        m_Root.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.up);
    }

    void ParseCommand(char s)
    {
        switch (s)
        {
            case 'F':
                Debug.Log("pos=" + m_Pointer.pos + ",rot=" + m_Pointer.rot);
                GameObject go = GameObject.Instantiate(Resources.Load("Branch")) as GameObject;
                go.name = "Branch" + m_Index;
                go.transform.position = m_Pointer.pos;
                go.transform.rotation = m_Pointer.rot;
                go.transform.parent = m_Root.transform;
                Vector3 n = m_Pointer.rot * (FORWARD_DISTANCE * Vector3.up) + m_Pointer.pos;
                //Vector3 n = m_Pointer.rot * (FORWARD_DISTANCE * Vector3.up) + m_Pointer.pos;
                m_Pointer.pos = n;
                break;
            case '-':
                // m_Pointer.rot = m_Pointer.rot * Quaternion.AngleAxis(90.0f, Vector3.right);
                m_Pointer.rot = m_Pointer.rot * Quaternion.AngleAxis(90.0f, Vector3.right);
                break;
            case '+':
                m_Pointer.rot = m_Pointer.rot * Quaternion.AngleAxis(-90.0f, Vector3.right);
                break;
        }
        m_Index++;
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}