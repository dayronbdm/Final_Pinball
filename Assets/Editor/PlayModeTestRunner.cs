using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Unity.AI.Assistant.PlayModeTest
{
    [InitializeOnLoad]
    internal static class PlayModeTestRunner
    {
        private const string StateKey = "PlayModeTest.State";
        private const string ResultKey = "PlayModeTest.Result";
        private const string ScriptPathKey = "PlayModeTest.ScriptPath";

        static PlayModeTestRunner()
        {
            string state = SessionState.GetString(StateKey, "Idle");
            if (state == "WaitingForCompile")
            {
                EditorApplication.delayCall += () => {
                    SessionState.SetString(StateKey, "EnteringPlayMode");
                    EditorApplication.isPlaying = true;
                };
            }
            else if (state == "InPlayMode")
            {
                if (EditorApplication.isPlaying) EditorApplication.update += WaitFramesThenRun;
            }
            else if (EditorApplication.isPlaying)
            {
                 SessionState.SetString(StateKey, "InPlayMode");
                 EditorApplication.update += WaitFramesThenRun;
            }
        }

        private static int _frameCount = 0;
        private static bool _setupDone = false;
        private static double _testStartTime = 0;

        private static void WaitFramesThenRun()
        {
            _frameCount++;
            if (_frameCount < 15) return;

            if (!_setupDone) {
                _setupDone = true;
                _testStartTime = EditorApplication.timeSinceStartup;
                Debug.Log("[Test] Setup: Checking for ball");
                var ball = Object.FindAnyObjectByType<Ball>();
                if (ball != null) Debug.Log("[Test] Ball found at " + ball.transform.position);
                else Debug.Log("[Test] Ball NOT found");
                return;
            }

            float elapsed = (float)(EditorApplication.timeSinceStartup - _testStartTime);
            if (elapsed > 1.0f && elapsed < 1.1f) {
                var keyboard = InputSystem.GetDevice<Keyboard>();
                if (keyboard != null) {
                    using (StateEvent.From(keyboard, out var eventPtr)) {
                        keyboard[Key.Space].WriteValueIntoEvent(1f, eventPtr);
                        InputSystem.QueueEvent(eventPtr);
                    }
                    Debug.Log("[Test] Space pressed");
                }
            }

            if (elapsed > 3.0f) {
                var ball = Object.FindAnyObjectByType<Ball>();
                if (ball != null) Debug.Log("[Test] Final Ball velocity: " + ball.GetComponent<Rigidbody>().linearVelocity);
                FinishTest();
            }
        }

        private static void FinishTest()
        {
            EditorApplication.update -= WaitFramesThenRun;
            SessionState.SetString(ResultKey, "Done");
            SessionState.SetString(StateKey, "Done");
            EditorApplication.isPlaying = false;
        }
    }
}