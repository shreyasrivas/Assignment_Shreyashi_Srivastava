using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour {
    public static Food instance;
    private List<GameObject> foodPrefab = new List<GameObject> ();
    private Dictionary<string, int> foods = new Dictionary<string, int> ();
    private Vector3 startPos = new Vector3 (0f, 0f, 0f);

    public Text streak_text;
    static Dictionary<string, int> streakValue = new Dictionary<string, int> ();
    public Text score_value;
    int totaScore = 0;

    // Start is called before the first frame update
    void Awake () {
        instance = this;
        createJson ();
        SpawnFood (startPos);
    }

    void createJson () {
        string path = Application.streamingAssetsPath + "/dataFile.json";
        string jsonString = File.ReadAllText (path);
        JSONNode jsnode = SimpleJSON.JSON.Parse (jsonString);
        foreach (JSONNode js in jsnode["player"]["food"]) {
            foods.Add (js["color"], js["score"]);
        }
    }

    public void updateScore (string food_color) {
        totaScore = totaScore + (foods[food_color] * streakValue[food_color]);
        score_value.text = "Score: " + totaScore.ToString ();
        PlayerPrefs.SetInt ("TotalScore", totaScore);
        if (PlayerPrefs.GetInt ("HighestScore") < PlayerPrefs.GetInt ("TotalScore")) {
            PlayerPrefs.SetInt ("HighestScore", PlayerPrefs.GetInt ("TotalScore"));
        }
    }

    public void MoveFood (string food_eaten) {
        UpdateStreaks (food_eaten);
        updateScore (food_eaten);
        int spawnPointX = Random.Range (-7, 7);
        int spawnPointY = Random.Range (-5, 5);
        int rand = Random.Range (0, 2);
        foodPrefab[rand].SetActive (true);
        foodPrefab[rand].transform.position = new Vector3 (spawnPointX, spawnPointY, 0f);
    }

    void SpawnFood (Vector3 foodPos) {
        foreach (var food in foods) {
            GameObject obj = Instantiate (Resources.Load ("Food/" + food.Key), foodPos, Quaternion.identity) as GameObject;
            obj.transform.SetParent (transform, true);
            obj.name = food.Key;
            obj.SetActive (false);
            foodPrefab.Add (obj);
        }
        int rand = Random.Range (0, 2);
        foodPrefab[rand].SetActive (true);
    }

    void UpdateStreaks (string food_color) {
        if (streakValue.ContainsKey (food_color)) {
            streakValue[food_color] += 1;
        } else {
            streakValue.Clear ();
            streakValue.Add (food_color, 1);
        };
        streak_text.text = food_color.ToUpper () + " STREAK: " + streakValue[food_color];
    }

}