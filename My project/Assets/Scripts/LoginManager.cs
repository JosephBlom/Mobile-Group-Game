using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.IO;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;

    [SerializeField] Player player;

    public string username;
    public string password;

    public void CreateAccount()
    {
        if (usernameField.text != "" || passwordField.text != "")
        {
            username = usernameField.text;
            password = passwordField.text;

            string path = Application.persistentDataPath + "/" + username + ".game";
            if (File.Exists(path))
            {
                Debug.Log("Account Already Exists Try Login Instead.");
            }
            else
            {
                SaveSystem.CreateAccount(player, username, password);
                FindObjectOfType<Animator>().SetBool("IsOpen", true);
                FindObjectOfType<Player>().unlockedWorlds[0] = "Mercury";
                FindObjectOfType<Player>().unlockedLevels[0] = "Mercury 1";
            }
        }
        else
        {
            Debug.Log("One Or Both Of The Input Fields Are Empty Please Fill In The Information.");
        }
    }

    public void Login()
    {
        if(usernameField.text != "" || passwordField.text != "")
        {
            username = usernameField.text;
            password = passwordField.text;

            string path = Application.persistentDataPath + "/" + username + ".game";
            if (File.Exists(path))
            {
                PlayerData data = SaveSystem.LoadPlayer(username, password);
                if (data.password != password)
                {
                    Debug.Log("You May Have Typed Your Username Or Password Incorrectly Try Again. You May Also Not Have An Account Yet Maybe Try Create Account.");
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
