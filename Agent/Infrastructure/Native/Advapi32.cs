using System.Runtime.InteropServices;

namespace AgentClient.Infrastructure.Native
{
    public class Advapi32
    {
        public enum LogonUserProvider
        {
            LOGON32_PROVIDER_DEFAULT = 0,
            LOGON32_PROVIDER_WINNT35 = 1,
            LOGON32_PROVIDER_WINNT40 = 2,
            LOGON32_PROVIDER_WINNT50 = 3,
            LOGON32_PROVIDER_VIRTUAL = 4
        };

        public enum LogonUserType
        {
            LOGON32_LOGON_INTERACTIVE = 2,
            LOGON32_LOGON_NETWORK = 3,
            LOGON32_LOGON_BATCH = 4,
            LOGON32_LOGON_SERVICE = 5,
            LOGON32_LOGON_UNLOCK = 7,
            LOGON32_LOGON_CLEARTEXT = 8,
            LOGON32_LOGON_CREDENTIAL = 9,
            LOGON32_LOGON_NEW_CREDENTIALS = 10
        };

        [DllImport("Advapi32.dll",
            SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool LogonUserA(
            string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            LogonUserType dwLogonType,
            LogonUserProvider dwLogonProvider,
            out IntPtr phToken
            );

        [DllImport("Advapi32.dll", SetLastError = true)]
        public static extern bool ImpersonateLoggedOnUser(
            IntPtr hToken
            );

        [DllImport("Advapi32.dll", SetLastError = true)]
        public static extern bool RevertToSelf();
    }
}
