using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpaceController : MonoBehaviour
{
    public Transform player; // ќб'Їкт камери або гравц€, €кого ми в≥дстежуЇмо
    public Transform terrain; // Ћандшафт
    public Transform water; // ¬ода

    // ¬становлюЇмо межу, п≥сл€ €коњ об'Їкти зм≥щуютьс€
    public float boundary = 5f;

    void Update()
    {
        CheckAndReposition(terrain);
        CheckAndReposition(water);
    }

    // ћетод перев≥р€Ї, чи вийшов об'Їкт за межу, ≥ перем≥щуЇ його
    void CheckAndReposition(Transform obj)
    {
        Vector3 offset = Vector3.zero;

        // якщо гравець перем≥стивс€ за межу по ос≥ X
        if (player.position.x > boundary)
            offset.x -= boundary * 2;
        else if (player.position.x < -boundary)
            offset.x += boundary * 2;

        // якщо гравець перем≥стивс€ за межу по ос≥ Z
        if (player.position.z > boundary)
            offset.z -= boundary * 2;
        else if (player.position.z < -boundary)
            offset.z += boundary * 2;

        // якщо Ї зм≥щенн€, перем≥щуЇмо об'Їкт
        if (offset != Vector3.zero)
            obj.position += offset;
    }
}
