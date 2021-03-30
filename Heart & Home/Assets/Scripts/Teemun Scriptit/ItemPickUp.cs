using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
    InventoryManager invManager;
    TextOnPickUp pickUpText;
    AudioSource audioSource;
    GameObject player;
    public AudioClip pickUpSound;
    public ItemDataScriptable itemToAdd;
    public string infoText;
    //public GameObject placeHolderText;
    void Start() {
        invManager = FindObjectOfType<InventoryManager>();
        pickUpText = FindObjectOfType<TextOnPickUp>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //placeHolderText.SetActive(true);
        if(collision.gameObject.tag == "Player") {
            pickUpText.NewText(infoText);
            audioSource.PlayOneShot(pickUpSound);

            invManager.AddItemInInventory(invManager.personalInvIngredients, itemToAdd);
            gameObject.SetActive(false);
            print("Added " + itemToAdd.item);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) { //Drop
        if (collision.gameObject.tag == "Player") {
            pickUpText.NewText(infoText);
            audioSource.PlayOneShot(pickUpSound);

            invManager.AddItemInInventory(invManager.personalInvIngredients, itemToAdd);
            gameObject.SetActive(false);
            print("Added " + itemToAdd.item);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        //placeHolderText.SetActive(false);
    }

    //void OnTriggerStay2D(Collider2D collision) {
    //    if (Input.GetKeyDown(KeyCode.E)) {
            
    //        invManager.AddItemInInventory(invManager.personalInvIngredients, itemToAdd);
    //        gameObject.SetActive(false);
    //        print("Added " + itemToAdd.item);
    //    }
    //}
}
