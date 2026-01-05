using AgentClient.Domain.Models.Agents;
using AgentClient.Infrastructure.Cryptography.FileDecryption;
using AgentClient.Infrastructure.Utilities.KeyStorage;

namespace AgentClient.Application.Commands
{
    public class DecryptFileCommand : AgentCommand
    {
        public override string Name => "decrypt-aes";

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
                var decrypt = new FileDecryptor(KeyStore.devKey);
                decrypt.DecryptFile(path);
            }
            catch (Exception ex)
            {
                return $"Decryption failed: {ex.Message}";
            }

            return "Decryption successful";
        }
    }
}
