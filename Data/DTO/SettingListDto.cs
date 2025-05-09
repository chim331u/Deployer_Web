using Deployer_Web.Data.Enum;

namespace Deployer_Web.Data.DTO;

public class SettingListDto
{
    public int Id { get; set; }
    public string? Alias { get; set; }
    public SettingType? Type { get; set; }
}