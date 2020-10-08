using UnityEngine.UI;
using UnityEngine;

public class AccountLogin : MonoBehaviour
{
    #region Variables

    public Text _ErrorText;
    public GameObject Pet, LoginPanel, CreationPanel, GamePanel, ErrorPanel;
    public InputField _UN, _UP;
    string _Name, _Pass;

    #endregion

    #region Functions

    public void ToggleCreationPanel()
    {
        if (ErrorPanel.activeInHierarchy == true)
        {
            ErrorPanel.SetActive(false);
        }
        if (CreationPanel.activeInHierarchy == false)
        {
            CreationPanel.SetActive(true);
        }
        else
        {
            CreationPanel.SetActive(false);
        }
    }
    public void AtemptLogin()
    {
        if (ErrorPanel.activeInHierarchy == true)
        {
            ErrorPanel.SetActive(false);
        }
        if (_UN.text.Length != 0 && _UP.text.Length != 0 && _UN.text.Length <= 18 && _UP.text.Length <= 18)
        {
            if (ErrorPanel.activeInHierarchy == true)
            {
                ErrorPanel.SetActive(false);
            }
            _Name = _UN.text;
            _Pass = _UP.text;
        }
        else
        {
            ErrorPanel.SetActive(true);
            _ErrorText.text = "Missing log in credentials!";
            return;
        }

        Account AccountWebProxy = WebServer.AccountLogIn(_Name, _Pass);

        if (AccountWebProxy != null)
        {
            if (ErrorPanel.activeInHierarchy == true)
            {
                ErrorPanel.SetActive(false);
            }

            gameObject.GetComponent<AccountContainer>().HoldAccount(AccountWebProxy);
            GamePanel.SetActive(true);
            Pet.GetComponent<PetMonster>().GetSoul();
            LoginPanel.SetActive(false);
        }
        else
        {
            ErrorPanel.SetActive(true);
            _ErrorText.text = "wrong account name or password";
        }
    }
    #endregion
}