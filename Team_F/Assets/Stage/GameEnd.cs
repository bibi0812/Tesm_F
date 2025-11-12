using UnityEngine;





public class GameEnd : MonoBehaviour

{

    // ボタンから呼び出す関数

    public void EndGame()

    {

        // エディタでの動作チェック用

        #if UNITY_EDITOR

             UnityEditor.EditorApplication.isPlaying = false;

        #else

             // ビルド後（実行ファイル時）の終了処理

               Application.Quit();

        #endif

    }

}