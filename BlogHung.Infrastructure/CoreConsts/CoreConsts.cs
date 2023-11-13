namespace BlogHung.Infrastructure.CoreConsts
{
    public partial class CoreConsts
    {
        /// <summary>
        /// User's login account
        /// </summary>
        public const string ClaimAccountName = "account-name";

        /// <summary>
        /// Employee code
        /// </summary>
        public const string ClaimIAMAccountName = "employee_id";

        /// <summary>
        /// Login id
        /// </summary>
        public const string ClaimAccountId = "account-id";


        /// <summary>
        /// Range of services allowed to access
        /// </summary>
        public const string Scope = "scope";

        /// <summary>
        /// User information
        /// - mobile/desktop/web
        /// - server
        /// </summary>
        public const string ClaimSaleChannelId = "salechannel-id";

        /// <summary>
        /// Token id
        /// </summary>
        public const string ClaimIdToken = "jid";

        /// <summary>
        /// Login id
        /// </summary>
        public const string ClaimRoleName = "role-name";

        /// <summary>
        /// Login id
        /// </summary>
        public const string ClaimRoleId = "role-id";

    }
}
