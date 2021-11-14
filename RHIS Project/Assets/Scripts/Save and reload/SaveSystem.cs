using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer (Player player){
    	BinaryFormatter formatter = new BinaryFormatter();
    	string path = Application.persistentDataPath + "/player.data";
    	FileStream stream = new FileStream(path, FileMode.Create);

    	PlayerData data = new PlayerData(player);

    	formatter.Serialize(stream, data);
    	stream.Close(); 
    }

    public static PlayerData LoadPlayer (){
    	string path = Application.persistentDataPath + "/player.data";
    	if(file.Exist(path)){
    		BinaryFormatter formatter = new BinaryFormatter();
    		FileStream stream = new FileStream(path, FileMode.Open);

    		PlayerData data = formatter.Deserialize(stream) as PlayerData;
    		stream.Close();

    		return data;
    	}
    	else{
    		Debug.LogError("Save file not found in " + path);
    		return null;
    	}
    }
}
