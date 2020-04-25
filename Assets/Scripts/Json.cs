using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para salvar arquivos
using System.IO;

public class Json : MonoBehaviour {

    private void Start() {
        PlayerData playerData = new PlayerData();
        playerData.position = new Vector3(1, 2, 3);

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "saveFile.json", json);


        string jsonRecovered = File.ReadAllText(Application.dataPath + "saveFile.json");
        PlayerData dataRecover = JsonUtility.FromJson<PlayerData>(json);   
    }
}


public class PlayerData {
    public Vector3 position;
    public int health;
}