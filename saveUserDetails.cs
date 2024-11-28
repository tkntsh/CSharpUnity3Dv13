using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class saveUserDetails : MonoBehaviour
{
    // References to the UI elements
    public GameObject nameInput;
    public GameObject surnameInput;
    public GameObject ageInput;
    public GameObject phoneInput;
    public Button saveButton;
    public GameObject errorText;

    public Material material1; // Assign the new material in the Inspector
    public Material material2;
    public Button changeMaterialButton; // Assign the button in the Inspector

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        // Load any previously saved data
        LoadNurseInfo();

        // Add a listener to the save button to save the data
        saveButton.onClick.AddListener(SaveNurseInfo);
    }

    void SaveNurseInfo()
    {
        // Retrieve data from input fields
        string name = nameInput.GetComponent<Text>().text;
        string surname = surnameInput.GetComponent<Text>().text;
        string ageText = ageInput.GetComponent<Text>().text;
        string phone = phoneInput.GetComponent<Text>().text;

        // Check if any fields are empty
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(ageText) || string.IsNullOrEmpty(phone))
        {
            // Set error message active and display it
            errorText.SetActive(true);
            return;
        }

        // Check if age is a valid integer
        int age;
        if (!int.TryParse(ageText, out age))
        {
            errorText.SetActive(true);
            return;
        }

        // Save data using PlayerPrefs
        PlayerPrefs.SetString("NurseName", name);
        PlayerPrefs.SetString("NurseSurname", surname);
        PlayerPrefs.SetInt("NurseAge", age);
        PlayerPrefs.SetString("NursePhone", phone);

        // Ensure the data is saved to disk
        PlayerPrefs.Save();

        Debug.Log("Nurse information saved successfully.");

        SceneManager.LoadScene("ObjectInteraction");
        errorText.SetActive(false);
    }

    //switch from leap motion scene to main menu
    public void mainMenuButton()
    {
        SceneManager.LoadScene("loginScene");
    }

    public void LoadNurseInfo()
    {
        // Load data from PlayerPrefs if it exists
        if (PlayerPrefs.HasKey("NurseName"))
        {
            nameInput.GetComponent<Text>().text = PlayerPrefs.GetString("NurseName");
            surnameInput.GetComponent<Text>().text = PlayerPrefs.GetString("NurseSurname");
            ageInput.GetComponent<Text>().text = PlayerPrefs.GetInt("NurseAge").ToString();
            phoneInput.GetComponent<Text>().text = PlayerPrefs.GetString("NursePhone");
        }
    }
    //
    public void ChangeMaterial1OnClick()
    {
        // Change the material of the GameObject
        objectRenderer.material = material1;

        Debug.Log("Material1 changed successfully.");
    }
    //
    public void ChangeMaterial2OnClick()
    {
        // Change the material of the GameObject
        objectRenderer.material = material2;

        Debug.Log("Material2 changed successfully.");
    }
}
