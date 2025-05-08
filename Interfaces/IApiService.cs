using Deployer_Web.Data.DTO;
using Deployer_Web.Data.DTO.DockerDeployer;

namespace Deployer_Web.Interfaces
{
    public interface IApiService
    {
        string GetRestUrl();
        Task<List<DockerConfigListDto>> GetActiveDockerConfigList();
        Task<DockerConfigsDto?> GetDockerConfig(int id);
        Task<DockerConfigsDto> UpdateConfig(DockerConfigsDto dockerConfig);
        Task<DockerConfigsDto> AddConfig(DockerConfigsDto dockerConfig);
        Task<bool> DeleteConfig(int dockerConfigId);
        Task<List<DeployDetailDto>> GetDeployDetailList(string dockerConfigId);
        Task<DeployResult> GetDeployDetail(int deployDetatilId);
        Task<string> RunDeploy(int dockerConfigId);
        Task<string> GetDockerFile(DockerConfigsDto dockerConfig);

        Task<DeployResult> UpdateDeployDetail(int id, string result);
        Task<string> SendCommand(int id, string command);
        


    }
}
