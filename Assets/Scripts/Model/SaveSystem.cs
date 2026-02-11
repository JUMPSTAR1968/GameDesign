using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    //JSON File

    [SerializeField] GameObject playerModel;
    public void Save(Vector3 playerPosition, Quaternion playerRotation) 
    {
        SavePosition savePosition = new SavePosition
        {
            position = playerPosition,
            rotation = playerRotation
        };

        string json = JsonUtility.ToJson(savePosition);

        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }

    public void Load() 
    {
        if (File.Exists(Application.persistentDataPath + "/save.txt"))
        {
            string loadfile = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            SavePosition savePosition = JsonUtility.FromJson<SavePosition>(loadfile);
            playerModel.transform.position = savePosition.position;
            playerModel.transform.rotation = savePosition.rotation;
        }
        else 
        { 
            playerModel.transform.position = Vector3.zero;
            playerModel.transform.rotation = Quaternion.identity;
        }
    }
}

public class SavePosition
{
    public Vector3 position;
    public Quaternion rotation;
}