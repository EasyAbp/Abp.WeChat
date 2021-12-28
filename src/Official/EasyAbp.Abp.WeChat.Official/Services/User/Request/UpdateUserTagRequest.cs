namespace EasyAbp.Abp.WeChat.Official.Services.User.Request
{
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
}