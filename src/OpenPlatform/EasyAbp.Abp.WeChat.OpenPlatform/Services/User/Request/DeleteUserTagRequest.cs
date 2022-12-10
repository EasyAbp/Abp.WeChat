namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public class DeleteUserTagRequest : OperationUserTagRequest
{
    public DeleteUserTagRequest(long tagId)
    {
        Tag = new UserTagDefinition
        {
            Id = tagId
        };
    }
}