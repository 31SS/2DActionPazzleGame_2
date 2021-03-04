using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour , IPickupable
{
    public AudioClip getCoin;

    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
