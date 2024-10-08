using Source.Application;

namespace Source.Features.User
{
    public class UserModel : BaseModel<UserModel>
    {
        public int Level { get; private set; }

        public void IncreaseLevel()
        {
            Level++;
            UpdateModel();
        }
    }
}