namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

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