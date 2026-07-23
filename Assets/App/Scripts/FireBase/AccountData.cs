using System.Collections.Generic;
using UnityEngine;

public class AccountData : MonoBehaviour
{
    private static string usernamePasswordCharacters =
        "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789!@#$%^&*()-_=+[]{}|;:'\",.<>/?`~";

    public static string usableCharacters =
        "A-Z, a-z, 0-9, !@#$%^&*()-_=+[]{}|;:'\",.<>/?`~";

    public static bool isUsable(char lastCharacter)
    {
        foreach (char character in usernamePasswordCharacters)
        {
            if (character == lastCharacter)
            {
                return true;
            }
        }

        return false;
    }

}



