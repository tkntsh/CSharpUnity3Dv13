using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayInfo : MonoBehaviour
{
    public GameObject userInfoText;

    void Start()
    {
        string name = PlayerPrefs.GetString("Name");
        string surname = PlayerPrefs.GetString("Surname");
        int age = PlayerPrefs.GetInt("Age");
        string phone = PlayerPrefs.GetString("Phone");
        string dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        userInfoText.GetComponent<Text>().text = $"Name: {name}\nSurname: {surname}\nAge: {age}\nPhone: {phone}\nInserted On: {dateTime}";
        userInfoText.SetActive(true);
    }
}
