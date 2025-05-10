using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Deployer_Web.Data;
using Deployer_Web.Data.DTO;
using Deployer_Web.Data.DTO.DockerDeployer;
using Deployer_Web.Interfaces;

namespace Deployer_Web.Service
{
    public class ApiServices : IApiService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _config;

        public ApiServices(IConfiguration config)
        {
            _httpClient = new HttpClient();

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                NumberHandling =
                    JsonNumberHandling.AllowReadingFromString |
                    JsonNumberHandling.WriteAsString,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            _config = config;
        }

        public async Task<List<DockerConfigListDto>> GetActiveDockerConfigList()
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/GetDockerConfigList", string.Empty));


            var dataResponse = new List<DockerConfigListDto>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dataResponse = JsonSerializer.Deserialize<List<DockerConfigListDto>>(content, _serializerOptions);
                }

                return dataResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<DockerConfigsDto?> GetDockerConfig(int id)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/GetDockerConfig/{id}", string.Empty));

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var dataResponse = JsonSerializer.Deserialize<DockerConfigsDto>(content, _serializerOptions);
                    return dataResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<DockerConfigsDto> UpdateConfig(DockerConfigsDto dockerConfig)
        {
            Uri uri = new Uri(
                string.Format(GetRestUrl() + $"api/v1/UpdateDockerConfig/{dockerConfig.Id}", string.Empty));

            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(uri, dockerConfig);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var dataResponse = JsonSerializer.Deserialize<DockerConfigsDto>(content, _serializerOptions);
                    return dataResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<bool> DeleteConfig(int dockerId)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/DeleteDockerConfig/{dockerId}", string.Empty));

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    // string content = await response.Content.ReadAsStringAsync();
                    // var dataResponse = JsonSerializer.Deserialize<DockerConfigsDto>(content, _serializerOptions);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return false;
            }
        }

        public async Task<DockerConfigsDto> AddConfig(DockerConfigsDto dockerConfig)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/AddDockerConfig", string.Empty));

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(uri, dockerConfig);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var dataResponse = JsonSerializer.Deserialize<DockerConfigsDto>(content, _serializerOptions);
                    return dataResponse;
                }

                return null;
                ////var dataTest = await _client.GetFromJsonAsync<FilesDetail[]>("api/FilesDetails");
                //var dataTest = await _client.GetFromJsonAsync<FilesDetail[]>(uri);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<List<DeployDetailDto>> GetDeployDetailList(string dockerConfigId)
        {
            Uri uri = new Uri(
                string.Format(GetRestUrl() + $"api/v1/GetDeployDetailList/{dockerConfigId}", string.Empty));


            var dataResponse = new List<DeployDetailDto>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dataResponse = JsonSerializer.Deserialize<List<DeployDetailDto>>(content, _serializerOptions);
                }

                return dataResponse;
                ////var dataTest = await _client.GetFromJsonAsync<FilesDetail[]>("api/FilesDetails");
                //var dataTest = await _client.GetFromJsonAsync<FilesDetail[]>(uri);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<DeployDetailDto> GetDeployDetail(int deployDetatilId)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/GetDeployDetailResult/{deployDetatilId}",
                string.Empty));


            DeployDetailDto dataResponse;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return dataResponse = JsonSerializer.Deserialize<DeployDetailDto>(content, _serializerOptions);
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<string> GetDockerFile(DockerConfigsDto dockerConfig)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/GetDockerFile", string.Empty));

            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(uri, dockerConfig);

                if (response.IsSuccessStatusCode)
                {
                    string _dockerFile = await response.Content.ReadAsStringAsync();
                    return _dockerFile;
                }

                return "Error";
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return ex.Message;
            }
        }

        public async Task<string> RunDeploy(int dockerConfigId)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/ExecuteFullDeploy/{dockerConfigId}", string.Empty));

            string _dataResponse;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return $"Error: {response.Content.Headers}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<SettingListDto>> GetSettingsList()
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/GetSettingList", string.Empty));

            var dataResponse = new List<SettingListDto>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dataResponse = JsonSerializer.Deserialize<List<SettingListDto>>(content, _serializerOptions);
                }

                return dataResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        public async Task<DeployDetailDto> UpdateDeployDetail(int id, string result)
        {
            Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/UpdateDeployDetailResult/{id}/{result}",
                string.Empty));

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var dataResponse = JsonSerializer.Deserialize<DeployDetailDto>(content, _serializerOptions);
                    return dataResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);

                return null;
            }
        }

        #region Service

        public string GetRestUrl()
        {
            var uri = _config.GetSection("Uri").Value;
            return uri;
        }

        public async Task<DockerCommandResponse<string>> SendCommand(int id, string command)
        {
            try
            {
                Uri uri = new Uri(string.Format(GetRestUrl() + $"api/v1/SendSSHCommand/{id}", string.Empty));

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(uri, JsonSerializer.Serialize(command));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var dataResponse = JsonSerializer.Deserialize<DockerCommandResponse<string>>(content, _serializerOptions);
                    return new DockerCommandResponse<string>(dataResponse.Data, dataResponse.Message, dataResponse.IsSuccess);
                }
                return new DockerCommandResponse<string>("Error: " + response.StatusCode, "Send Command", false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
                return new DockerCommandResponse<string>("Error: " + ex.Message, "Send Command", false);
            }
        }

        #endregion
    }
}