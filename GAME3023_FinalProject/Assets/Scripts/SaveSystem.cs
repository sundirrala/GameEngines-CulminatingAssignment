using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;



    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }


    public void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavePlayerData data = new SavePlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
      
    }

    public SavePlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePlayerData data = formatter.Deserialize(stream) as SavePlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public void EvtSavePlayer()
    {
        Debug.Log("Saving");
        SavePlayer(player);
    }

    public void EvtLoadPlayer()
    {
        Debug.Log("Loading...");
        SavePlayerData data = LoadPlayer();

        Vector2 pos;
        pos.x = data.pos.x;
        pos.y = data.pos.y;
        player.transform.position = pos;

        SceneManager.LoadScene(data.activeSceneName);

        Debug.Log("The player's position is " + pos);
    }
}
