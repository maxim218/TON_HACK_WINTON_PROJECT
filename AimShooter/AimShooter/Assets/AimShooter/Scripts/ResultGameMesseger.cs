using UnityEngine;

public static class ResultGameMesseger {
    private const string FunctionName = "JsGameResult";
    private const string ErrorTextMessage = "Error in SendGameResult";
    
    public static void SendGameResult(string gameName, int heroScore, int enemyScore) {
        try {
            string message = gameName + "_" + heroScore + "_" + enemyScore;
            Application.ExternalCall(FunctionName, message);
        } catch {
            Debug.LogError(ErrorTextMessage);
        }
    }
}