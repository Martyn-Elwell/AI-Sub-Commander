using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public void Outline(bool active);
}

public enum InteractionType
{
    NONE = 0,
    BREACH = 1,
    COVER = 2,
    SABOTAGE = 3,
}
