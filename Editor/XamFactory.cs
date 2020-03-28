using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Xam.Editor
{
	public class XamFactory : EditorWindow
	{
		public static GameManager CreateGameManager( string name )
		{
			GameObject gameManager = new GameObject( name, typeof( GameManager ) );
			CreatePrefabAttachment<Level.LevelLoader>( gameManager );
			CreatePrefabAttachment<Gameplay.TimeManager>( gameManager );

			Audio.AudioManager audioManager = CreateAudioManager( "AudioManager" );
			audioManager.transform.SetParent( gameManager.transform );

			Initialization.TransitionController transitionController = CreatePrefabAttachment<Initialization.TransitionController>( gameManager );
			CreateFadeTransition( transitionController.gameObject );

			return gameManager.GetComponent<GameManager>();
		}

		public static Initialization.LevelInitializer CreateLevelInitializer( string name )
		{
			GameObject levelInitializer = new GameObject( name, typeof( Initialization.LevelInitializer ) );
			CreatePrefabAttachment<Initialization.DelayInitializer>( levelInitializer );

			GameManager gameManagerPrefab = XamEditorUtilities.LoadAssetReferencing<GameManager>();
			if ( gameManagerPrefab != null )
			{
				SerializedObject levelInitializerSerialObj = new SerializedObject( levelInitializer.GetComponent<Initialization.LevelInitializer>() );
				{
					SerializedProperty gameManagerPrefabProperty = levelInitializerSerialObj.FindProperty( "m_gameManagerPrefab" );
					gameManagerPrefabProperty.objectReferenceValue = gameManagerPrefab;
				}
				levelInitializerSerialObj.ApplyModifiedPropertiesWithoutUndo();
			}

			return levelInitializer.GetComponent<Initialization.LevelInitializer>();
		}

		public static T CreatePrefabAttachment<T>( GameObject prefabParent, string explicitName = null ) where T : Component
		{
			string name = explicitName ?? typeof( T ).Name;
			GameObject newAttachment = new GameObject( name, typeof( T ) );
			newAttachment.transform.SetParent( prefabParent.transform );

			return newAttachment.GetComponent<T>();
		}

		private static Audio.AudioManager CreateAudioManager( string name )
		{
			GameObject audioManager = new GameObject( name, typeof( Audio.AudioManager ) );
			AudioSource sfxSource = CreatePrefabAttachment<AudioSource>( audioManager, "SFXSource" );
			{
				sfxSource.loop = false;
				sfxSource.playOnAwake = false;
			}
			AudioSource musicSource = CreatePrefabAttachment<AudioSource>( audioManager, "MusicSource" );
			{
				musicSource.loop = true;
				musicSource.playOnAwake = true;
			}

			SerializedObject audioManagerSerialObj = new SerializedObject( audioManager.GetComponent<Audio.AudioManager>() );
			{
				SerializedProperty sfxSourceProperty = audioManagerSerialObj.FindProperty( "m_sfxSource" );
				sfxSourceProperty.objectReferenceValue = sfxSource;
				SerializedProperty musicSourceProperty = audioManagerSerialObj.FindProperty( "m_musicSource" );
				musicSourceProperty.objectReferenceValue = musicSource;
			}
			audioManagerSerialObj.ApplyModifiedPropertiesWithoutUndo();

			return audioManager.GetComponent<Audio.AudioManager>();
		}

		private static Initialization.FadeTransition CreateFadeTransition( GameObject prefabParent )
		{
			Initialization.FadeTransition fadeTransition = CreatePrefabAttachment<Initialization.FadeTransition>( prefabParent );

			Canvas transitionCanvas = fadeTransition.gameObject.AddComponent<Canvas>();
			transitionCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

			Image fadeElement = CreatePrefabAttachment<Image>( fadeTransition.gameObject, "FadeElement" );
			fadeElement.rectTransform.localPosition = Vector3.zero;
			fadeElement.rectTransform.anchorMin = Vector2.zero;
			fadeElement.rectTransform.anchorMax = Vector2.one;

			return fadeTransition;
		}
	}
}