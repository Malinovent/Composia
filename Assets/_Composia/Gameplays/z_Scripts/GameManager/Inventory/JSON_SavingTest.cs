using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSON_SavingTest : MonoBehaviour
{
    private JSON_TESTCLASSDATA _testData = new JSON_TESTCLASSDATA();

    public int _valueI;
    public float _valueF;
    public string _valueS;
    public List<string> _listS = new List<string>();

    public void Awake()
    {
        _testData._valueI = _valueI;
        _testData._valueF = _valueF;
        _testData._valueS = _valueS;
        _testData._listS = _listS;

        string json = JsonUtility.ToJson(_testData);

        if (!File.Exists(Application.persistentDataPath + "/JSON_TESTDATA.json"))
        {
            Debug.Log("Create new save file");
            System.IO.File.WriteAllText(Application.persistentDataPath + "/JSON_TESTDATA.json", json);
        }
        else
        {
            //Access file to not overwrite everthing
            if (System.IO.File.ReadAllText(Application.persistentDataPath + "/JSON_TESTDATA.json") !=
                json)
            {
                Debug.Log("OverwriteSave file");//Can use same method to overwrite?
                System.IO.File.WriteAllText(Application.persistentDataPath + "/JSON_TESTDATA.json", json);
            }
            else
            {
                Debug.Log("Text already saved !");
            }
        }
    }

    //void Start()
    //{
    //    Debug.Log(Application.persistentDataPath);
    //}

}

[System.Serializable]
public class JSON_TESTCLASSDATA
{
    public int _valueI;
    public float _valueF;
    public string _valueS;
    public List<string> _listS = new List<string>();
}
