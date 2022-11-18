using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemoSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject player;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = player.transform.position;
        text.text = "";
    }

    public void ButtonPress()
    {
        StartCoroutine(ButtonAction());
    }

    private IEnumerator ButtonAction()
    {
        text.text = "3";
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        text.text = "";
        player.transform.position = initialPosition;
    }
}
