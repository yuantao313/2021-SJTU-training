using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Next2Main : MonoBehaviour
{
    public void OnLoginButtonClick()
    {
        SceneManager.LoadScene(5);
    }
}
