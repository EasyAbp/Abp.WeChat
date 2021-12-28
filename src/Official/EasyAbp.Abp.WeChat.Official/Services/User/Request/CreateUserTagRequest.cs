namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
    public class CreateUserTagRequest : OperationUserTagRequest
    {
        public CreateUserTagRequest(string name)
        {
            Tag = new UserTagDefinition
            {
                Name = name
            };
        }
    }
}