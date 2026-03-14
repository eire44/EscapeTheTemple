using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    public GameObject portal;
    public GameObject welcomePamphlet;
    public GameObject thankYouPamphlet;
    public float fadeDuration = 2f;
    Renderer portalRenderer;
    Collider portalCollider;

    void Start()
    {
        portalRenderer = portal.GetComponent<Renderer>();
        portalCollider = portal.GetComponent<Collider>();
    }

    public void enablePortal()
    {
        StartCoroutine(illuminatePortal());
        changePamphlets();
    }

    IEnumerator illuminatePortal()
    {
        Color startColor = portalRenderer.material.color;
        Color targetColor = Color.white;

        float time = 0f;

        while (time < fadeDuration)
        {
            portalRenderer.material.color = Color.Lerp(startColor, targetColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        portalRenderer.material.color = targetColor;
        portalCollider.isTrigger = true;
    }

    public void goBackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Inicio");
    }

    public void changePamphlets()
    {
        welcomePamphlet.SetActive(false);
        thankYouPamphlet.SetActive(true);
    }
}
