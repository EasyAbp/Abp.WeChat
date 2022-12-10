namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;

public class UpdateUserTagRequest : OperationUserTagRequest
{
    public UpdateUserTagRequest(long tagId, string newTagName)
    {
        Tag = new UserTagDefinition
        {
            Id = tagId,
            Name = newTagName
        };
    }
}