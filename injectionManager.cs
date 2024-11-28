using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class injectionManager : MonoBehaviour
{
    //game objects for application
    public Color color1;
    public Color color2;
    public ParticleSystem particleSystem;
    public float depthStart = 0f;
    public float depthTrigger1 = 3f; 
    public float depthTrigger2 = 6f; 
    public AudioSource audioSound;
    private bool isInsertedCorrectly = false;
    private bool isInsertedTooDeep = false;
    private Vector3 startPosition;
    public GameObject arm;
    private Material originalMat;
    public GameObject userInfoText;

    void Start()
    {
        //get the start position of the needle before insertion
        startPosition = transform.position;
        originalMat = GetComponent<Renderer>().material;
    }

    //checking if object hit outside of arm
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == arm)
        {
            startPosition = transform.position;
            /*if (insertionSound != null)
            {
                insertionSound.Play();
            }*/
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }
    }
    //method to change colours referring to depth
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == arm)
        {
            //calc depth from start position to current position of needle
            depthStart = Vector3.Distance(startPosition, transform.position) * 100;
            //check colour according to position of needle
            if(depthStart >= depthTrigger1 && !isInsertedCorrectly)
            {
                isInsertedCorrectly = true;
                changeArmColor(color1);
                DisplayUserInfo();
            }
            if(depthStart >= depthTrigger2 && !isInsertedTooDeep)
            {
                isInsertedTooDeep = true;
                changeArmColor(color2);

                /*if (audioSound != null)
                {
                    audioSound.Play();
                }*/
            }
        }
    }
    //method to check if needle is no longer touching arm
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == arm)
        {
            //reseting all objects to default
            isInsertedCorrectly = false;
            isInsertedTooDeep = false;
            depthStart = 0f;
            arm.GetComponent<Renderer>().material = originalMat;
            //stopping particle system
            if(particleSystem != null)
            {
                particleSystem.Stop();

            }
            /*if(audioSound != null)
            {
                audioSound.Stop();
            }*/
        }
    }

    //changing colour of arm when needle touches
    private void changeArmColor(Color color1)
    {
        Renderer armRenderer = arm.GetComponent<Renderer>();
        if (armRenderer != null)
        {
            armRenderer.material.color = color1;
        }
    }
    //displaying user info on canvas
    public void DisplayUserInfo()
    {
        //retrieving user info to print
        string name = PlayerPrefs.GetString("Name");
        string surname = PlayerPrefs.GetString("Surname");
        int age = PlayerPrefs.GetInt("Age");
        string phone = PlayerPrefs.GetString("Phone");
        string dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        //printing user info
        userInfoText.GetComponent<Text>().text = $"Name: {name}\nSurname: {surname}\nAge: {age}\nPhone: {phone}\nInserted On: {dateTime}";
        userInfoText.SetActive(true);
    }

    public void switchToMenu()
    {
        SceneManager.LoadScene("loginScene");
    }
}
