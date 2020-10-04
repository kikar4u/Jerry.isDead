using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Action", menuName = "Action")]
public class Action : ScriptableObject
{
    public InventaireHandler.AlgoActionEnum actionName;
    public enum ActionType {Direction, Action};
    public ActionType actionType;
    public Sprite actionActivated;
    public Sprite actionDeactivated;
    public Sprite actionEmpty;
}
