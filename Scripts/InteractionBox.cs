using Godot;
using System;
using System.Collections.Generic;

public partial class InteractionBox : Area2D
{
    private List<Node2D> interactablesInRange = new List<Node2D>();

    private void _on_body_entered(Node2D body) {
        if (body is IInteractable) {
            interactablesInRange.Add(body);

        }
    }

    private void _on_body_exited(Node2D body) {
        if (body is IInteractable) {
            if (interactablesInRange.Contains(body)) {
                interactablesInRange.Remove(body);

            }
        }
    }

    public Node2D find_nearest_interactable() {
        if (interactablesInRange.Count == 1) {
            return interactablesInRange[0];

        } else if (interactablesInRange.Count > 1) {

            (Node2D, float) closest = default;

            for (int i = 0; i < interactablesInRange.Count; i++) {
                Godot.Vector2 itemPos = interactablesInRange[i].GlobalPosition;

                float distance = GlobalPosition.DistanceTo(itemPos);

                if (closest.Item1 is null) {
                    closest = (interactablesInRange[i], distance);
                } else {
                    if (distance <= closest.Item2) {
                        closest = (interactablesInRange[i], distance);
                    }
                }
            }

            return closest.Item1;

        } else {
            return null;
        }
    }

}
