using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] GameObject playerModel;

    // 1. Updated Save to accept 'spherePosition'
    public void Save(Vector3 playerPosition, Quaternion playerRotation, int currentScore, Vector3 spherePosition)
    {
        SavePosition savePosition = new SavePosition
        {
            position = playerPosition,
            rotation = playerRotation,
            score = currentScore,
            spherePos = spherePosition // Save the sphere's spot
        };

        string json = JsonUtility.ToJson(savePosition);
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
        Debug.Log("Saved Game (Player + Sphere + Score)");
    }

    public SavePosition Load()
    {
        string path = Application.persistentDataPath + "/save.txt";
        if (File.Exists(path))
        {
            string loadfile = File.ReadAllText(path);
            SavePosition savePosition = JsonUtility.FromJson<SavePosition>(loadfile);

            // Apply Player Data
            playerModel.transform.position = savePosition.position;
            playerModel.transform.rotation = savePosition.rotation;

            return savePosition;
        }
        else
        {
            // Default Start
            playerModel.transform.position = Vector3.zero;
            playerModel.transform.rotation = Quaternion.identity;
            return null;
        }
    }

    // New Function: Delete Save
    public void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/save.txt";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save File Deleted!");
        }
    }
}

[System.Serializable]
public class SavePosition
{
    public Vector3 position;
    public Quaternion rotation;
    public int score;
    public Vector3 spherePos; // Added Sphere Position
}