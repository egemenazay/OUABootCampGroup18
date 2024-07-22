using UnityEngine;

public class DrawerController : MonoBehaviour
{
    public Transform drawer;  // Çekmece objesi
    public Transform player;  // Oyuncu objesi
    public float openDistance = 0.5f;  // Çekmece açýlma mesafesi
    public float openSpeed = 2f;  // Çekmece açýlma hýzý
    public float interactionDistance = 2f;  // Etkileþim mesafesi

    private bool isOpen = false;  // Çekmece açýk mý?
    private Vector3 closedPosition;  // Çekmece kapalý pozisyonu
    private Vector3 openPosition;  // Çekmece açýk pozisyonu

    void Start()
    {
        closedPosition = drawer.localPosition;
        openPosition = closedPosition + new Vector3(0, 0, openDistance);
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;  // Çekmece durumunu deðiþtir
        }

        if (isOpen)
        {
            drawer.localPosition = Vector3.Lerp(drawer.localPosition, openPosition, Time.deltaTime * openSpeed);
        }
        else
        {
            drawer.localPosition = Vector3.Lerp(drawer.localPosition, closedPosition, Time.deltaTime * openSpeed);
        }
    }
}
