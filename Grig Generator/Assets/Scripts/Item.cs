using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Color redColor;
    [SerializeField] Color greenColor;
    [SerializeField] Color blueColor;

    public GameObject[] items;

    public string myTag;

    ColorItemGenerator colorItemGenerator;
    Deleter deleter;

    void Start()
    {
        colorItemGenerator = FindObjectOfType<ColorItemGenerator>();
        deleter = FindObjectOfType<Deleter>();
        ChooseRightColor();
        myTag = transform.tag;
    }

    public void StartDeletePocess()
    {
        FindSameItemAsYou();
        PrepareToDelete();
        myTag = transform.tag;
        colorItemGenerator.needReset = true;
    }

    void ChooseRightColor()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        string getColor = "";

        switch (Random.Range(0, 3))
        {
            case 0:
                renderer.color = redColor;
                getColor = "RED";
                gameObject.tag = getColor;
                break;

            case 1:
                renderer.color = greenColor;
                getColor = "GREEN";
                gameObject.tag = getColor;
                break;

            case 2:
                renderer.color = blueColor;
                getColor = "BLUE";
                gameObject.tag = getColor;
                break;
        }

        gameObject.name = $"Item:{getColor} {transform.position.x} {transform.position.y}";
    }

    void FindSameItemAsYou()
    {
        items = GameObject.FindGameObjectsWithTag(myTag);
    }

    void PrepareToDelete()
    {
        foreach (var item in items)
        {
            Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 itemPosition = new Vector2(item.transform.position.x, item.transform.position.y);


            string itemTag = item.GetComponent<Item>().myTag;

            if (myPosition + Vector2.left == itemPosition && itemTag == myTag)
            {
                item.tag = "ToDelete";
            }

            if (myPosition - Vector2.left == itemPosition && itemTag == myTag)
            {
                item.tag = "ToDelete";
            }

            if (myPosition + Vector2.up == itemPosition && itemTag == myTag)
            {
                item.tag = "ToDelete";
            }

            if (myPosition - Vector2.up == itemPosition && itemTag == myTag)
            {
                item.tag = "ToDelete";
            }
        }

        deleter.DeleteAll(); ;
    }

    void OnTriggerExit(Collider other)
    {
        if(!other.GetComponent<Tile>().isBlocked)
        {
            other.GetComponent<Tile>().isPlaceable = true;
        }
    }
}
