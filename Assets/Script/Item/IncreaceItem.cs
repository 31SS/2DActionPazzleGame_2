using UnityEngine;
//拾うとPlayerを増やせるアイテムの処理
public class IncreaceItem : MonoBehaviour, IPickupable {

    public GameObject originPlayer;
    public void PickedUp()
    {
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
        Invoke( "Increace", 0.5f);
    }

    private void Increace()
    {
        Instantiate(originPlayer, transform.position, Quaternion.identity);
    }
}