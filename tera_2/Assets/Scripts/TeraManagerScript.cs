using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 	ゲーム全体を管理するスクリプト
/// 	デバックフラグなどを記憶させておく
/// </summary>
public class TeraManagerScript : MonoBehaviour {

	// ゲーム中に存在するシーンの定数
	public enum SceneNameType {
		Title = 1,			// タイトル
		DataSelect = 2,		// データセレクト
		Lobby = 3,			// ロビー
		StageSelect = 4,	// ステージセレクト
		ModOfTetlapod = 5,	// テトラポッド強化画面
		Liblary = 6,		// 図鑑
		Game = 7,			// ゲームプレイ
		DayEnd = 8,			// 一日終了
	}
	// ゲーム中に存在するシーンの文字列
	public static Dictionary<SceneNameType, string> SceneNameDic = new Dictionary<SceneNameType, string>{
		{ TeraManagerScript.SceneNameType.Title, "TitleScene" },
		{ TeraManagerScript.SceneNameType.DataSelect, "TitleScene" },
		{ TeraManagerScript.SceneNameType.Lobby, "TitleScene" },
		{ TeraManagerScript.SceneNameType.StageSelect, "TitleScene" },
		{ TeraManagerScript.SceneNameType.ModOfTetlapod, "TitleScene" },
		{ TeraManagerScript.SceneNameType.Liblary, "TitleScene" },
		{ TeraManagerScript.SceneNameType.Game, "TitleScene" },
		{ TeraManagerScript.SceneNameType.DayEnd, "TitleScene" },
	};

	// デバックツールを表示するかどうか
	[SerializeField]
	private bool degug_flg = false;

	// DontDestroy系のアイテム
	private GameObject dont_destroy_canvas = null;
	private GameObject loading_prefab = null;

	// シーン遷移用のアイテム
	private Scene previous;

	// シングルトンパターン
	private static TeraManagerScript instance = null;
	public static TeraManagerScript Instance {
		get{
			if( instance == null ){
					instance = ( TeraManagerScript ) FindObjectOfType( typeof( TeraManagerScript ) );
			}

			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		// ゲームヒエラルキー上にこの Manager は常に一つ
		if( instance != null ){
			Destroy( this.gameObject );
			return;
		}

		// dontdestroyonload化
		DontDestroyOnLoad( this.gameObject );

		// dontdestroyonload 用の canvas を呼び出し
		GameObject prefab = ( GameObject )Resources.Load("Prefabs/DontDestroyCanvas");
		dont_destroy_canvas = Instantiate( prefab, Vector3.zero, Quaternion.identity );
		dont_destroy_canvas.name = "DontDestroyCanvas";
		DontDestroyOnLoad( dont_destroy_canvas );

		// loading 画面の初期化
		loading_prefab = GameObject.Find("DontDestroyCanvas/Load");
		loading_prefab.SetActive( false );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SceneChange( TeraManagerScript.SceneNameType type ){
		// 現在のシーンを記憶させる
		this.previous = SceneManager.GetActiveScene();

		// ロード画面を呼びだす
		this.CallLoading( true );
	}

	/// <summary>
	///		ロード画面の表示/非表示
	/// </summary>
	/// <param name="display_flg"> true: 表示 / false: 非表示 </param>
	public void CallLoading( bool display_flg ){
		loading_prefab.SetActive( display_flg );
	}
}
