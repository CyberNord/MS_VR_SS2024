using UnityEngine;

public class MoveBasket : MonoBehaviour
{
    public GameObject basket;
    public Transform newPosition;

    public void MoveBasketToNewPosition()
    {
        if (basket != null && newPosition != null)
        {
            basket.transform.position = newPosition.position;
        }
    }
}
