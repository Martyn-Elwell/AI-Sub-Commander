using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public void Outline(bool active);
}

public enum InteractionType
{
    BREACH = 0,
    COVER = 1,
    SABOTAGE = 2,
}
