using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] TextMeshProUGUI usernameText;

    [SerializeField] Player player;

    public string username;
    public string password;

    string sceneName;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void CreateAccount()
    {
        if (usernameField.text != "" || passwordField.text != "")
        {
            username = usernameField.text;
            password = passwordField.text;

            player.unlockedWorlds.Add("Mercury");
            player.unlockedLevels.Add("Mercury 1");
            player.password = password;
            player.username = username;
            string path = Application.persistentDataPath + "/" + username + ".game";
            if (File.Exists(path))
            {
                usernameText.text = "Account Already Exists Try Login Instead.";
            }
            else
            {
                SaveSystem.SavePlayer(player, username);
                FindObjectOfType<Animator>().SetBool("IsOpen", true);
                GameObject saveIndicator = GameObject.Find("saveIndicator");
                saveIndicator.GetComponent<Image>().color = Color.blue;
            }
        }
        else
        {
            Debug.Log("One Or Both Of The Input Fields Are Empty Please Fill In The Information.");
        }
    }

    public void Login()
    {
        if(usernameField.text != "" && passwordField.text != "")
        {
            username = usernameField.text;
            password = passwordField.text;

            string path = Application.persistentDataPath + "/" + username + ".game";
            if (File.Exists(path))
            {
                PlayerData data = SaveSystem.LoadPlayer(username, password);
                if (data.password != password)
                {
                    usernameText.text = "You May Have Typed Your Username Or Password Incorrectly Try Again. You May Also Not Have An Account Yet Maybe Try Create Account.";
                }
                else
                {
                    SceneManager.LoadScene("LevelSelect");
                }
            }
        }
        else
        {
            Debug.Log("One Or Both Of The Input Fields Are Empty Please Fill In The Information.");
        }
    }
}
