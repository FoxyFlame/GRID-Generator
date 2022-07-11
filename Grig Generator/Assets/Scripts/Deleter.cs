using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deleter : MonoBehaviour
{
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(EndDeleteProcess);
    }

    void EndDeleteProcess()
    {
        Item[] items = GameObject.FindObjectsOfType<Item>();

        foreach(var item in items)
        {
            item.GetComponent<Item>().StartDeletePocess();
        }
        DeleteAll();
    }

    public void DeleteAll()
    {
        GameObject[] toDelete;
        toDelete = GameObject.FindGameObjectsWithTag("ToDelete");

        foreach (var item in toDelete)
        {
            item.transform.position += Vector3.forward;
            Destroy(item, 0.1f);
        }
    }

}
