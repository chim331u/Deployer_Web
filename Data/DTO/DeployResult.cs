namespace Deployer_Web.Data.DTO
{
    public class DeployResult
    {
        public int Id { get; set; }
        public DateTime DeployStart { get; set; }
        public DateTime DeployEnd { get; set; }
        public string? Duration { get; set; }

        public bool? Result { get; set; }

        public string? LogFilePath { get; set; }
        public string? LogText { get; set; }
    }
}
