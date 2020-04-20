using Photon.Pun;
using UnityEngine;

public class ItemLoot : MonoBehaviour
{
    [SerializeField]
    Items item;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    int amount;
    [SerializeField]
    KeyCode key = KeyCode.E;

    public GameObject player;
    SpriteRenderer spritRenderer;

    private bool isInRange;

    private void Start()
    {
        spritRenderer = GetComponent<SpriteRenderer>();
        spritRenderer.sprite = item.Icon;
    }

    private void OnValidate()
    {
        spritRenderer = GetComponent<SpriteRenderer>();
        spritRenderer.sprite = item.Icon;
    }

    private void Update()
    {

        if (player == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            PhotonView PV;
            foreach (var player in players)
            {
                PV = player.GetPhotonView();
                if (PV.IsMine)
                {
                    this.player = player;
                }
            }
        }

        if (inventory == null)
            inventory = FindObjectOfType<Character>().Inventory;

        if (isInRange && Input.GetKeyDown(key))
        {
            if (item != null)
            {
                Items itemCopy = item.GetCopy();
                if (inventory.AddItem(itemCopy))
                {
                    amount--;
                    if (amount == 0)
                        Destroy(gameObject);
                }
                else
                {
                    itemCopy.Destroy();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log('e');
        PhotonView PVC = collision.gameObject.GetPhotonView();
        if (collision.gameObject == player && PVC.IsMine)
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log('x');
        PhotonView PVC = collision.gameObject.GetPhotonView();
        if (collision.gameObject == player && PVC.IsMine)
            isInRange = false;
    }
}
