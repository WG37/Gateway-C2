namespace AgentClient.Infrastructure.Utilities.HWID
{
    public static class HwidNormalization
    {
        private static readonly string[] InvalidTokens = {
            "NULL",
            "DEFAULT",
            "DEFAULT STRING",
            "O.E.M",
            "OEM",
            "UNKOWN",
            "TO BE FILLED"
        };

        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }

            var result = input.Trim().ToUpperInvariant();

            result = result.Replace(" ", "")
                           .Replace("\t", "")
                           .Replace("\r", "")
                           .Replace("\n", "");

            foreach (var token in InvalidTokens)
            {
                if (result.Contains("token"))
                {
                    return "";
                }
            }

            return result;
        }
    }
}
