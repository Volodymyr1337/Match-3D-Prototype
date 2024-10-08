using System;
using Source.Application;
using UnityEngine;

namespace Source.Features.User
{
    [Serializable]
    public class UserModel : BaseModel<UserModel>
    {
        [SerializeField] private int _level;
        [SerializeField] private int _lives;
        
        public int Level => _level;
        public int Lives => _lives;

        public UserModel(int level, int lives)
        {
            _level = level;
            _lives = lives;
            UpdateModel();
        }
        
        public void IncreaseLevel()
        {
            _level++;
            UpdateModel();
        }
        
        public void DeductLives()
        {
            _lives--;
            UpdateModel();
        }
    }
}