using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        Debug.Log("SelectItem() " + item);

        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(102);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(3);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-100);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }          
    }

    public void BuyItem()
    {
        if (player.diamonds >= currentItemCost)
        {
            //award item
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            player.diamonds -= currentItemCost;
            Debug.Log("Purchased" + currentSelectedItem);
            Debug.Log("Remaining gems: " + player.diamonds);
            shopPanel.SetActive(false);
        }
        else
        {
            Debug.Log("You do not have enough gems. Closing Shop");
            shopPanel.SetActive(false);
        }
    }
}
