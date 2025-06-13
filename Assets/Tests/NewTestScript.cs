using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using System.Reflection;

public class InitializationTests
{
    static bool HasMethod(object obj, string methodName)
    {
        if (obj == null) return false;

        System.Type type = obj.GetType();
        MethodInfo method = type.GetMethod(methodName);

        return method != null;
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");

        // Wait for the scene to load
        yield return null;
        while (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().isLoaded)
            yield return null;
    }
    // A Test behaves as an ordinary method
    [Test]
    public void EventManagerInit()
    {
        if (EventManager.Instance == null ||
            !HasMethod(EventManager.Instance, "Subscribe") ||
            !HasMethod(EventManager.Instance, "Unsubscribe") ||
            !HasMethod(EventManager.Instance, "Notify"))
            throw new System.NullReferenceException("Event manager did not instantiate properly");
    }

    [Test]
    public void MusicPlayerInit()
    {
        if (MusicPlayer.Instance == null)
            throw new System.NullReferenceException("Music player did not instantiate properly");
    }

    [Test]
    public void PlayerInit()
    {
        Player player = UnityEngine.Object.FindObjectOfType<Player>();
        if (player == null ||
            !HasMethod(player, "Die") ||
            !HasMethod(player, "Respawn"))
            throw new System.NullReferenceException("Player did not instantiate properly");
    }

    [Test]
    public void ActivatableObjectInit()
    {
        if (UnityEngine.Object.FindObjectOfType<Activateable>() == null)
            throw new System.NullReferenceException("No activatable object instantiated properly");
        foreach (Activateable i in UnityEngine.Object.FindObjectsOfType<Activateable>())
        {

            if (!HasMethod(i, "Activate") ||
                !HasMethod(i, "Deactivate"))
                throw new System.NullReferenceException("No activatable object instantiated properly");
            i.Activate();
            i.Deactivate();
        }
    }

    [Test]
    public void ExitDoorInit()
    {
        if (UnityEngine.Object.FindObjectOfType<ExitDoor>() == null)
            throw new System.NullReferenceException("Exit door did not instantiate properly");
    }

}

