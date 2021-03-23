using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiviTriggeri : MonoBehaviour {
    PlayerController pController;
    FadeController fadeController;
    Rigidbody2D rockRB;
    public GameObject rock, newRock;
    public Transform dropPosition, newPlayerPosition;
    bool playerTouched, rockFalling;

    void Start() {
        fadeController = FindObjectOfType<FadeController>();
        pController = FindObjectOfType<PlayerController>();
        rockRB = rock.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!playerTouched) {
            fadeController.StartCoroutine("FadeUp");
            rockRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            pController.playerRB.velocity = new Vector2(0f, 0f);
            pController.enabled = false;
            playerTouched = true;
        }
    }

    void Update() {
        if (fadeController.fade.a >= 1f && playerTouched) {
            pController.enabled = true;
            pController.playerRB.position = newPlayerPosition.position;
            rock.transform.position = dropPosition.position;
            rockRB.gravityScale = 1f;
            rockFalling = true;
        }
        if (rockFalling) StartCoroutine(NewRockPositionAfterFalling());
    }

    IEnumerator NewRockPositionAfterFalling() {
        yield return new WaitForSeconds(2);
        rock.SetActive(false);
        newRock.SetActive(true);
        gameObject.SetActive(false);
    }
}
