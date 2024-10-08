using System;
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay;
using UnityEngine;

namespace Source.Features.User
{
    public class UserController : BaseController
    {
        private const string USER_DATA_KEY = "userdata";
        public UserModel UserModel { get; private set; }

        public override UniTask Initialize()
        {
            LoadUserData();
            
            UserModel.OnModelUpdated += SaveUserData;
            GameplayController.OnStartGame += OnStartGame;
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            UserModel.OnModelUpdated -= SaveUserData;
            GameplayController.OnStartGame -= OnStartGame;
            base.Dispose();
        }

        private void SaveUserData(UserModel model)
        {
            string userData = JsonUtility.ToJson(UserModel);
            
            PlayerPrefs.SetString(USER_DATA_KEY, userData);
        }

        private void OnStartGame()
        {
            UserModel.UpdateModel();
        }

        private void LoadUserData()
        {
            if (PlayerPrefs.HasKey(USER_DATA_KEY))
            {
                try
                {
                    string json = PlayerPrefs.GetString(USER_DATA_KEY);
                    UserModel = JsonUtility.FromJson<UserModel>(json);
                    return;
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to parse user data!");
                }
            }
            
            UserModel = new UserModel(1, 5);
        }
    }
}