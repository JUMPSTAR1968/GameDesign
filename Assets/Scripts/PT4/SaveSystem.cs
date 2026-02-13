using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] GameObject playerModel;

    // 1. Updated Save to accept 'score'
    public void Save(Vector3 playerPosition, Quaternion playerRotation, int currentScore)
    {
        SavePosition savePosition = new SavePosition
        {
            position = playerPosition,
            rotation = playerRotation,
            score = currentScore // Save the score
        };

        string json = JsonUtility.ToJson(savePosition);
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
        Debug.Log("Saved Score: " + currentScore);
    }

    // 2. Updated Load to RETURN data so MoveAnim can see the score
    public SavePosition Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save.txt"))
        {
            string loadfile = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            SavePosition savePosition = JsonUtility.FromJson<SavePosition>(loadfile);

            // Set Position immediately
            playerModel.transform.position = savePosition.position;
            playerModel.transform.rotation = savePosition.rotation;

            // Return the data so we can extract the score in the other script
            return savePosition;
        }
        else
        {
            // Default setup if no save file exists
            playerModel.transform.position = Vector3.zero;
            playerModel.transform.rotation = Quaternion.identity;
            return null;
        }
    }
}

[System.Serializable]
public class SavePosition
{
    public Vector3 position;
    public Quaternion rotation;
    public int score; // Added Score variable here
}