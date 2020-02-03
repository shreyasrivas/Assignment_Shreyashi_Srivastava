using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public Text top_score_text;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("TotalScore", 0);
        top_score_text.text += PlayerPrefs.GetInt("HighestScore").ToString();
    }

    public void ShowHome(){
        SceneManager.LoadScene("Gameplay");
    }
}
