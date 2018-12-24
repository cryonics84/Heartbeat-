using UnityEngine;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class HeartBeatController : MonoBehaviour
{
    bool canToggleHeartMonitor = true;

    const int WM_SETTEXT = 0X000C;
    const uint WM_KEYDOWN = 0x100;
    const uint WM_KEYUP = 0x101;
    const uint WM_CHAR = 0x102;

    IntPtr hWnd; // main window
    //IntPtr hWndEdit; // child window
    IntPtr unityhWnd;

    public string windowClass = "Notepad";

    public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
    public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
    public const int VK_LCONTROL = 0xA2; //Left Control key code
    public const int Space = 0x20; //Space key code

    [DllImport("user32.dll", SetLastError = true)]
    static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

    [DllImport("User32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    //static extern int SetForegroundWindow(IntPtr point);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowEx(IntPtr parentWindow, IntPtr previousChildWindow, string windowClass, string windowTitle);

    [DllImport("user32.dll")]
    static extern System.IntPtr GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(System.IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string className, string windowName);
    
    // Start is called before the first frame update
    void Start()
    {
        hWnd = FindWindow("QWidget", null);
        //hWndEdit = FindWindowEx(hWnd, IntPtr.Zero, windowChildClass, "");
        unityhWnd = GetActiveWindow();
        
    }

    public void TriggerBeat()
    {
        if (!canToggleHeartMonitor) return;
        print("Calling heartbeat...");
        canToggleHeartMonitor = false;
        SetForegroundWindow(hWnd);

        PressKeys();

        StartCoroutine(FocusUnity());
    }


    public static void PressKeys()
    {
        // Hold Control down and press A
        keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
        keybd_event(Space, 0, KEYEVENTF_EXTENDEDKEY, 0);
        keybd_event(Space, 0, KEYEVENTF_KEYUP, 0);
        keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);
    }

    IEnumerator FocusUnity()
    {
        yield return new WaitForSeconds(0.5f);
        SetForegroundWindow(unityhWnd);
        canToggleHeartMonitor = true;
    }
}
