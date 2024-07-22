using UnityEngine;

public class CabinetDoorController : MonoBehaviour
{
    public Transform door;  // Kapak objesi
    public Transform player;  // Oyuncu objesi
    public float openAngle = 90f;  // Kapak açýlma açýsý
    public float openSpeed = 2f;  // Kapak açýlma hýzý
    public float interactionDistance = 2f;  // Etkileþim mesafesi

    private bool isOpen = false;  // Kapak açýk mý?
    private Vector3 closedRotation;  // Kapak kapalý rotasyonu
    private Vector3 openRotation;  // Kapak açýk rotasyonu

    void Start()
    {
        closedRotation = door.localEulerAngles;
        openRotation = closedRotation + new Vector3(0, openAngle, 0);
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;  // Kapak durumunu deðiþtir
        }

        if (isOpen)
        {
            door.localEulerAngles = Vector3.Lerp(door.localEulerAngles, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            door.localEulerAngles = Vector3.Lerp(door.localEulerAngles, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
