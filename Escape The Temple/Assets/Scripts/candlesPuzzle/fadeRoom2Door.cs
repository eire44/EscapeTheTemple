using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeRoom2Door : MonoBehaviour
{
    public float fadeDuration = 2f;

    Material materialInstance;
    float elapsedTime = 0f;
    bool fading = false;

    void Start()
    {
        materialInstance = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (fading)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            Color color = materialInstance.color;
            color.a = alpha;
            materialInstance.color = color;

            if (elapsedTime >= fadeDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void StartFade()
    {
        fading = true;
        elapsedTime = 0f;
    }
}
