using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crypt : MonoBehaviour
{
    //Fonctions d'encryptage (pour les saves)

    public static string Encrypt(string s)
    {
        string clef = "I will be better someday";
        System.Text.StringBuilder output = new System.Text.StringBuilder(s);

        int j = 0;
        for (int i = 0; i < s.Length; i++)
        {
            output[i] = (char)(s[i] + clef[j]);
            j++;
            if (j >= 24)
                j = 0;
        }
        return output.ToString();
    }

    public static string Decrypt(string s)
    {
        string clef = "I will be better someday";
        System.Text.StringBuilder output = new System.Text.StringBuilder(s);

        int j = 0;
        for (int i = 0; i < s.Length; i++)
        {
            output[i] = (char)(s[i] - clef[j]);
            j++;
            if (j >= 24)
                j = 0;
        }
        return output.ToString();
    }

    public static int EncInt(int a)
    {
        return (((a + 29) * 13) + 47) * 7;
    }
    public static int DecInt(int a)
    {
        return (((a / 7) - 47) / 13) - 29;
    }
}
